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
import type { LoadOptions, SortDescriptor } from "devextreme/data";
import "devextreme/dist/css/dx.material.blue.light.css";

import { useState } from "react";
import { supabase } from "@/lib/supabaseClient";
import { devexpressColumns } from "@/lib/productColumn";
import type { Product } from "@/lib/mockData";
import CustomStore from "devextreme/data/custom_store";

type SimpleFilter =
  | [
      keyof Product,
      "=" | "<" | ">" | "<=" | ">=" | "contains",
      string | number | boolean
    ]
  | [SimpleFilter, "and" | "or", SimpleFilter];

export default function DevExpressGrid() {
  const [error, setError] = useState<string | null>(null);

  const defaultFilterValue: SimpleFilter = ["active", "=", true];

  const dataSource = new CustomStore<Product, number>({
    key: "id",

    load: async (loadOptions: LoadOptions) => {
      try {
        const pageSize = loadOptions.take ?? 20;
        const from = loadOptions.skip ?? 0;
        const to = from + pageSize - 1;

        let query = supabase
          .from("products")
          .select("*", { count: "exact" })
          .range(from, to);

        if (Array.isArray(loadOptions.sort) && loadOptions.sort.length > 0) {
          const sortItem = loadOptions.sort[0] as SortDescriptor<Product>;
          if (typeof sortItem === "string") {
            query = query.order(sortItem, { ascending: true });
          } else if (
            typeof sortItem === "object" &&
            sortItem.selector &&
            typeof sortItem.selector === "string"
          ) {
            query = query.order(sortItem.selector, {
              ascending: sortItem.desc ? false : true,
            });
          }
        }

        const applyFilter = (filter: unknown): void => {
          if (
            Array.isArray(filter) &&
            filter.length === 3 &&
            typeof filter[0] === "string"
          ) {
            const [field, operator, value] = filter as [
              keyof Product,
              string,
              string | number | boolean
            ];

            if (value !== undefined) {
              switch (operator) {
                case "contains":
                  query = query.ilike(field as string, `%${value}%`);
                  break;
                case "=":
                  query = query.eq(field as string, value);
                  break;
                case "<":
                  query = query.lt(field as string, value as number | string);
                  break;
                case ">":
                  query = query.gt(field as string, value as number | string);
                  break;
                case "<=":
                  query = query.lte(field as string, value as number | string);
                  break;
                case ">=":
                  query = query.gte(field as string, value as number | string);
                  break;
              }
            }
          } else if (Array.isArray(filter)) {
            for (const part of filter) {
              if (Array.isArray(part)) applyFilter(part);
            }
          }
        };

        if (Array.isArray(loadOptions.filter)) applyFilter(loadOptions.filter);

        const { data, count, error } = await query;
        if (error) throw error;

        setError(null);
        return { data: data ?? [], totalCount: count ?? 0 };
      } catch (err) {
        console.error("Błąd ładowania danych:", err);
        setError((err as Error).message);
        throw err;
      }
    },

    insert: async (values): Promise<Product> => {
      const { data, error } = await supabase
        .from("products")
        .insert(values)
        .select()
        .single();
      if (error) throw error;
      return data as Product;
    },

    update: async (key, values): Promise<Product> => {
      const { data, error } = await supabase
        .from("products")
        .update(values)
        .eq("id", key)
        .select()
        .single();
      if (error) throw error;
      return data as Product;
    },

    remove: async (key): Promise<void> => {
      const { error } = await supabase.from("products").delete().eq("id", key);
      if (error) throw error;
    },
  });

  if (error)
    return (
      <div className="p-6 text-red-600 font-medium">
        Błąd podczas wczytywania danych: {error}
      </div>
    );

  return (
    <div className="p-6">
      <DataGrid
        dataSource={dataSource}
        remoteOperations={{ paging: true, sorting: true, filtering: true }}
        keyExpr="id"
        showBorders
        rowAlternationEnabled
        hoverStateEnabled
        columnAutoWidth
        repaintChangesOnly
        defaultFilterValue={defaultFilterValue}
      >
        <FilterRow visible applyFilter="auto" />
        <HeaderFilter visible />
        <FilterPanel visible />
        <FilterBuilderPopup
          position={{ of: window, at: "top", my: "top", offset: { y: 10 } }}
        />

        <Editing mode="row" allowUpdating allowAdding allowDeleting useIcons />

        <Paging defaultPageSize={10} />
        <Pager
          showPageSizeSelector
          allowedPageSizes={[10, 20, 50, 100]}
          showNavigationButtons
          showInfo
          infoText="Strona {0} z {1} ({2} rekordów)"
        />

        <Toolbar>
          <ToolbarItem name="addRowButton" />
          <ToolbarItem name="exportButton" />
        </Toolbar>

        {devexpressColumns.map((col) => (
          <Column key={col.dataField} {...col} />
        ))}
      </DataGrid>
    </div>
  );
}
