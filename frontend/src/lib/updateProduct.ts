import { supabase } from "./supabaseClient";
import type { Product } from "./mockData";

export async function updateProduct(updatedRow: Product): Promise<boolean> {
  const { id, ...dataToUpdate } = updatedRow;

  const { error } = await supabase
    .from("products")
    .update({
      ...dataToUpdate,
      updated_at: new Date().toISOString(),
    })
    .eq("id", id);

  if (error) {
    console.error("Błąd aktualizacji produktu:", error.message);
    return false;
  }

  console.log("Zaktualizowano produkt:", id);
  return true;
}
