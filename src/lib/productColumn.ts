import type { ColumnDef } from "@tanstack/react-table";
import type { GridColDef } from "@mui/x-data-grid";
import type { Product } from "./mockData";
import type { ColDef } from "ag-grid-community";

export const tanstackColumns: ColumnDef<Product>[] = [
  { accessorKey: "id", header: "ID" },
  { accessorKey: "name", header: "Nazwa produktu" },
  { accessorKey: "price", header: "Cena (PLN)" },
  { accessorKey: "category", header: "Kategoria" },
  { accessorKey: "description", header: "Opis" },
];

export const muiColumns: GridColDef<Product>[] = [
  { field: "id", headerName: "ID", width: 100 },
  { field: "name", headerName: "Nazwa produktu", flex: 1, editable: true },
  { field: "price", headerName: "Cena (PLN)", flex: 1, editable: true },
  { field: "category", headerName: "Kategoria", flex: 1, editable: true },
  { field: "description", headerName: "Opis", flex: 2, editable: true },
];

export const agColumns: ColDef<Product>[] = [
  { field: "id", headerName: "ID", width: 90 },
  { field: "name", headerName: "Nazwa produktu" },
  { field: "price", headerName: "Cena (PLN)" },
  { field: "category", headerName: "Kategoria" },
  { field: "description", headerName: "Opis" },
];

export const devexpressColumns = [
  { dataField: "id", caption: "ID", width: 80, allowEditing: false },
  { dataField: "name", caption: "Nazwa produktu", allowEditing: true },
  { dataField: "price", caption: "Cena (PLN)", width: 120, allowEditing: true },
  {
    dataField: "category",
    caption: "Kategoria",
    width: 150,
    allowEditing: true,
  },
  { dataField: "description", caption: "Opis", allowEditing: true },
];
