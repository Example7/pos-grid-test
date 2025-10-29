export type Product = {
  id: number;
  name: string;
  price: number;
  category: string;
  description: string;
};

export function generateProducts(count: number = 1000): Product[] {
  const categories = ["RTV", "AGD", "Sport", "Odzież"];
  return Array.from({ length: count }).map((_, i) => ({
    id: i + 1,
    name: `Produkt ${i + 1}`,
    price: Math.floor(Math.random() * 1000),
    category: categories[i % categories.length],
    description: `Opis produktu ${i + 1}`,
  }));
}
