"use client";

import {
  AllCommunityModule,
  ModuleRegistry,
  InfiniteRowModelModule,
  NumberFilterModule,
  TextFilterModule,
  RowSelectionModule,
  type IDatasource,
  type IGetRowsParams,
  type GridApi,
  type GridReadyEvent,
  type CellValueChangedEvent,
} from "ag-grid-community";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-theme-quartz.css";
import { useMemo, useRef, useState, useCallback } from "react";
import { supabase } from "@/lib/supabaseClient";
import { addProduct } from "@/lib/addProduct";
import { deleteProduct } from "@/lib/deleteProduct";
import type { Product } from "@/lib/mockData";
import { agColumns } from "@/lib/productColumn";
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

ModuleRegistry.registerModules([
  AllCommunityModule,
  InfiniteRowModelModule,
  NumberFilterModule,
  TextFilterModule,
  RowSelectionModule,
]);

type TextFilter = {
  filterType: "text";
  type: "contains" | "equals" | "startsWith" | "endsWith";
  filter: string;
};

type NumberFilter = {
  filterType: "number";
  type:
    | "equals"
    | "lessThan"
    | "lessThanOrEqual"
    | "greaterThan"
    | "greaterThanOrEqual"
    | "inRange";
  filter: number;
  filterTo?: number;
};

export default function AgGridView() {
  const gridRef = useRef<AgGridReact<Product>>(null);
  const [gridApi, setGridApi] = useState<GridApi<Product> | null>(null);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [form, setForm] = useState<Partial<Product>>({});

  const defaultColDef = useMemo(
    () => ({
      sortable: true,
      resizable: true,
      flex: 1,
      minWidth: 140,
    }),
    []
  );

  const createDatasource = useCallback(
    (): IDatasource => ({
      getRows: async (params: IGetRowsParams) => {
        try {
          const { startRow, endRow, sortModel, filterModel } = params;
          let query = supabase
            .from("products")
            .select("*", { count: "exact" })
            .range(startRow, endRow - 1);

          if (sortModel && sortModel.length > 0) {
            const sort = sortModel[0];
            query = query.order(sort.colId, { ascending: sort.sort === "asc" });
          }

          const numericColumns = ["id", "price", "stock"];

          for (const [key, filter] of Object.entries(filterModel)) {
            const f = filter as TextFilter | NumberFilter;

            if (numericColumns.includes(key) && f.filterType === "text")
              continue;

            if (f.filterType === "text") {
              if (f.type === "contains")
                query = query.ilike(key, `%${f.filter}%`);
              else if (f.type === "equals") query = query.eq(key, f.filter);
              else if (f.type === "startsWith")
                query = query.ilike(key, `${f.filter}%`);
              else if (f.type === "endsWith")
                query = query.ilike(key, `%${f.filter}`);
            } else if (f.filterType === "number") {
              switch (f.type) {
                case "equals":
                  query = query.eq(key, f.filter);
                  break;
                case "lessThan":
                  query = query.lt(key, f.filter);
                  break;
                case "lessThanOrEqual":
                  query = query.lte(key, f.filter);
                  break;
                case "greaterThan":
                  query = query.gt(key, f.filter);
                  break;
                case "greaterThanOrEqual":
                  query = query.gte(key, f.filter);
                  break;
                case "inRange":
                  if (f.filterTo !== undefined)
                    query = query.gte(key, f.filter).lte(key, f.filterTo);
                  break;
              }
            }
          }

          const { data, count, error } = await query;
          if (error) throw error;
          params.successCallback(data ?? [], count ?? 0);
        } catch (err) {
          console.error("Błąd pobierania danych:", err);
          params.failCallback();
        }
      },
    }),
    []
  );

  const onGridReady = (params: GridReadyEvent<Product>) => {
    setGridApi(params.api);
    params.api.setGridOption("datasource", createDatasource());
  };

  const handleAddProduct = async () => {
    if (!form.name || !form.price || !form.category) {
      alert("Uzupełnij nazwę, kategorię i cenę produktu!");
      return;
    }
    await addProduct({
      ...form,
      sku: crypto.randomUUID(),
      price: Number(form.price),
      stock: Number(form.stock ?? 0),
      active: true,
    });
    setIsDialogOpen(false);
    gridApi?.refreshInfiniteCache();
    setForm({});
  };

  const handleDeleteSelected = async () => {
    if (!gridApi) return;
    const selected = gridApi.getSelectedRows();
    if (selected.length === 0) {
      alert("Zaznacz wiersze do usunięcia");
      return;
    }
    if (!confirm(`Na pewno usunąć ${selected.length} produktów?`)) return;

    for (const row of selected) await deleteProduct(row.id);
    gridApi.refreshInfiniteCache();
  };

  const handleCellValueChanged = async (
    event: CellValueChangedEvent<Product>
  ) => {
    const { data, colDef, newValue, oldValue } = event;

    if (newValue === oldValue) return;

    try {
      const { error } = await supabase
        .from("products")
        .update({ [colDef.field!]: newValue })
        .eq("id", data.id);

      if (error) throw error;

      console.log(`Zmieniono ${colDef.field} dla produktu ${data.id}`);
    } catch (err) {
      console.error("Błąd podczas aktualizacji:", err);
      alert("Nie udało się zapisać zmian w bazie.");
    }
  };

  return (
    <div className="p-6">
      <div className="flex justify-between mb-4">
        <div></div>
        <div className="flex gap-2">
          <Button onClick={() => setIsDialogOpen(true)}>+ Dodaj produkt</Button>
          <Button variant="destructive" onClick={handleDeleteSelected}>
            Usuń zaznaczone
          </Button>
        </div>
      </div>

      <div
        className="ag-theme-quartz rounded-lg border shadow-sm bg-white"
        style={{ height: "75vh", width: "100%" }}
      >
        <AgGridReact<Product>
          ref={gridRef}
          onGridReady={onGridReady}
          columnDefs={agColumns}
          defaultColDef={defaultColDef}
          rowModelType="infinite"
          cacheBlockSize={20}
          pagination
          paginationPageSize={20}
          rowSelection={{
            mode: "multiRow",
            checkboxes: true,
          }}
          animateRows
          onCellValueChanged={handleCellValueChanged}
        />
      </div>

      <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
        <DialogContent className="max-w-3xl">
          <DialogHeader>
            <DialogTitle>Dodaj nowy produkt</DialogTitle>
          </DialogHeader>

          <div className="grid grid-cols-2 gap-4 py-2">
            {["name", "category", "price", "stock", "supplier"].map((key) => (
              <div key={key}>
                <Label>{key}</Label>
                <Input
                  type={["price", "stock"].includes(key) ? "number" : "text"}
                  value={String(form[key as keyof Product] ?? "")}
                  onChange={(e) =>
                    setForm({
                      ...form,
                      [key]: ["price", "stock"].includes(key)
                        ? Number(e.target.value)
                        : e.target.value,
                    })
                  }
                />
              </div>
            ))}
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
