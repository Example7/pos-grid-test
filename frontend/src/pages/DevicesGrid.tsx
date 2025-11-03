"use client";

import DataGrid, {
  Column,
  Paging,
  Pager,
  Editing,
  Toolbar,
  Item as ToolbarItem,
  FilterRow,
} from "devextreme-react/data-grid";
import CustomStore from "devextreme/data/custom_store";
import "devextreme/dist/css/dx.material.blue.light.css";

export default function DevicesGrid() {
  const apiUrl = "http://localhost:5135/odata/Devices";

  const dataSource = new CustomStore({
    key: "Id",
    load: async () => {
      const res = await fetch(apiUrl);
      const json = await res.json();
      return { data: json.value, totalCount: json["@odata.count"] ?? 0 };
    },
    insert: async (values) => {
      const res = await fetch(apiUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(values),
      });
      return await res.json();
    },
    update: async (key, values) => {
      const res = await fetch(`${apiUrl}(${key})`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(values),
      });
      return await res.json();
    },
    remove: async (key) => {
      await fetch(`${apiUrl}(${key})`, { method: "DELETE" });
    },
  });

  return (
    <div className="min-h-screen flex flex-col items-center py-10">
      <div className="w-full max-w-6xl bg-white shadow-lg rounded-2xl p-6 border border-gray-200">
        <h2 className="text-2xl font-semibold mb-4">Devices</h2>
        <DataGrid
          dataSource={dataSource}
          keyExpr="Id"
          showBorders
          rowAlternationEnabled
          columnAutoWidth
        >
          <FilterRow visible applyFilter="auto" />
          <Editing
            mode="row"
            allowAdding
            allowUpdating
            allowDeleting
            useIcons
          />

          <Paging defaultPageSize={20} />
          <Pager showPageSizeSelector showInfo />

          <Toolbar>
            <ToolbarItem name="addRowButton" />
            <ToolbarItem name="searchPanel" />
          </Toolbar>

          <Column dataField="Id" caption="ID" width={70} allowEditing={false} />
          <Column dataField="Name" caption="Device Name" />
          <Column dataField="PosId" caption="POS ID" />
          <Column dataField="Location" caption="Location" />
          <Column dataField="Active" caption="Active" dataType="boolean" />
          <Column dataField="CreatedAt" caption="Created At" dataType="date" />
          <Column dataField="UpdatedAt" caption="Updated At" dataType="date" />
        </DataGrid>
      </div>
    </div>
  );
}
