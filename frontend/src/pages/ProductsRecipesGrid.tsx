"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function ProductsRecipesGrid() {
  const apiUrl = "http://localhost:5135/odata/ProductsRecipes";

  const productsStore = new CustomStore({
    key: "ProductId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Products");
      if (!res.ok) throw new Error("Błąd ładowania produktów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Receptury produktów"
      keyExpr="ProductRecipeId"
      columns={
        <>
          <Column
            dataField="ProductRecipeId"
            caption="ID receptury"
            allowEditing={false}
            width={150}
          />

          <Column
            dataField="ProductId"
            caption="Produkt"
            width={250}
            lookup={{
              dataSource: productsStore,
              valueExpr: "ProductId",
              displayExpr: "ProductName",
            }}
          >
            <RequiredRule message="Wybierz produkt" />
          </Column>

          <Column dataField="RecipeName" caption="Nazwa receptury" width={300}>
            <RequiredRule message="Podaj nazwę receptury" />
          </Column>

          <Column
            dataField="CreatedAt"
            caption="Data utworzenia"
            dataType="date"
            allowEditing={false}
            width={180}
          />
        </>
      }
    />
  );
}
