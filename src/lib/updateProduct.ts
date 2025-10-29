import { supabase } from "./supabaseClient";
import type { Product } from "./mockData";

export async function updateProduct(updatedRow: Product): Promise<boolean> {
  const { error } = await supabase
    .from("products")
    .update({
      name: updatedRow.name,
      price: updatedRow.price,
      category: updatedRow.category,
      description: updatedRow.description,
    })
    .eq("id", updatedRow.id);

  if (error) {
    console.error("Błąd zapisu:", error.message);
    return false;
  }
  return true;
}
