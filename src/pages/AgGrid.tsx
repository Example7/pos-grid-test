"use client";

import {
  AllCommunityModule,
  ModuleRegistry,
  type ColDef,
} from "ag-grid-community";
import type { GridApi } from "ag-grid-community";

import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-theme-quartz.css";

import { useMemo, useRef, useState } from "react";
import { useSupabaseProducts } from "@/hooks/useSupabaseProducts";
import { agColumns } from "@/lib/productColumn";
import { updateProduct } from "@/lib/updateProduct";
import { addProduct } from "@/lib/addProduct";
import { deleteProduct } from "@/lib/deleteProduct";
import { LoadingSpinner } from "@/components/LoadingSpinner";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Dialog,
  DialogHeader,
  DialogContent,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import { Label } from "@/components/ui/label";

ModuleRegistry.registerModules([AllCommunityModule]);

export default function AgGridView() {
  const {
    data: rowData,
    loading,
    error,
    progress,
    refetch,
  } = useSupabaseProducts();

  const gridRef = useRef<AgGridReact>(null);
  const [gridApi, setGridApi] = useState<GridApi | null>(null);
  const [quickFilter, setQuickFilter] = useState("");
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [form, setForm] = useState({
    name: "",
    price: 0,
    category: "",
    description: "",
  });

  const columnDefs = useMemo<ColDef[]>(() => {
    return agColumns.map((col) => ({
      ...col,
      editable: col.field !== "id",
      onCellValueChanged: async (params) => {
        const success = await updateProduct(params.data);
        if (success) refetch();
        else alert("Błąd zapisu do bazy");
      },
    }));
  }, [refetch]);

  const defaultColDef = useMemo<ColDef>(
    () => ({
      sortable: true,
      filter: true,
      resizable: true,
      flex: 1,
      minWidth: 120,
    }),
    []
  );

  const handleAddProduct = async () => {
    if (!form.name || !form.category) {
      alert("Uzupełnij nazwę i kategorię produktu!");
      return;
    }

    try {
      await addProduct({
        name: form.name,
        price: Number(form.price),
        category: form.category,
        description: form.description,
        sku: crypto.randomUUID(),
        stock: 100,
        warehouse: "Nowy Sącz",
        brand: "Generic",
        supplier: "Default Supplier",
        discount: 0,
        rating: 0,
        active: true,
        color: "czarny",
        size: "M",
      });
      refetch();
      setIsDialogOpen(false);
      setForm({ name: "", price: 0, category: "", description: "" });
    } catch (err) {
      console.error(err);
      alert("Błąd dodawania produktu");
    }
  };

  const handleDeleteSelected = async () => {
    if (!gridApi) return;
    const selected = gridApi.getSelectedRows();
    if (selected.length === 0) {
      alert("Zaznacz wiersze do usunięcia");
      return;
    }
    if (!confirm(`Na pewno usunąć ${selected.length} produktów?`)) return;

    for (const row of selected) {
      await deleteProduct(row.id);
    }

    refetch();
  };

  if (loading)
    return (
      <LoadingSpinner text="Wczytywanie produktów..." progress={progress} />
    );

  if (error)
    return (
      <p className="text-red-600 text-center font-medium p-6">
        Błąd podczas ładowania danych: {error}
      </p>
    );

  return (
    <div className="p-6">
      <div className="flex justify-between mb-4">
        <div className="flex gap-2">
          <Input
            placeholder="Szukaj..."
            value={quickFilter}
            onChange={(e) => {
              setQuickFilter(e.target.value);
              gridApi?.setGridOption("quickFilterText", e.target.value);
            }}
            className="w-64"
          />
          <Button onClick={() => gridApi?.setFilterModel(null)}>Resetuj</Button>
        </div>
        <div className="flex gap-2">
          <Button onClick={() => setIsDialogOpen(true)}>+ Dodaj produkt</Button>
          <Button variant="destructive" onClick={handleDeleteSelected}>
            Usuń zaznaczone
          </Button>
        </div>
      </div>

      <div
        className="ag-theme-quartz rounded-lg border shadow-sm"
        style={{ height: "75vh", width: "100%" }}
      >
        <AgGridReact
          ref={gridRef}
          onGridReady={(params) => setGridApi(params.api)}
          rowData={rowData}
          columnDefs={columnDefs}
          defaultColDef={defaultColDef}
          pagination
          paginationPageSize={20}
          rowSelection="multiple"
          animateRows
          suppressRowClickSelection
          quickFilterText={quickFilter}
        />
      </div>

      <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
        <DialogContent className="max-w-md">
          <DialogHeader>
            <DialogTitle>Dodaj nowy produkt</DialogTitle>
          </DialogHeader>
          <div className="grid gap-3 py-2">
            <div>
              <Label>Nazwa</Label>
              <Input
                value={form.name}
                onChange={(e) => setForm({ ...form, name: e.target.value })}
              />
            </div>
            <div>
              <Label>Cena (PLN)</Label>
              <Input
                type="number"
                value={form.price}
                onChange={(e) =>
                  setForm({ ...form, price: Number(e.target.value) })
                }
              />
            </div>
            <div>
              <Label>Kategoria</Label>
              <Input
                value={form.category}
                onChange={(e) => setForm({ ...form, category: e.target.value })}
              />
            </div>
            <div>
              <Label>Opis</Label>
              <Input
                value={form.description}
                onChange={(e) =>
                  setForm({ ...form, description: e.target.value })
                }
              />
            </div>
          </div>

          <DialogFooter>
            <Button variant="outline" onClick={() => setIsDialogOpen(false)}>
              Anuluj
            </Button>
            <Button onClick={handleAddProduct}>Dodaj</Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
