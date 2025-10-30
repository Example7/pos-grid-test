import type { Product } from "./mockData";
import type { ColumnDef } from "@tanstack/react-table";
import type { GridColDef } from "@mui/x-data-grid";
import type { ColDef } from "ag-grid-community";

type BaseColumn = {
  key: keyof Product;
  label: string;
  editable?: boolean;
  width?: number;
  flex?: number;
};

// 🔹 Wszystkie kolumny z bazy danych products
export const productColumnsBase: BaseColumn[] = [
  { key: "id", label: "ID", editable: false, width: 80 },
  { key: "name", label: "Nazwa produktu", editable: true, flex: 1 },
  { key: "price", label: "Cena (PLN)", editable: true, width: 120 },
  { key: "category", label: "Kategoria", editable: true, flex: 1 },
  { key: "description", label: "Opis", editable: true, flex: 2 },
  { key: "sku", label: "SKU", editable: false },
  { key: "stock", label: "Stan magazynowy", editable: true },
  { key: "warehouse", label: "Magazyn", editable: true },
  { key: "brand", label: "Marka", editable: true },
  { key: "supplier", label: "Dostawca", editable: true },
  { key: "discount", label: "Zniżka (%)", editable: true },
  { key: "rating", label: "Ocena", editable: false },
  { key: "active", label: "Aktywny", editable: true },
  { key: "color", label: "Kolor", editable: true },
  { key: "size", label: "Rozmiar", editable: true },
  { key: "created_at", label: "Utworzono", editable: false },
  { key: "updated_at", label: "Zaktualizowano", editable: false },
];

export const tanstackColumns: ColumnDef<Product>[] = productColumnsBase.map(
  (col) => ({
    accessorKey: col.key,
    header: col.label,
  })
);

export const muiColumns: GridColDef<Product>[] = productColumnsBase.map(
  (col) => ({
    field: col.key,
    headerName: col.label,
    width: col.width,
    flex: col.flex,
    editable: col.editable ?? false,
  })
);

export const agColumns: ColDef<Product>[] = productColumnsBase.map((col) => ({
  field: col.key,
  headerName: col.label,
  width: col.width,
  flex: col.flex,
  editable: col.editable ?? false,
}));

export const devexpressColumns = productColumnsBase.map((col) => ({
  dataField: col.key,
  caption: col.label,
  width: col.width,
  allowEditing: col.editable ?? false,
}));
