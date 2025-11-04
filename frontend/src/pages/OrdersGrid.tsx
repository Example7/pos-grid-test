"use client";

import { Column, RequiredRule, PatternRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function OrdersGrid() {
  const apiUrl = "http://localhost:5135/odata/Orders";

  const storesStore = new CustomStore({
    key: "StoreId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Stores");
      if (!res.ok) throw new Error("Błąd ładowania magazynów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const paymentsMethodsStore = new CustomStore({
    key: "PaymentMethodId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/PaymentsMethods");
      if (!res.ok) throw new Error("Błąd ładowania metod płatności");
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

  const realizationStatusesStore = new CustomStore({
    key: "OrderRealizationStatusId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/OrdersRealizationsStatuses"
      );
      if (!res.ok) throw new Error("Błąd ładowania statusów realizacji");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const realizationTypesStore = new CustomStore({
    key: "OrderRealizationTypeId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/OrdersRealizationsTypes"
      );
      if (!res.ok) throw new Error("Błąd ładowania typów realizacji");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const paymentStatusesStore = new CustomStore({
    key: "OrderPaymentStatusId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/OrdersPaymentsStatuses"
      );
      if (!res.ok) throw new Error("Błąd ładowania statusów płatności");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Zamówienia"
      keyExpr="OrderId"
      columns={
        <>
          <Column
            dataField="OrderId"
            caption="ID"
            allowEditing={false}
            width={200}
            cellRender={({ value }) => value?.substring(0, 8) + "..."}
          />

          <Column
            dataField="OrderNumber"
            caption="Numer zamówienia"
            width={180}
            allowEditing={false}
          />

          <Column
            dataField="StoreId"
            caption="Magazyn"
            width={180}
            lookup={{
              dataSource: storesStore,
              valueExpr: "StoreId",
              displayExpr: "StoreName",
            }}
          />

          <Column
            dataField="EmployeeId"
            caption="Pracownik"
            width={200}
            lookup={{
              dataSource: employeesStore,
              valueExpr: "EmployeeId",
              displayExpr: "EmployeeName",
            }}
          />

          <Column
            dataField="PaymentMethodId"
            caption="Metoda płatności"
            width={200}
            lookup={{
              dataSource: paymentsMethodsStore,
              valueExpr: "PaymentMethodId",
              displayExpr: "PaymentMethodName",
            }}
          />

          <Column
            dataField="OrderRealizationTypeId"
            caption="Typ realizacji"
            width={200}
            lookup={{
              dataSource: realizationTypesStore,
              valueExpr: "OrderRealizationTypeId",
              displayExpr: "OrderRealizationTypeName",
            }}
          />

          <Column
            dataField="OrderRealizationStatusId"
            caption="Status realizacji"
            width={200}
            lookup={{
              dataSource: realizationStatusesStore,
              valueExpr: "OrderRealizationStatusId",
              displayExpr: "OrderRealizationStatusName",
            }}
          />

          <Column
            dataField="OrderPaymentStatusId"
            caption="Status płatności"
            width={200}
            lookup={{
              dataSource: paymentStatusesStore,
              valueExpr: "OrderPaymentStatusId",
              displayExpr: "OrderPaymentStatusName",
            }}
          />

          <Column
            dataField="TotalAmount"
            caption="Kwota całkowita"
            dataType="number"
            width={150}
          >
            <RequiredRule message="Kwota jest wymagana" />
          </Column>

          <Column
            dataField="DiscountAmount"
            caption="Rabat"
            dataType="number"
            width={120}
          />

          <Column dataField="Nip" caption="NIP" width={150}>
            <PatternRule
              pattern={/^[0-9]{10}$/}
              message="Niepoprawny format NIP"
            />
          </Column>

          <Column dataField="Source" caption="Źródło" width={150} />

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
