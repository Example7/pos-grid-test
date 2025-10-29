"use client";

import * as React from "react";
import {
  DataGrid,
  GridRowEditStopReasons,
  type GridEventListener,
  type GridRowModel,
} from "@mui/x-data-grid";
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
import { useSupabaseProducts } from "@/hooks/useSupabaseProducts";
import { supabase } from "@/lib/supabaseClient";
import type { Product } from "@/lib/mockData";

export default function MuiGridView() {
  const { data: rows, loading, error, refetch } = useSupabaseProducts();

  const [paginationModel, setPaginationModel] = React.useState({
    pageSize: 10,
    page: 0,
  });

  const [selection, setSelection] = React.useState<number[]>([]);
  const [openAdd, setOpenAdd] = React.useState(false);
  const [newProduct, setNewProduct] = React.useState<Partial<Product>>({
    name: "",
    price: 0,
    category: "",
    description: "",
  });
  const [filter, setFilter] = React.useState("");

  const handleRowEditStop: GridEventListener<"rowEditStop"> = (
    params,
    event
  ) => {
    if (params.reason === GridRowEditStopReasons.rowFocusOut) {
      event.defaultMuiPrevented = true;
    }
  };

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

    refetch();
    return updatedRow;
  };

  const handleAddProduct = async () => {
    if (!newProduct.name || !newProduct.category) {
      alert("Uzupełnij nazwę i kategorię produktu");
      return;
    }

    const { error } = await supabase.from("products").insert([newProduct]);

    if (error) {
      console.error("Błąd dodawania:", error.message);
      alert("Błąd dodawania produktu");
    } else {
      setOpenAdd(false);
      setNewProduct({ name: "", price: 0, category: "", description: "" });
      refetch();
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
      refetch();
      setSelection([]);
    }
  };

  const filteredRows = rows.filter((r) =>
    r.name.toLowerCase().includes(filter.toLowerCase())
  );

  if (loading) return <p>Ładowanie danych...</p>;
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
          rows={filteredRows}
          columns={muiColumns}
          getRowId={(row) => row.id}
          pagination
          paginationModel={paginationModel}
          onPaginationModelChange={setPaginationModel}
          pageSizeOptions={[10, 25, 50, 100]}
          editMode="row"
          onRowEditStop={handleRowEditStop}
          processRowUpdate={handleProcessRowUpdate}
          checkboxSelection
          onRowSelectionModelChange={(newSelectionModel) => {
            const ids = Array.isArray(newSelectionModel)
              ? (newSelectionModel as (string | number)[]).map((id) =>
                  Number(id)
                )
              : [];
            setSelection(ids);
          }}
          disableRowSelectionOnClick
          sx={{
            border: "none",
            "& .MuiDataGrid-columnHeaders": {
              backgroundColor: "#f3f4f6",
              fontWeight: "bold",
            },
            "& .MuiDataGrid-cell": { fontSize: "0.95rem" },
            "& .MuiDataGrid-row:hover": { backgroundColor: "#f9fafb" },
          }}
        />
      </div>

      <Dialog open={openAdd} onClose={() => setOpenAdd(false)}>
        <DialogTitle>Dodaj nowy produkt</DialogTitle>
        <DialogContent
          sx={{ display: "flex", flexDirection: "column", gap: 2, mt: 1 }}
        >
          <TextField
            label="Nazwa"
            value={newProduct.name}
            onChange={(e) =>
              setNewProduct({ ...newProduct, name: e.target.value })
            }
          />
          <TextField
            label="Cena"
            type="number"
            value={newProduct.price}
            onChange={(e) =>
              setNewProduct({ ...newProduct, price: Number(e.target.value) })
            }
          />
          <TextField
            label="Kategoria"
            value={newProduct.category}
            onChange={(e) =>
              setNewProduct({ ...newProduct, category: e.target.value })
            }
          />
          <TextField
            label="Opis"
            value={newProduct.description}
            onChange={(e) =>
              setNewProduct({ ...newProduct, description: e.target.value })
            }
          />
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
