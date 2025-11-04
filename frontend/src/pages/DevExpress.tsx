"use client";

import { useState, type ReactNode } from "react";
import DataGrid, {
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

type DevExpressGridProps = {
  apiUrl: string;
  title: string;
  keyExpr?: string;
  columns: ReactNode;
  enableValidation?: boolean;
};

export default function DevExpressGrid({
  apiUrl,
  title,
  keyExpr = "id",
  columns,
  enableValidation = false,
}: DevExpressGridProps) {
  const [wrapEnabled, setWrapEnabled] = useState(false);

  const asyncValidation = async (params: any) => {
    try {
      const res = await fetch(
        `${apiUrl.replace("/odata/", "/api/")}CheckNameUnique`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            id: params.data?.[keyExpr],
            name: params.value,
          }),
        }
      );
      const result = await res.json();
      return result?.isUnique ?? true;
    } catch {
      return true;
    }
  };

  const dataSource = new CustomStore({
    key: keyExpr,
    load: async (loadOptions: any) => {
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
          data: json.value ?? json,
          totalCount: json["@odata.count"] ?? 0,
        };
      } catch (error) {
        console.error("Błąd ładowania danych:", error);
        throw error;
      }
    },
    insert: async (values: any) => {
      const res = await fetch(apiUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(values),
      });
      if (!res.ok) throw new Error(`Błąd dodawania: ${res.status}`);
      return await res.json();
    },
    update: async (key: any, values: any) => {
      const res = await fetch(`${apiUrl}(${key})`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(values),
      });
      if (!res.ok) throw new Error(`Błąd aktualizacji: ${res.status}`);
      return await res.json();
    },
    remove: async (key: any) => {
      const res = await fetch(`${apiUrl}(${key})`, { method: "DELETE" });
      if (!res.ok) throw new Error(`Błąd usuwania: ${res.status}`);
    },
  });

  return (
    <div className="min-h-screen flex flex-col items-center py-10">
      <div className="w-full bg-white shadow-lg rounded-2xl p-6 border border-gray-200">
        <h2 className="text-2xl font-semibold mb-4">{title}</h2>

        <DataGrid
          dataSource={dataSource}
          remoteOperations={{ paging: true, sorting: true, filtering: true }}
          keyExpr={keyExpr}
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
            allowAdding
            allowUpdating
            allowDeleting
            useIcons
          />

          <Paging defaultPageSize={20} />
          <Pager
            visible={true}
            showPageSizeSelector
            allowedPageSizes={[5, 10, 20, 50, 100]}
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

          <ColumnChooser enabled mode="select" />

          {columns}

          {enableValidation && (
            <>
              <RequiredRule message="To pole jest wymagane" />
              <PatternRule
                pattern={/^[0-9]+(\.[0-9]{1,2})?$/}
                message="Podaj poprawną liczbę"
              />
              <AsyncRule
                message="Wartość nie jest unikalna"
                validationCallback={asyncValidation}
              />
            </>
          )}
        </DataGrid>
      </div>
    </div>
  );
}
