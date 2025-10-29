import { supabase } from "./supabaseClient";

export async function deleteProduct(id: number): Promise<boolean> {
  const { error } = await supabase.from("products").delete().eq("id", id);

  if (error) {
    console.error("Błąd usuwania:", error.message);
    return false;
  }
  return true;
}
