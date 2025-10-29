import { useState, useMemo } from "react";
import {
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";
import {
  Table,
  TableHeader,
  TableHead,
  TableRow,
  TableBody,
  TableCell,
} from "@/components/ui/table";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import { generateProducts } from "@/lib/mockData";
import { tanstackColumns } from "@/lib/productColumn";

const allData = generateProducts(1000);

export default function ShadcnTable() {
  const [pageIndex, setPageIndex] = useState(0);
  const pageSize = 20;

  const pageData = useMemo(() => {
    const start = pageIndex * pageSize;
    return allData.slice(start, start + pageSize);
  }, [pageIndex]);

  const table = useReactTable({
    data: pageData,
    columns: tanstackColumns,
    getCoreRowModel: getCoreRowModel(),
  });

  const totalPages = Math.ceil(allData.length / pageSize);

  return (
    <div className="p-6">
      <div className="rounded-md border overflow-auto h-[70vh] bg-white shadow-sm">
        <Table>
          <TableHeader className="bg-gray-100 sticky top-0 z-10">
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableHead
                    key={header.id}
                    className="text-gray-700 font-semibold"
                  >
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
              <TableRow key={row.id} className="hover:bg-gray-50">
                {row.getVisibleCells().map((cell) => (
                  <TableCell key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
            ))}

            {table.getRowModel().rows.length === 0 && (
              <TableRow>
                <TableCell
                  colSpan={tanstackColumns.length}
                  className="text-center py-6"
                >
                  Brak danych do wyświetlenia
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>

      {/* Paginacja */}
      <div className="flex justify-center mt-6">
        <Pagination>
          <PaginationContent>
            <PaginationItem>
              <PaginationPrevious
                role="button"
                onClick={() => setPageIndex((p) => Math.max(p - 1, 0))}
              />
            </PaginationItem>

            {Array.from({ length: totalPages })
              .slice(
                Math.max(0, pageIndex - 2),
                Math.min(totalPages, pageIndex + 3)
              )
              .map((_, i) => {
                const actualPage = Math.max(0, pageIndex - 2) + i;
                return (
                  <PaginationItem key={actualPage}>
                    <PaginationLink
                      role="button"
                      isActive={actualPage === pageIndex}
                      onClick={() => setPageIndex(actualPage)}
                    >
                      {actualPage + 1}
                    </PaginationLink>
                  </PaginationItem>
                );
              })}

            <PaginationItem>
              <PaginationNext
                role="button"
                onClick={() =>
                  setPageIndex((p) => Math.min(p + 1, totalPages - 1))
                }
              />
            </PaginationItem>
          </PaginationContent>
        </Pagination>
      </div>

      <p className="text-sm text-gray-500 text-center mt-2">
        Strona {pageIndex + 1} z {totalPages} ({allData.length} rekordów)
      </p>
    </div>
  );
}
