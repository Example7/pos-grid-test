"use client";

import { useMemo, useState } from "react";
import {
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getPaginationRowModel,
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
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";

import { useSupabaseProducts } from "@/hooks/useSupabaseProducts";
import { addProduct } from "@/lib/addProduct";
import { updateProduct } from "@/lib/updateProduct";
import { deleteProduct } from "@/lib/deleteProduct";
import type { Product } from "@/lib/mockData";
import { LoadingSpinner } from "@/components/LoadingSpinner";

export default function ShadcnTable() {
  const { data, loading, error, refetch, progress } = useSupabaseProducts();

  const [editing, setEditing] = useState<Product | null>(null);
  const [form, setForm] = useState<Partial<Product>>({});
  const [isNew, setIsNew] = useState(false);
  const [globalFilter, setGlobalFilter] = useState("");

  const columns: ColumnDef<Product>[] = useMemo(
    () => [
      { accessorKey: "id", header: "ID" },
      { accessorKey: "name", header: "Nazwa" },
      { accessorKey: "price", header: "Cena (PLN)" },
      { accessorKey: "category", header: "Kategoria" },
      { accessorKey: "brand", header: "Marka" },
      { accessorKey: "supplier", header: "Dostawca" },
      { accessorKey: "warehouse", header: "Magazyn" },
      { accessorKey: "color", header: "Kolor" },
      { accessorKey: "size", header: "Rozmiar" },
      { accessorKey: "stock", header: "Stan" },
      { accessorKey: "discount", header: "Rabat (%)" },
      { accessorKey: "rating", header: "Ocena" },
      { accessorKey: "active", header: "Aktywny" },
      { accessorKey: "description", header: "Opis" },
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
                  refetch();
                }
              }}
            >
              Usuń
            </Button>
          </div>
        ),
      },
    ],
    [refetch]
  );

  const table = useReactTable({
    data: data ?? [],
    columns,
    state: { globalFilter },
    onGlobalFilterChange: setGlobalFilter,
    getCoreRowModel: getCoreRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
  });

  if (loading)
    return (
      <LoadingSpinner text="Wczytywanie produktów..." progress={progress} />
    );

  if (error)
    return (
      <p className="text-red-600 font-medium p-6">
        Błąd podczas ładowania danych: {error}
      </p>
    );

  const handleSave = async () => {
    if (!form.name || !form.category) {
      alert("Uzupełnij nazwę i kategorię produktu!");
      return;
    }

    if (isNew) {
      await addProduct({
        name: form.name!,
        price: form.price ?? 0,
        category: form.category!,
        description: form.description ?? "",
        sku: crypto.randomUUID(),
        stock: form.stock ?? 100,
        warehouse: form.warehouse ?? "Nowy Sącz",
        brand: form.brand ?? "Generic",
        supplier: form.supplier ?? "Default Supplier",
        discount: form.discount ?? 0,
        rating: form.rating ?? 0,
        active: form.active ?? true,
        color: form.color ?? "czarny",
        size: form.size ?? "M",
      });
    } else if (editing) {
      const updated = { ...editing, ...form } as Product;
      await updateProduct(updated);
    }

    setEditing(null);
    setForm({});
    setIsNew(false);
    refetch();
  };

  return (
    <div className="p-6">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-xl font-semibold">Tabela produktów (Shadcn)</h2>

        <div className="flex gap-2">
          <Input
            placeholder="Szukaj..."
            value={globalFilter ?? ""}
            onChange={(e) => setGlobalFilter(e.target.value)}
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

      <div className="rounded-md border overflow-auto h-[70vh]">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableHead key={header.id} className="whitespace-nowrap">
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
              <TableRow key={row.id} className="hover:bg-muted/40">
                {row.getVisibleCells().map((cell) => (
                  <TableCell
                    key={cell.id}
                    className="align-top max-w-[200px] text-sm"
                  >
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
                  table.getCanPreviousPage() ? table.previousPage() : undefined
                }
                aria-disabled={!table.getCanPreviousPage()}
                className={
                  !table.getCanPreviousPage()
                    ? "opacity-50 pointer-events-none"
                    : ""
                }
              />
            </PaginationItem>

            {(() => {
              const totalPages = table.getPageCount();
              const current = table.getState().pagination.pageIndex;
              const maxVisible = 5;

              const start = Math.max(0, current - Math.floor(maxVisible / 2));
              const end = Math.min(totalPages, start + maxVisible);

              const pages = [];
              if (start > 0) pages.push(<span key="start">...</span>);
              for (let i = start; i < end; i++) {
                pages.push(
                  <PaginationItem key={i}>
                    <PaginationLink
                      onClick={() => table.setPageIndex(i)}
                      isActive={i === current}
                      className={
                        i === current
                          ? "bg-primary text-primary-foreground"
                          : "hover:bg-muted"
                      }
                    >
                      {i + 1}
                    </PaginationLink>
                  </PaginationItem>
                );
              }
              if (end < totalPages) pages.push(<span key="end">...</span>);
              return pages;
            })()}

            <PaginationItem>
              <PaginationNext
                onClick={() =>
                  table.getCanNextPage() ? table.nextPage() : undefined
                }
                aria-disabled={!table.getCanNextPage()}
                className={
                  !table.getCanNextPage()
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
            }).map(([key, label]) => {
              const field = key as keyof Product;
              return (
                <div key={key}>
                  <Label>{label}</Label>
                  <Input
                    type={
                      ["price", "stock", "discount", "rating"].includes(key)
                        ? "number"
                        : "text"
                    }
                    value={
                      typeof form[field] === "boolean"
                        ? form[field]
                          ? "true"
                          : "false"
                        : form[field] ?? ""
                    }
                    onChange={(e) =>
                      setForm({
                        ...form,
                        [field]: [
                          "price",
                          "stock",
                          "discount",
                          "rating",
                        ].includes(key)
                          ? Number(e.target.value)
                          : e.target.value,
                      })
                    }
                  />
                </div>
              );
            })}

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
