"use client";

import { useState } from "react";
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
  ColumnChooser,
  RequiredRule,
  PatternRule,
  AsyncRule,
} from "devextreme-react/data-grid";
import CustomStore from "devextreme/data/custom_store";

export default function DevExpressGrid() {
  const apiUrl = "http://localhost:5135/odata/Products";
  const [wrapEnabled, setWrapEnabled] = useState(false);

  const asyncValidation = async (params: any) => {
    try {
      const res = await fetch(`http://localhost:5135/api/CheckNameUnique`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          id: params.data?.Id,
          name: params.value,
        }),
      });
      const result = await res.json();
      return result?.isUnique ?? true;
    } catch {
      return true;
    }
  };

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

        // Sortowanie
        if (Array.isArray(loadOptions.sort) && loadOptions.sort.length > 0) {
          const sort = loadOptions.sort[0];
          if (typeof sort === "object" && sort.selector) {
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

        // Filtrowanie
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
    <div className="min-h-screen flex flex-col items-center py-10">
      <div className="w-full bg-white shadow-lg rounded-2xl p-6 border border-gray-200">
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
          width="100%"
          columnAutoWidth
          allowColumnResizing
          allowColumnReordering
          wordWrapEnabled={wrapEnabled}
        >
          <FilterRow visible applyFilter="auto" />
          <HeaderFilter visible />
          <FilterPanel visible />

          <Editing
            mode="batch"
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
          />

          <Toolbar>
            <ToolbarItem name="addRowButton" />
            <ToolbarItem name="saveButton" />
            <ToolbarItem name="revertButton" />
            <ToolbarItem name="searchPanel" />
            <ToolbarItem name="columnChooserButton" />
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

          <ColumnChooser enabled={true} mode="select" />

          <Column dataField="Id" caption="ID" width={70} allowEditing={false} />

          <Column dataField="Name" caption="Nazwa produktu" width={180}>
            <RequiredRule message="Nazwa produktu jest wymagana" />
            <AsyncRule
              message="Produkt o tej nazwie już istnieje"
              validationCallback={asyncValidation}
            />
          </Column>

          <Column dataField="Category" caption="Kategoria" width={130}>
            <RequiredRule message="Podaj kategorię" />
          </Column>

          <Column
            dataField="Price"
            caption="Cena"
            dataType="number"
            format="#,##0.00 zł"
            width={100}
          >
            <RequiredRule message="Cena jest wymagana" />
            <PatternRule
              pattern={/^[0-9]+(\.[0-9]{1,2})?$/}
              message="Podaj poprawną liczbę"
            />
          </Column>

          <Column dataField="Stock" caption="Stan" dataType="number" width={90}>
            <PatternRule pattern={/^\d+$/} message="Stan musi być liczbą" />
          </Column>

          <Column
            dataField="Discount"
            caption="Zniżka (%)"
            dataType="number"
            width={120}
          >
            <PatternRule
              pattern={/^(?:[0-9]|[1-9][0-9]|100)$/}
              message="Zniżka musi być w zakresie 0–100"
            />
          </Column>

          <Column dataField="Brand" caption="Marka" width={110}>
            <RequiredRule message="Podaj markę" />
          </Column>

          <Column dataField="Supplier" caption="Dostawca" width={130} />
          <Column dataField="Warehouse" caption="Magazyn" width={130} />
          <Column dataField="Color" caption="Kolor" width={100} />
          <Column dataField="Size" caption="Rozmiar" width={100} />

          <Column
            dataField="Rating"
            caption="Ocena"
            dataType="number"
            width={90}
          >
            <PatternRule
              pattern={/^[0-5](\.\d{1})?$/}
              message="Ocena 0–5 z dokładnością do 0.1"
            />
          </Column>

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
            allowEditing={false}
          />

          <Column
            dataField="UpdatedAt"
            caption="Zaktualizowano"
            dataType="date"
            width={170}
            allowEditing={false}
          />

          <Column dataField="Description" caption="Opis" minWidth={150}>
            <PatternRule
              pattern={/^.{0,500}$/}
              message="Opis nie może przekraczać 500 znaków"
            />
          </Column>
        </DataGrid>
      </div>
    </div>
  );
}
