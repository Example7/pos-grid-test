"use client";

import DataGrid, {
  Column,
  Paging,
  Pager,
  Editing,
  Toolbar,
  Item as ToolbarItem,
  FilterRow,
  HeaderFilter,
  FilterPanel,
  FilterBuilderPopup,
} from "devextreme-react/data-grid";
import CustomStore from "devextreme/data/custom_store";
import "devextreme/dist/css/dx.material.blue.light.css";

export default function ShopsGrid() {
  const apiUrl = "http://localhost:5135/odata/Shops";

  const dataSource = new CustomStore({
    key: "Id",
    load: async (loadOptions) => {
      try {
        const params = new URLSearchParams();
        const skip = loadOptions.skip ?? 0;
        const top = loadOptions.take ?? 20;
        params.append("$skip", String(skip));
        params.append("$top", String(top));
        params.append("$count", "true");

        const response = await fetch(`${apiUrl}?${params.toString()}`, {
          headers: { Accept: "application/json;odata.metadata=minimal" },
        });

        if (!response.ok) throw new Error(`HTTP ${response.status}`);
        const json = await response.json();
        return {
          data: json.value,
          totalCount: json["@odata.count"] ?? 0,
        };
      } catch (error) {
        console.error("Błąd ładowania danych:", error);
        throw error;
      }
    },
    insert: async (values) => {
      const res = await fetch(apiUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(values),
      });
      if (!res.ok) throw new Error(`Błąd dodawania: ${res.status}`);
      return await res.json();
    },
    update: async (key, values) => {
      const res = await fetch(`${apiUrl}(${key})`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(values),
      });
      if (!res.ok) throw new Error(`Błąd aktualizacji: ${res.status}`);
      return await res.json();
    },
    remove: async (key) => {
      const res = await fetch(`${apiUrl}(${key})`, { method: "DELETE" });
      if (!res.ok) throw new Error(`Błąd usuwania: ${res.status}`);
    },
  });

  return (
    <div className="min-h-screen flex flex-col items-center py-10">
      <div className="w-full max-w-6xl bg-white shadow-lg rounded-2xl p-6 border border-gray-200">
        <h2 className="text-2xl font-semibold mb-4">Shops</h2>
        <DataGrid
          dataSource={dataSource}
          remoteOperations={{ paging: true, sorting: true, filtering: true }}
          keyExpr="Id"
          showBorders
          rowAlternationEnabled
          hoverStateEnabled
          columnAutoWidth
          repaintChangesOnly
          height="700px"
        >
          <FilterRow visible applyFilter="auto" />
          <HeaderFilter visible />
          <FilterPanel visible />
          <FilterBuilderPopup position={{ of: window, at: "top", my: "top" }} />

          <Editing
            mode="row"
            allowUpdating
            allowAdding
            allowDeleting
            useIcons
          />

          <Paging defaultPageSize={20} />
          <Pager
            showPageSizeSelector
            allowedPageSizes={[10, 20, 50, 100]}
            showInfo
          />

          <Toolbar>
            <ToolbarItem name="addRowButton" />
            <ToolbarItem name="searchPanel" />
          </Toolbar>

          <Column dataField="Id" caption="ID" width={70} allowEditing={false} />
          <Column dataField="Name" caption="Shop Name" />
          <Column dataField="Location" caption="Location" />
          <Column dataField="Active" caption="Active" dataType="boolean" />
          <Column dataField="CreatedAt" caption="Created At" dataType="date" />
          <Column dataField="UpdatedAt" caption="Updated At" dataType="date" />
        </DataGrid>
      </div>
    </div>
  );
}
