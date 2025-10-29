import {
  AllCommunityModule,
  ModuleRegistry,
  type ColDef,
} from "ag-grid-community";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-theme-quartz.css";

import { useMemo } from "react";
import { useSupabaseProducts } from "@/hooks/useSupabaseProducts";
import { agColumns } from "@/lib/productColumn";
import { updateProduct } from "@/lib/updateProduct";

ModuleRegistry.registerModules([AllCommunityModule]);

export default function AgGridView() {
  const { data: rowData, loading, error, refetch } = useSupabaseProducts();

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

  if (loading) return <p>Ładowanie...</p>;
  if (error) return <p>Błąd: {error}</p>;

  return (
    <div className="p-6">
      <div
        className="ag-theme-quartz rounded-lg border shadow-sm"
        style={{ height: "75vh", width: "100%" }}
      >
        <AgGridReact
          rowData={rowData}
          columnDefs={columnDefs}
          defaultColDef={defaultColDef}
          pagination
          paginationPageSize={20}
          animateRows
        />
      </div>
    </div>
  );
}
