"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function ProductsRecipesItemsGrid() {
  const apiUrl = "http://localhost:5135/odata/ProductsRecipesItems";

  const productsRecipesStore = new CustomStore({
    key: "ProductRecipeId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/ProductsRecipes");
      if (!res.ok) throw new Error("Błąd ładowania receptur");
      const json = await res.json();
      return json.value ?? json;
    },
  });

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
      title="Składniki receptur produktów"
      keyExpr="ProductRecipeItemId"
      columns={
        <>
          <Column
            dataField="ProductRecipeItemId"
            caption="ID składnika"
            allowEditing={false}
            width={150}
          />

          <Column
            dataField="ProductRecipeId"
            caption="Receptura"
            width={250}
            lookup={{
              dataSource: productsRecipesStore,
              valueExpr: "ProductRecipeId",
              displayExpr: "RecipeName",
            }}
          >
            <RequiredRule message="Wybierz recepturę" />
          </Column>

          <Column
            dataField="ProductRecipeItemProductId"
            caption="Produkt w składzie"
            width={250}
            lookup={{
              dataSource: productsStore,
              valueExpr: "ProductId",
              displayExpr: "ProductName",
            }}
          >
            <RequiredRule message="Wybierz produkt składnik" />
          </Column>

          <Column
            dataField="Quantity"
            caption="Ilość"
            dataType="number"
            width={120}
          >
            <RequiredRule message="Podaj ilość" />
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
