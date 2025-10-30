export type Product = {
  id: number;
  name: string;
  price: number;
  category: string;
  description: string | null;
  sku: string;
  stock: number;
  warehouse: string;
  brand: string;
  supplier: string;
  created_at: string | null;
  updated_at: string | null;
  discount: number | null;
  rating: number | null;
  active: boolean | null;
  color: string | null;
  size: string | null;
};
