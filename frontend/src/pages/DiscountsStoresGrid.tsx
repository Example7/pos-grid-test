"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function DiscountsStoresGrid() {
  const apiUrl = "http://localhost:5135/odata/DiscountsStores";

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

  const storesStore = new CustomStore({
    key: "StoreId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Stores");
      if (!res.ok) throw new Error("Błąd ładowania sklepów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Rabat przypisany do sklepu"
      keyExpr="DiscountStoreId"
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
            dataField="StoreId"
            caption="Sklep"
            lookup={{
              dataSource: storesStore,
              valueExpr: "StoreId",
              displayExpr: "StoreName",
            }}
            width={250}
          >
            <RequiredRule message="Sklep jest wymagany" />
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
