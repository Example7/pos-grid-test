import { useMemo, useState } from "react";
import {
  AllCommunityModule,
  ModuleRegistry,
  type ColDef,
  type ICellRendererParams,
} from "ag-grid-community";
import { AgGridReact } from "ag-grid-react";

import "ag-grid-community/styles/ag-theme-quartz.css";

import { generateProducts, type Product } from "@/lib/mockData";
import { agColumns } from "@/lib/productColumn";

ModuleRegistry.registerModules([AllCommunityModule]);

const CategoryRenderer = (params: ICellRendererParams<Product>) => {
  const category = params.value as string;

  return (
    <span
      style={{
        display: "flex",
        alignItems: "center",
        height: "100%",
        width: "100%",
        fontWeight: 500,
        color: "#333",
      }}
    >
      <span
        style={{
          overflow: "hidden",
          whiteSpace: "nowrap",
          textOverflow: "ellipsis",
        }}
      >
        {category}
      </span>
    </span>
  );
};

const priceFormatter = (params: any) =>
  `${params.value.toLocaleString("pl-PL")} zł`;

export default function AgGridView() {
  const [rowData] = useState<Product[]>(generateProducts(1000));

  const columnDefs = useMemo<ColDef[]>(() => {
    return agColumns.map((col) => {
      if (col.field === "category")
        return { ...col, cellRenderer: CategoryRenderer };
      if (col.field === "price")
        return { ...col, valueFormatter: priceFormatter };
      return col;
    });
  }, []);

  const defaultColDef = useMemo<ColDef>(
    () => ({
      sortable: true,
      filter: true,
      editable: true,
      floatingFilter: true,
      resizable: true,
      flex: 1,
      minWidth: 130,
    }),
    []
  );

  const rowSelection: "multiple" = "multiple";

  return (
    <div className="p-6">
      <div
        className="ag-theme-quartz rounded-lg border shadow-sm"
        style={{
          height: "75vh",
          width: "100%",
          background: "#fff",
        }}
      >
        <AgGridReact
          rowData={rowData}
          columnDefs={columnDefs}
          defaultColDef={defaultColDef}
          pagination={true}
          paginationPageSize={20}
          animateRows={true}
          rowSelection={rowSelection}
          enableCellTextSelection={true}
          suppressRowClickSelection={true}
          onCellValueChanged={(e) =>
            console.log(
              `Zmieniono wartość w kolumnie '${e.colDef.field}':`,
              e.value
            )
          }
        />
      </div>
    </div>
  );
}
