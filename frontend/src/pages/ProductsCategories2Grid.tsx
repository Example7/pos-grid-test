"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function ProductsCategories2Grid() {
  const apiUrl =
    "http://localhost:5135/odata/ProductsCategories2?$expand=ProductCategory1,Products";

  const categories1Store = new CustomStore({
    key: "ProductCategory1Id",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/ProductsCategories1"
      );
      if (!res.ok) throw new Error("Błąd ładowania kategorii głównych");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Podkategorie produktów"
      keyExpr="ProductCategory2Id"
      columns={
        <>
          <Column
            dataField="ProductCategory2Id"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column
            dataField="Category2Name"
            caption="Nazwa podkategorii"
            width={250}
          >
            <RequiredRule message="Podaj nazwę podkategorii" />
            <StringLengthRule min={2} max={100} message="Od 2 do 100 znaków" />
          </Column>

          <Column
            dataField="ProductCategory1Id"
            caption="Kategoria główna"
            lookup={{
              dataSource: categories1Store,
              valueExpr: "ProductCategory1Id",
              displayExpr: "Category1Name",
            }}
            width={200}
          >
            <RequiredRule message="Wybierz kategorię główną" />
          </Column>

          <Column
            caption="Liczba produktów"
            calculateCellValue={(data: any) => data.Products?.length ?? 0}
            allowEditing={false}
            width={180}
          />

          <Column
            dataField="CreatedAt"
            caption="Utworzono"
            dataType="date"
            allowEditing={false}
            width={160}
          />
        </>
      }
    />
  );
}
