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

export default function DevExpressGrid() {
  const apiUrl = "http://localhost:5160/odata/Products";

  const dataSource = new CustomStore({
    key: "Id",
    load: async (loadOptions) => {
      try {
        const params = new URLSearchParams();

        // --- Paginacja
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
          headers: {
            Accept: "application/json;odata.metadata=minimal",
          },
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
      <div className="w-full max-w-7xl bg-white shadow-lg rounded-2xl p-6 border border-gray-200">
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
          </Toolbar>

          <Column dataField="Id" caption="ID" width={70} />
          <Column dataField="Name" caption="Nazwa produktu" />
          <Column dataField="Category" caption="Kategoria" />
          <Column
            dataField="Price"
            caption="Cena"
            dataType="number"
            format="#,##0.00 zł"
          />
          <Column dataField="Stock" caption="Stan" dataType="number" />
          <Column dataField="Discount" caption="Zniżka (%)" dataType="number" />
          <Column dataField="Brand" caption="Marka" />
          <Column dataField="Supplier" caption="Dostawca" />
          <Column dataField="Warehouse" caption="Magazyn" />
          <Column dataField="Color" caption="Kolor" />
          <Column dataField="Size" caption="Rozmiar" />
          <Column dataField="Rating" caption="Ocena" dataType="number" />
          <Column dataField="Active" caption="Aktywny" dataType="boolean" />
          <Column dataField="CreatedAt" caption="Utworzono" dataType="date" />
          <Column
            dataField="UpdatedAt"
            caption="Zaktualizowano"
            dataType="date"
          />
          <Column dataField="Description" caption="Opis" width={250} />
        </DataGrid>
      </div>
    </div>
  );
}
