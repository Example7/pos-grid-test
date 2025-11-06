"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function StockGrid() {
  const apiUrl = "http://localhost:5135/odata/Stocks";

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

  const yearsStore = new CustomStore({
    key: "YearId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Years");
      if (!res.ok) throw new Error("Błąd ładowania lat");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Stany magazynowe"
      keyExpr="StockId"
      columns={
        <>
          <Column
            dataField="StockId"
            caption="ID"
            allowEditing={false}
            width={70}
          />
          <Column
            dataField="StoreId"
            caption="Magazyn"
            lookup={{
              dataSource: storesStore,
              valueExpr: "StoreId",
              displayExpr: "StoreName",
            }}
            width={200}
          >
            <RequiredRule message="Magazyn jest wymagany" />
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
            dataField="YearId"
            caption="Rok"
            lookup={{
              dataSource: yearsStore,
              valueExpr: "YearId",
              displayExpr: "YearNumber",
            }}
            width={120}
          />

          <Column
            dataField="StockTotalQuantity"
            caption="Stan całkowity"
            dataType="number"
            allowEditing={false}
            width={160}
          ></Column>

          <Column
            dataField="StockAvailableQuantity"
            caption="Stan dostępny"
            dataType="number"
            allowEditing={false}
            width={160}
          />

          <Column
            dataField="StockMinQuantity"
            caption="Min. ilość"
            dataType="number"
            width={160}
          />

          <Column
            dataField="CreatedAt"
            caption="Utworzono"
            dataType="date"
            allowEditing={false}
            width={160}
          />

          <Column
            dataField="UpdatedAt"
            caption="Zaktualizowano"
            dataType="date"
            allowEditing={false}
            width={160}
          />
        </>
      }
    />
  );
}
