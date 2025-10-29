import { useEffect, useState } from "react";
import { supabase } from "@/lib/supabaseClient";
import type { Product } from "@/lib/mockData";

export function useSupabaseProducts() {
  const [data, setData] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  async function fetchProducts() {
    setLoading(true);
    setError(null);

    const { data, error } = await supabase
      .from("products")
      .select("*")
      .order("id", { ascending: true });

    if (error) setError(error.message);
    else setData(data || []);
    setLoading(false);
  }

  useEffect(() => {
    fetchProducts();

    const channel = supabase
      .channel("products-realtime")
      .on(
        "postgres_changes",
        {
          event: "*",
          schema: "public",
          table: "products",
        },
        (payload) => {
          console.log("Realtime event:", payload);

          setData((prev) => {
            if (payload.eventType === "INSERT" && payload.new) {
              return [...prev, payload.new as Product];
            }

            if (payload.eventType === "UPDATE" && payload.new) {
              return prev.map((p) =>
                p.id === payload.new.id ? (payload.new as Product) : p
              );
            }

            if (payload.eventType === "DELETE" && payload.old) {
              return prev.filter((p) => p.id !== payload.old.id);
            }

            return prev;
          });
        }
      )
      .subscribe();

    return () => {
      supabase.removeChannel(channel);
    };
  }, []);

  return { data, loading, error, refetch: fetchProducts };
}
