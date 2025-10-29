"use client";

import { useMemo, useState } from "react";
import DataGrid, {
  Column,
  Paging,
  Pager,
  SearchPanel,
  Export as DxExport,
  Selection,
  Editing,
  Toolbar,
  Item,
  FilterRow,
  HeaderFilter,
  Sorting,
} from "devextreme-react/data-grid";

import "devextreme/dist/css/dx.light.css";

import { generateProducts, type Product } from "@/lib/mockData";
import { devexpressColumns } from "@/lib/productColumn";

export default function DevExpressGrid() {
  const [data, setData] = useState<Product[]>(generateProducts(1000));

  const handleRowUpdated = (e: any) => {
    const updated = e.data;
    setData((prev) =>
      prev.map((row) => (row.id === updated.id ? { ...row, ...updated } : row))
    );
  };

  const columns = useMemo(() => {
    return devexpressColumns.map((col) => (
      <Column
        key={col.dataField}
        dataField={col.dataField}
        caption={col.caption}
        width={col.width}
        format={col.format}
        allowEditing={true}
      />
    ));
  }, []);

  return (
    <div className="p-6">
      <div
        className="rounded-md border shadow-sm overflow-hidden"
        style={{ height: "75vh", width: "100%" }}
      >
        <DataGrid
          dataSource={data}
          showBorders={true}
          showColumnLines={true}
          showRowLines={false}
          rowAlternationEnabled={true}
          allowColumnReordering={true}
          allowColumnResizing={true}
          columnAutoWidth={true}
          repaintChangesOnly={true}
          onRowUpdated={handleRowUpdated}
          height="100%"
        >
          <SearchPanel visible={true} highlightCaseSensitive={true} />

          <FilterRow visible={true} />
          <HeaderFilter visible={true} />
          <Sorting mode="multiple" />

          <Editing
            mode="cell"
            allowUpdating={true}
            allowAdding={false}
            allowDeleting={false}
          />

          <Paging defaultPageSize={20} />
          <Pager
            visible={true}
            showPageSizeSelector={true}
            allowedPageSizes={[10, 20, 50, 100]}
            showInfo={true}
            showNavigationButtons={true}
          />

          <Toolbar>
            <Item
              location="before"
              widget="dxButton"
              options={{ icon: "refresh" }}
            />
            <Item name="searchPanel" />
            <Item name="exportButton" />
          </Toolbar>

          <DxExport enabled={true} allowExportSelectedData={true} />

          <Selection mode="multiple" />

          {columns}
        </DataGrid>
      </div>
    </div>
  );
}
