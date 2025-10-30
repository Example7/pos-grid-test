import { supabase } from "./supabaseClient";
import type { Product } from "./mockData";

export async function addProduct(
  newProduct: Partial<Omit<Product, "id" | "created_at" | "updated_at">>
): Promise<Product> {
  const cleanData = { ...newProduct } as Record<string, unknown>;
  delete cleanData.id;
  delete cleanData.created_at;
  delete cleanData.updated_at;

  const { data, error } = await supabase
    .from("products")
    .insert([cleanData])
    .select()
    .single();

  if (error) {
    console.error("Błąd dodawania produktu:", error.message);
    throw new Error(error.message);
  }

  console.log("Dodano produkt:", data);
  return data as Product;
}
