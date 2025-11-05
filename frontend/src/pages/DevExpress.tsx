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
} from "devextreme-react/data-grid";
import CustomStore from "devextreme/data/custom_store";

type DevExpressGridProps = {
  apiUrl: string;
  readUrl?: string; // ← nowy parametr
  title: string;
  keyExpr?: string;
  columns: ReactNode;
};

export default function DevExpressGrid({
  apiUrl,
  readUrl,
  title,
  keyExpr = "id",
  columns,
}: DevExpressGridProps) {
  const [wrapEnabled, setWrapEnabled] = useState(false);

  const store = new CustomStore({
    key: keyExpr,

    load: async (loadOptions: any) => {
      try {
        const url = readUrl || apiUrl;
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

        // Filtrowanie (proste)
        if (loadOptions.filter) {
          const buildFilter = (filter: any): string => {
            if (Array.isArray(filter[0])) {
              return (filter as any[])
                .map((f: any) =>
                  Array.isArray(f)
                    ? buildFilter(f)
                    : f === "and" || f === "or"
                    ? f
                    : ""
                )
                .join(" ");
            }

            const [field, op, val] = filter;
            const value =
              typeof val === "string" ? `'${val.replace(/'/g, "''")}'` : val;
            switch (op) {
              case "=":
                return `${field} eq ${value}`;
              case "<>":
                return `${field} ne ${value}`;
              case ">":
                return `${field} gt ${value}`;
              case "<":
                return `${field} lt ${value}`;
              case ">=":
                return `${field} ge ${value}`;
              case "<=":
                return `${field} le ${value}`;
              case "contains":
                return `contains(${field}, ${value})`;
              case "startswith":
                return `startswith(${field}, ${value})`;
              case "endswith":
                return `endswith(${field}, ${value})`;
              default:
                return "";
            }
          };
          const filterQuery = buildFilter(loadOptions.filter);
          if (filterQuery) params.append("$filter", filterQuery);
        }

        const separator = url.includes("?") ? "&" : "?";
        const res = await fetch(`${url}${separator}${params.toString()}`, {
          headers: { Accept: "application/json;odata.metadata=minimal" },
        });
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        const json = await res.json();

        return {
          data: json.value ?? json,
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
    },

    remove: async (key) => {
      const res = await fetch(`${apiUrl}(${key})`, { method: "DELETE" });
      if (!res.ok) throw new Error(`Błąd usuwania: ${res.status}`);
    },
  });

  return (
    <div className="min-h-screen flex flex-col items-center py-10">
      <div className="w-full bg-white shadow-lg rounded-2xl p-6 border border-gray-200">
        <h2 className="text-2xl font-semibold mb-4">{title}</h2>

        <DataGrid
          dataSource={store}
          remoteOperations={{ paging: true, sorting: true, filtering: true }}
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
            visible
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
        </DataGrid>
      </div>
    </div>
  );
}
