"use client";

import React, { useState } from "react";
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

export default function DevExpressGrid() {
  const apiUrl = "http://localhost:5135/odata/Products";
  const [wrapEnabled, setWrapEnabled] = useState(false);

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

        // --- Sortowanie
        if (Array.isArray(loadOptions.sort) && loadOptions.sort.length > 0) {
          const sort = loadOptions.sort[0];
          if (typeof sort === "string") {
            params.append("$orderby", sort);
          } else if (typeof sort === "object" && sort.selector) {
            const selector =
              typeof sort.selector === "string"
                ? sort.selector
                : String(sort.selector);
            params.append(
              "$orderby",
              `${selector} ${sort.desc ? "desc" : "asc"}`
            );
          }
        }

        // --- Filtrowanie
        if (loadOptions.filter) {
          type DevExtremeFilter =
            | [string, string, string | number | boolean]
            | (string | DevExtremeFilter)[];

          const buildFilter = (filter: DevExtremeFilter): string => {
            if (Array.isArray(filter[0])) {
              return (filter as (string | DevExtremeFilter)[])
                .map((f) =>
                  Array.isArray(f)
                    ? buildFilter(f)
                    : f === "and" || f === "or"
                    ? f
                    : ""
                )
                .join(" ");
            }

            const [field, operator, value] = filter;
            const val =
              typeof value === "string"
                ? `'${value.replace(/'/g, "''")}'`
                : value;

            switch (operator) {
              case "=":
                return `${field} eq ${val}`;
              case "<>":
                return `${field} ne ${val}`;
              case ">":
                return `${field} gt ${val}`;
              case "<":
                return `${field} lt ${val}`;
              case ">=":
                return `${field} ge ${val}`;
              case "<=":
                return `${field} le ${val}`;
              case "contains":
                return `contains(${field}, ${val})`;
              case "notcontains":
                return `not contains(${field}, ${val})`;
              case "startswith":
                return `startswith(${field}, ${val})`;
              case "endswith":
                return `endswith(${field}, ${val})`;
              default:
                return "";
            }
          };

          const filterQuery = buildFilter(loadOptions.filter);
          if (filterQuery) params.append("$filter", filterQuery);
        }

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
    <div className="flex flex-col items-center py-10">
      <div className="bg-white shadow-lg rounded-2xl p-6 border border-gray-200">
        <h2 className="text-2xl font-semibold mb-4">Products</h2>

        <DataGrid
          dataSource={dataSource}
          remoteOperations={{ paging: true, sorting: true, filtering: true }}
          keyExpr="Id"
          showBorders
          rowAlternationEnabled
          hoverStateEnabled
          repaintChangesOnly
          height="700px"
          columnAutoWidth={false}
          allowColumnResizing={true}
          columnResizingMode="widget"
          wordWrapEnabled={wrapEnabled}
          width="100%"
          scrolling={{ mode: "standard", showScrollbar: "always" }}
        >
          <FilterRow visible applyFilter="auto" />
          <HeaderFilter visible />
          <FilterPanel visible />
          <FilterBuilderPopup
            position={{ of: window, at: "top", my: "top", offset: { y: 10 } }}
          />

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
            showNavigationButtons
            showInfo
            infoText="Strona {0} z {1} ({2} rekordów)"
          />

          <Toolbar>
            <ToolbarItem name="addRowButton" />
            <ToolbarItem name="searchPanel" />
            <ToolbarItem
              widget="dxButton"
              options={{
                text: wrapEnabled ? "Wyłącz zawijanie" : "Włącz zawijanie",
                icon: wrapEnabled ? "collapse" : "expand",
                hint: "Przełącz zawijanie tekstu",
                onClick: () => setWrapEnabled(!wrapEnabled),
              }}
              location="after"
            />
          </Toolbar>

          <Column dataField="Id" caption="ID" width={70} />
          <Column dataField="Name" caption="Nazwa produktu" width={180} />
          <Column dataField="Category" caption="Kategoria" width={130} />
          <Column
            dataField="Price"
            caption="Cena"
            dataType="number"
            format="#,##0.00 zł"
            width={100}
          />
          <Column
            dataField="Stock"
            caption="Stan"
            dataType="number"
            width={90}
          />
          <Column
            dataField="Discount"
            caption="Zniżka (%)"
            dataType="number"
            width={120}
          />
          <Column dataField="Brand" caption="Marka" width={110} />
          <Column dataField="Supplier" caption="Dostawca" width={130} />
          <Column dataField="Warehouse" caption="Magazyn" width={130} />
          <Column dataField="Color" caption="Kolor" width={100} />
          <Column dataField="Size" caption="Rozmiar" width={100} />
          <Column
            dataField="Rating"
            caption="Ocena"
            dataType="number"
            width={90}
          />
          <Column
            dataField="Active"
            caption="Aktywny"
            dataType="boolean"
            width={120}
          />
          <Column
            dataField="CreatedAt"
            caption="Utworzono"
            dataType="date"
            width={130}
          />
          <Column
            dataField="UpdatedAt"
            caption="Zaktualizowano"
            dataType="date"
            width={170}
          />
          <Column dataField="Description" caption="Opis" minWidth={150} />
        </DataGrid>
      </div>
    </div>
  );
}
