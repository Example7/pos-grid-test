import { useEffect, useState } from "react";
import { supabase } from "@/lib/supabaseClient";

export function useSupabaseProducts() {
  const [data, setData] = useState<any[]>([]);
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
  }, []);

  return { data, loading, error, refetch: fetchProducts };
}
