"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function DiscountsProductsGrid() {
  const apiUrl = "http://localhost:5135/odata/DiscountsProducts";

  const discountsStore = new CustomStore({
    key: "DiscountId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Discounts");
      if (!res.ok) throw new Error("Błąd ładowania rabatów");
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
      title="Rabat przypisany do produktu"
      keyExpr="DiscountProductId"
      columns={
        <>
          <Column
            dataField="DiscountId"
            caption="Rabat"
            lookup={{
              dataSource: discountsStore,
              valueExpr: "DiscountId",
              displayExpr: "DiscountName",
            }}
            width={250}
          >
            <RequiredRule message="Rabat jest wymagany" />
          </Column>

          <Column
            dataField="ProductId"
            caption="Produkt"
            lookup={{
              dataSource: productsStore,
              valueExpr: "ProductId",
              displayExpr: "ProductName",
            }}
            width={250}
          >
            <RequiredRule message="Produkt jest wymagany" />
          </Column>

          <Column
            dataField="CreatedAt"
            caption="Data przypisania"
            dataType="datetime"
            allowEditing={false}
            width={180}
          />
        </>
      }
    />
  );
}
