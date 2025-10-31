import { useEffect, useRef, useState } from "react";
import { supabase } from "@/lib/supabaseClient";
import type { Product } from "@/lib/mockData";

export function useSupabaseProducts() {
  const [data, setData] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [progress, setProgress] = useState(0);
  const initialized = useRef(false);

  const fetchAllProducts = async () => {
    setLoading(true);
    setError(null);
    setProgress(0);

    const chunkSize = 1000;
    const allData: Product[] = [];

    try {
      const { count, error: countError } = await supabase
        .from("products")
        .select("*", { count: "exact", head: true });

      if (countError) throw countError;
      const totalCount = count ?? 0;
      const totalChunks = Math.ceil(totalCount / chunkSize);

      for (let i = 0; i < totalChunks; i += 10) {
        const batch = Array.from(
          { length: Math.min(10, totalChunks - i) },
          (_, j) => {
            const chunkIndex = i + j;
            return supabase
              .from("products")
              .select("*")
              .order("id", { ascending: true })
              .range(chunkIndex * chunkSize, (chunkIndex + 1) * chunkSize - 1);
          }
        );

        const results = await Promise.all(batch);
        results.forEach((r) => {
          if (r.error) console.error("Błąd chunku:", r.error.message);
          if (r.data) allData.push(...r.data);
        });

        setProgress(Math.min(((i + 10) / totalChunks) * 100, 100));
      }

      const uniqueData = Array.from(
        new Map(allData.map((item) => [item.id, item])).values()
      );

      setData(uniqueData);
    } catch (err) {
      if (err instanceof Error) setError(err.message);
      else setError("Nieznany błąd podczas pobierania danych");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (initialized.current) return;
    initialized.current = true;

    fetchAllProducts();

    supabase.removeAllChannels();

    const channel = supabase
      .channel("products-realtime")
      .on(
        "postgres_changes",
        { event: "*", schema: "public", table: "products" },
        (payload) => {
          setData((prev) => {
            const newRow = payload.new as Product;
            const oldRow = payload.old as Product;
            switch (payload.eventType) {
              case "INSERT":
                return prev.some((p) => p.id === newRow.id)
                  ? prev
                  : [...prev, newRow];
              case "UPDATE":
                return prev.map((p) => (p.id === newRow.id ? newRow : p));
              case "DELETE":
                return prev.filter((p) => p.id !== oldRow.id);
              default:
                return prev;
            }
          });
        }
      )
      .subscribe();

    return () => {
      supabase.removeChannel(channel);
    };
  }, []);

  return { data, loading, error, progress, refetch: fetchAllProducts };
}
