"use client";

import { useCallback, useEffect, useMemo, useState } from "react";
import {
  flexRender,
  getCoreRowModel,
  useReactTable,
  type ColumnDef,
} from "@tanstack/react-table";
import {
  Table,
  TableHeader,
  TableHead,
  TableRow,
  TableBody,
  TableCell,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import { Input } from "@/components/ui/input";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import { Label } from "@/components/ui/label";
import { supabase } from "@/lib/supabaseClient";
import { addProduct } from "@/lib/addProduct";
import { updateProduct } from "@/lib/updateProduct";
import { deleteProduct } from "@/lib/deleteProduct";
import type { Product } from "@/lib/mockData";

export default function ShadcnTable() {
  const [data, setData] = useState<Product[]>([]);
  const [rowCount, setRowCount] = useState(0);
  const [error, setError] = useState<string | null>(null);
  const [filter, setFilter] = useState("");
  const [pagination, setPagination] = useState({ pageIndex: 0, pageSize: 10 });

  const [editing, setEditing] = useState<Product | null>(null);
  const [form, setForm] = useState<Partial<Product>>({});
  const [isNew, setIsNew] = useState(false);

  const fetchPage = useCallback(async () => {
    try {
      const from = pagination.pageIndex * pagination.pageSize;
      const to = from + pagination.pageSize - 1;

      let query = supabase
        .from("products")
        .select("*", { count: "exact" })
        .order("id", { ascending: true })
        .range(from, to);

      if (filter.trim()) query = query.ilike("name", `%${filter}%`);

      const { data, count, error } = await query;
      if (error) throw error;

      setData(data ?? []);
      setRowCount(count ?? 0);
      setError(null);
    } catch (err) {
      console.error("Błąd pobierania danych:", err);
      setError((err as Error).message);
    }
  }, [pagination, filter]);

  useEffect(() => {
    fetchPage();
  }, [fetchPage]);

  const columns: ColumnDef<Product>[] = useMemo(
    () => [
      { accessorKey: "id", header: "ID" },
      { accessorKey: "name", header: "Nazwa" },
      { accessorKey: "price", header: "Cena (PLN)" },
      { accessorKey: "category", header: "Kategoria" },
      { accessorKey: "brand", header: "Marka" },
      { accessorKey: "supplier", header: "Dostawca" },
      { accessorKey: "warehouse", header: "Magazyn" },
      { accessorKey: "stock", header: "Stan" },
      { accessorKey: "discount", header: "Rabat (%)" },
      {
        id: "actions",
        header: "Akcje",
        cell: ({ row }) => (
          <div className="flex gap-2">
            <Button
              variant="outline"
              size="sm"
              onClick={() => {
                setIsNew(false);
                setEditing(row.original);
                setForm(row.original);
              }}
            >
              Edytuj
            </Button>
            <Button
              variant="destructive"
              size="sm"
              onClick={async () => {
                if (confirm(`Na pewno usunąć "${row.original.name}"?`)) {
                  await deleteProduct(row.original.id);
                  fetchPage();
                }
              }}
            >
              Usuń
            </Button>
          </div>
        ),
      },
    ],
    [fetchPage]
  );

  const table = useReactTable({
    data,
    columns,
    pageCount: Math.ceil(rowCount / pagination.pageSize),
    state: { pagination },
    manualPagination: true,
    getCoreRowModel: getCoreRowModel(),
    onPaginationChange: setPagination,
  });

  const handleSave = async () => {
    if (!form.name || !form.category) {
      alert("Uzupełnij nazwę i kategorię produktu!");
      return;
    }

    if (isNew) {
      await addProduct(form);
    } else if (editing) {
      await updateProduct({ ...editing, ...form } as Product);
    }

    setEditing(null);
    setForm({});
    setIsNew(false);
    fetchPage();
  };

  if (error)
    return (
      <p className="text-red-600 font-medium p-6">
        Błąd podczas ładowania danych: {error}
      </p>
    );

  return (
    <div className="p-6">
      <div className="flex justify-between items-center mb-4">
        <div className="flex gap-2">
          <Input
            placeholder="Szukaj po nazwie..."
            value={filter}
            onChange={(e) => setFilter(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && fetchPage()}
            className="w-64"
          />
          <Button
            onClick={() => {
              setIsNew(true);
              setEditing({} as Product);
              setForm({});
            }}
          >
            + Dodaj produkt
          </Button>
        </div>
      </div>

      <div className="rounded-md border overflow-auto h-[70vh] bg-white">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableHead key={header.id}>
                    {flexRender(
                      header.column.columnDef.header,
                      header.getContext()
                    )}
                  </TableHead>
                ))}
              </TableRow>
            ))}
          </TableHeader>

          <TableBody>
            {table.getRowModel().rows.map((row) => (
              <TableRow key={row.id}>
                {row.getVisibleCells().map((cell) => (
                  <TableCell key={cell.id} className="text-sm">
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>

      <div className="flex justify-center mt-6">
        <Pagination>
          <PaginationContent className="flex items-center gap-2">
            <PaginationItem>
              <PaginationPrevious
                onClick={() =>
                  pagination.pageIndex > 0 &&
                  setPagination((p) => ({ ...p, pageIndex: p.pageIndex - 1 }))
                }
                className={
                  pagination.pageIndex === 0
                    ? "opacity-50 pointer-events-none"
                    : ""
                }
              />
            </PaginationItem>

            <PaginationItem>
              <PaginationLink>{pagination.pageIndex + 1}</PaginationLink>
            </PaginationItem>

            <PaginationItem>
              <PaginationNext
                onClick={() =>
                  (pagination.pageIndex + 1) * pagination.pageSize < rowCount &&
                  setPagination((p) => ({ ...p, pageIndex: p.pageIndex + 1 }))
                }
                className={
                  (pagination.pageIndex + 1) * pagination.pageSize >= rowCount
                    ? "opacity-50 pointer-events-none"
                    : ""
                }
              />
            </PaginationItem>
          </PaginationContent>
        </Pagination>
      </div>

      <Dialog open={!!editing} onOpenChange={() => setEditing(null)}>
        <DialogContent className="max-w-3xl">
          <DialogHeader>
            <DialogTitle>
              {isNew ? "Dodaj produkt" : "Edytuj produkt"}
            </DialogTitle>
          </DialogHeader>

          <div className="grid grid-cols-2 gap-4 py-2">
            {Object.entries({
              name: "Nazwa",
              price: "Cena (PLN)",
              category: "Kategoria",
              description: "Opis",
              brand: "Marka",
              supplier: "Dostawca",
              warehouse: "Magazyn",
              color: "Kolor",
              size: "Rozmiar",
              stock: "Stan",
              discount: "Rabat (%)",
              rating: "Ocena (0–5)",
            }).map(([key, label]) => (
              <div key={key}>
                <Label>{label}</Label>
                <Input
                  type={
                    ["price", "stock", "discount", "rating"].includes(key)
                      ? "number"
                      : "text"
                  }
                  value={String(form[key as keyof Product] ?? "")}
                  onChange={(e) =>
                    setForm({
                      ...form,
                      [key]: ["price", "stock", "discount", "rating"].includes(
                        key
                      )
                        ? Number(e.target.value)
                        : e.target.value,
                    })
                  }
                />
              </div>
            ))}

            <div className="flex items-center gap-2 mt-4">
              <input
                type="checkbox"
                checked={form.active ?? true}
                onChange={(e) => setForm({ ...form, active: e.target.checked })}
              />
              <Label>Aktywny</Label>
            </div>
          </div>

          <DialogFooter>
            <Button variant="outline" onClick={() => setEditing(null)}>
              Anuluj
            </Button>
            <Button onClick={handleSave}>{isNew ? "Dodaj" : "Zapisz"}</Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
