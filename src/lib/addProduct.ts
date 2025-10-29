import { supabase } from "./supabaseClient";
import type { Product } from "./mockData";

export async function addProduct(newProduct: Omit<Product, "id">) {
  const { data, error } = await supabase
    .from("products")
    .insert([newProduct])
    .select();

  if (error) {
    console.error("Błąd dodawania produktu:", error.message);
    throw new Error(error.message);
  }

  console.log("Dodano produkt:", data?.[0]);
  return data?.[0];
}
