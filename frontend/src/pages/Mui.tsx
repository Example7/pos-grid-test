"use client";

import * as React from "react";
import { DataGrid, type GridRowModel } from "@mui/x-data-grid";
import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
} from "@mui/material";
import { muiColumns } from "@/lib/productColumn";
import { supabase } from "@/lib/supabaseClient";
import { addProduct } from "@/lib/addProduct";
import type { Product } from "@/lib/mockData";

export default function MuiGridView() {
  const [rows, setRows] = React.useState<Product[]>([]);
  const [rowCount, setRowCount] = React.useState(0);
  const [loading, setLoading] = React.useState(false);
  const [error, setError] = React.useState<string | null>(null);

  const [paginationModel, setPaginationModel] = React.useState({
    pageSize: 10,
    page: 0,
  });

  const [selection, setSelection] = React.useState<number[]>([]);
  const [openAdd, setOpenAdd] = React.useState(false);
  const [newProduct, setNewProduct] = React.useState<Partial<Product>>({});
  const [filter, setFilter] = React.useState("");

  const excludedFields = ["id", "created_at", "updated_at"];

  const fetchPage = React.useCallback(async () => {
    try {
      setLoading(true);
      const { page, pageSize } = paginationModel;
      const from = page * pageSize;
      const to = from + pageSize - 1;

      let query = supabase
        .from("products")
        .select("*", { count: "exact" })
        .order("id", { ascending: true });

      if (filter) query = query.ilike("name", `%${filter}%`);

      query = query.range(from, to);

      const { data, count, error } = await query;

      if (error) throw error;

      setRows(data ?? []);
      setRowCount(count ?? 0);
      setError(null);
    } catch (err) {
      console.error("Błąd pobierania danych:", err);
      setError((err as Error).message);
    } finally {
      setLoading(false);
    }
  }, [paginationModel, filter]);

  React.useEffect(() => {
    fetchPage();
  }, [fetchPage]);

  const handleProcessRowUpdate = async (newRow: GridRowModel<Product>) => {
    const updatedRow = newRow as Product;
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
      console.error("Błąd aktualizacji:", error.message);
      alert("Błąd zapisu w bazie");
      return newRow;
    }

    fetchPage();
    return updatedRow;
  };

  const handleAddProduct = async () => {
    try {
      if (Object.keys(newProduct).length === 0) {
        alert("Uzupełnij dane produktu");
        return;
      }

      await addProduct(newProduct);
      setOpenAdd(false);
      setNewProduct({});
      fetchPage();
    } catch (error) {
      console.error("Błąd dodawania produktu:", (error as Error).message);
      alert("Nie udało się dodać produktu");
    }
  };

  const handleDeleteSelected = async () => {
    if (selection.length === 0) {
      alert("Zaznacz rekordy do usunięcia");
      return;
    }

    if (!confirm(`Na pewno usunąć ${selection.length} produktów?`)) return;

    const { error } = await supabase
      .from("products")
      .delete()
      .in("id", selection);

    if (error) {
      console.error("Błąd usuwania:", error.message);
      alert("Błąd usuwania produktów");
    } else {
      alert("Usunięto zaznaczone produkty.");
      fetchPage();
      setSelection([]);
    }
  };

  if (error) return <p>Błąd: {error}</p>;

  return (
    <>
      <Box
        sx={{
          mb: 2,
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <TextField
          label="Szukaj..."
          variant="outlined"
          size="small"
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && fetchPage()}
          sx={{ width: 250 }}
        />
        <Box>
          <Button
            variant="contained"
            onClick={() => setOpenAdd(true)}
            sx={{ mr: 1 }}
          >
            + Dodaj produkt
          </Button>
          <Button
            variant="outlined"
            color="error"
            onClick={handleDeleteSelected}
            disabled={selection.length === 0}
          >
            Usuń zaznaczone
          </Button>
        </Box>
      </Box>

      <div
        style={{
          height: 600,
          width: "100%",
          backgroundColor: "white",
          borderRadius: "0.75rem",
          overflow: "hidden",
          boxShadow: "0 2px 10px rgba(0,0,0,0.1)",
        }}
      >
        <DataGrid
          rows={rows}
          rowCount={rowCount}
          columns={muiColumns}
          getRowId={(row) => row.id}
          loading={loading}
          pagination
          paginationMode="server"
          paginationModel={paginationModel}
          onPaginationModelChange={setPaginationModel}
          pageSizeOptions={[10, 25, 50, 100]}
          editMode="row"
          processRowUpdate={handleProcessRowUpdate}
          checkboxSelection
          disableRowSelectionOnClick
          onRowSelectionModelChange={(newSelectionModel) => {
            if (Array.isArray(newSelectionModel)) {
              setSelection(newSelectionModel.map((id) => Number(id)));
            } else if (
              typeof newSelectionModel === "object" &&
              newSelectionModel !== null &&
              "ids" in newSelectionModel &&
              newSelectionModel.ids instanceof Set
            ) {
              const idsArray = Array.from(newSelectionModel.ids) as (
                | string
                | number
              )[];
              setSelection(idsArray.map((id) => Number(id)));
            } else {
              console.warn(
                "Nieoczekiwany typ selectionModel:",
                newSelectionModel
              );
              setSelection([]);
            }
          }}
        />
      </div>

      <Dialog open={openAdd} onClose={() => setOpenAdd(false)}>
        <DialogTitle>Dodaj nowy produkt</DialogTitle>
        <DialogContent
          sx={{ display: "flex", flexDirection: "column", gap: 2, mt: 1 }}
        >
          {Object.keys(rows[0] ?? {})
            .filter((key) => !excludedFields.includes(key))
            .map((key) => (
              <TextField
                key={key}
                label={key.charAt(0).toUpperCase() + key.slice(1)}
                value={String(newProduct[key as keyof Product] ?? "")}
                onChange={(e) =>
                  setNewProduct((prev) => ({
                    ...prev,
                    [key]:
                      key.toLowerCase().includes("price") ||
                      key.toLowerCase().includes("amount") ||
                      key.toLowerCase().includes("ilosc")
                        ? Number(e.target.value)
                        : e.target.value,
                  }))
                }
                type={
                  key.toLowerCase().includes("price") ||
                  key.toLowerCase().includes("amount") ||
                  key.toLowerCase().includes("ilosc")
                    ? "number"
                    : "text"
                }
              />
            ))}
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenAdd(false)}>Anuluj</Button>
          <Button onClick={handleAddProduct} variant="contained">
            Dodaj
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}
