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

export default function ShadcnTable() {
  const { data, loading, error, refetch } = useSupabaseProducts();
  const [editing, setEditing] = useState<Product | null>(null);
  const [form, setForm] = useState<Partial<Product>>({});
  const [isNew, setIsNew] = useState(false);
  const [globalFilter, setGlobalFilter] = useState("");

  const columns: ColumnDef<Product>[] = useMemo(
    () => [
      { accessorKey: "id", header: "ID" },
      { accessorKey: "name", header: "Nazwa produktu" },
      { accessorKey: "price", header: "Cena (PLN)" },
      { accessorKey: "category", header: "Kategoria" },
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
                }
              }}
            >
              Usuń
            </Button>
          </div>
        ),
      },
    ],
    []
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

  if (loading) return <p>Ładowanie...</p>;
  if (error) return <p>Błąd: {error}</p>;

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
        <h2 className="text-xl font-semibold">Tabela Shadcn (Supabase)</h2>
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
                  <TableCell key={cell.id}>
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
          <PaginationContent className="flex items-center gap-1">
            <PaginationItem>
              <PaginationPrevious
                onClick={() =>
                  table.getCanPreviousPage() ? table.previousPage() : undefined
                }
                aria-disabled={!table.getCanPreviousPage()}
                className={`${
                  !table.getCanPreviousPage()
                    ? "opacity-50 pointer-events-none"
                    : ""
                }`}
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
                className={`${
                  !table.getCanNextPage()
                    ? "opacity-50 pointer-events-none"
                    : ""
                }`}
              />
            </PaginationItem>
          </PaginationContent>
        </Pagination>
      </div>

      <Dialog open={!!editing} onOpenChange={() => setEditing(null)}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>
              {isNew ? "Dodaj nowy produkt" : "Edytuj produkt"}
            </DialogTitle>
          </DialogHeader>

          <div className="grid gap-4 py-2">
            <div>
              <Label>Nazwa</Label>
              <Input
                value={form.name ?? ""}
                onChange={(e) => setForm({ ...form, name: e.target.value })}
              />
            </div>
            <div>
              <Label>Cena</Label>
              <Input
                type="number"
                value={form.price ?? ""}
                onChange={(e) =>
                  setForm({ ...form, price: Number(e.target.value) })
                }
              />
            </div>
            <div>
              <Label>Kategoria</Label>
              <Input
                value={form.category ?? ""}
                onChange={(e) => setForm({ ...form, category: e.target.value })}
              />
            </div>
            <div>
              <Label>Opis</Label>
              <Input
                value={form.description ?? ""}
                onChange={(e) =>
                  setForm({ ...form, description: e.target.value })
                }
              />
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
