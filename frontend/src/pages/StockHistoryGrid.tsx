"use client";

import { Column, RequiredRule, NumericRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function StockHistoryGrid() {
  const apiUrl = "http://localhost:5135/odata/StockHistorys";

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
      title="Historia stanów magazynowych"
      keyExpr="Id"
      columns={
        <>
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
            dataField="EmployeeId"
            caption="Pracownik"
            lookup={{
              dataSource: employeesStore,
              valueExpr: "EmployeeId",
              displayExpr: "EmployeeName",
            }}
            width={200}
          />

          <Column
            dataField="DocumentType"
            caption="Typ dokumentu"
            width={180}
          />

          <Column
            dataField="DocumentNumber"
            caption="Numer dokumentu"
            width={200}
          />

          <Column
            dataField="QuantityChange"
            caption="Zmiana ilości"
            dataType="number"
            width={160}
          >
            <NumericRule message="Wartość musi być liczbą" />
          </Column>

          <Column
            dataField="QuantityAfter"
            caption="Stan po zmianie"
            dataType="number"
            width={160}
          />

          <Column
            dataField="CreatedAt"
            caption="Data operacji"
            dataType="datetime"
            allowEditing={false}
            width={180}
          />
        </>
      }
    />
  );
}
