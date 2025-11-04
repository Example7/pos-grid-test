"use client";

import { Column, RequiredRule, PatternRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function OrdersItemsGrid() {
  const apiUrl = "http://localhost:5135/odata/OrdersItems";

  const ordersStore = new CustomStore({
    key: "OrderId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Orders");
      if (!res.ok) throw new Error("Błąd ładowania zamówień");
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

  const vatRatesStore = new CustomStore({
    key: "ProductVatRateId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/ProductsVatRates");
      if (!res.ok) throw new Error("Błąd ładowania stawek VAT");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const employeesStore = new CustomStore({
    key: "EmployeeId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Employees");
      if (!res.ok) throw new Error("Błąd ładowania pracowników");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Pozycje zamówień"
      keyExpr="OrderItemId"
      columns={
        <>
          <Column
            dataField="OrderItemId"
            caption="ID"
            allowEditing={false}
            width={200}
            cellRender={({ value }) => value?.substring(0, 8) + "..."}
          />

          <Column
            dataField="OrderId"
            caption="Zamówienie"
            width={180}
            lookup={{
              dataSource: ordersStore,
              valueExpr: "OrderId",
              displayExpr: "OrderNumber",
            }}
          />

          <Column
            dataField="ProductId"
            caption="Produkt"
            width={220}
            lookup={{
              dataSource: productsStore,
              valueExpr: "ProductId",
              displayExpr: "ProductName",
            }}
          >
            <RequiredRule message="Wybierz produkt" />
          </Column>

          <Column
            dataField="OrderItemQuantity"
            caption="Ilość"
            dataType="number"
            width={120}
          >
            <RequiredRule message="Podaj ilość" />
            <PatternRule
              pattern={/^[0-9]+(\.[0-9]{1,2})?$/}
              message="Niepoprawna liczba"
            />
          </Column>

          <Column
            dataField="OrderItemPrice"
            caption="Cena jednostkowa"
            dataType="number"
            width={150}
          >
            <RequiredRule message="Podaj cenę" />
          </Column>

          <Column
            dataField="OrderItemTotal"
            caption="Wartość całkowita"
            dataType="number"
            allowEditing={false}
            width={150}
          />

          <Column
            dataField="OrderItemProductDiscountPercentRatio"
            caption="Rabat (%)"
            dataType="number"
            width={130}
          />

          <Column
            dataField="OrderItemProductVatRateId"
            caption="Stawka VAT"
            width={150}
            lookup={{
              dataSource: vatRatesStore,
              valueExpr: "ProductVatRateId",
              displayExpr: "VatRateValue",
            }}
          />

          <Column
            dataField="OrderItemGrossPrice"
            caption="Cena brutto"
            dataType="number"
            width={140}
          />

          <Column
            dataField="OrderItemGrossValue"
            caption="Wartość brutto"
            dataType="number"
            width={150}
          />

          <Column
            dataField="OrderItemCreatedByEmployeId"
            caption="Utworzone przez"
            width={180}
            lookup={{
              dataSource: employeesStore,
              valueExpr: "EmployeeId",
              displayExpr: "EmployeeName",
            }}
          />

          <Column
            dataField="OrderItemCreatedAt"
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
