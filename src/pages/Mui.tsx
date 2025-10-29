import * as React from "react";
import {
  DataGrid,
  GridToolbar,
  GridRowEditStopReasons,
  type GridEventListener,
  type GridRowModel,
} from "@mui/x-data-grid";
import { generateProducts, type Product } from "@/lib/mockData";
import { muiColumns } from "@/lib/productColumn";

const initialRows: Product[] = generateProducts(1000);

export default function MuiGridView() {
  const [rows, setRows] = React.useState<Product[]>(initialRows);
  const [paginationModel, setPaginationModel] = React.useState({
    pageSize: 10,
    page: 0,
  });

  const handleRowEditStop: GridEventListener<"rowEditStop"> = (
    params,
    event
  ) => {
    if (params.reason === GridRowEditStopReasons.rowFocusOut) {
      event.defaultMuiPrevented = true;
    }
  };

  const handleProcessRowUpdate = (newRow: GridRowModel<Product>) => {
    const updatedRow = newRow as Product;
    setRows((prev) =>
      prev.map((r) => (r.id === updatedRow.id ? updatedRow : r))
    );
    return updatedRow;
  };

  return (
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
        columns={muiColumns}
        pagination
        paginationModel={paginationModel}
        onPaginationModelChange={setPaginationModel}
        pageSizeOptions={[10, 25, 50, 100]}
        editMode="row"
        onRowEditStop={handleRowEditStop}
        processRowUpdate={handleProcessRowUpdate}
        slots={{ toolbar: GridToolbar }}
        slotProps={{
          toolbar: {
            showQuickFilter: true,
            quickFilterProps: { debounceMs: 500 },
          },
        }}
        disableRowSelectionOnClick
        checkboxSelection
        sx={{
          border: "none",
          "& .MuiDataGrid-columnHeaders": {
            backgroundColor: "#f3f4f6",
            fontWeight: "bold",
          },
          "& .MuiDataGrid-cell": {
            fontSize: "0.95rem",
          },
          "& .MuiDataGrid-row:hover": {
            backgroundColor: "#f9fafb",
          },
        }}
      />
    </div>
  );
}
