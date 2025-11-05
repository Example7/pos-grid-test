"use client";

import { Column, RequiredRule, PatternRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function OutcomesFinancialDocumentsItemsGrid() {
  const apiUrl = "http://localhost:5135/odata/OutcomesFinancialDocumentsItems";

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

  const documentsStore = new CustomStore({
    key: "OutcomeFinancialDocumentId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/OutcomesFinancialDocuments"
      );
      if (!res.ok) throw new Error("Błąd ładowania dokumentów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Pozycje dokumentów sprzedaży"
      keyExpr="OutcomeFinancialDocumentItemId"
      columns={
        <>
          <Column
            dataField="OutcomeFinancialDocumentItemId"
            caption="ID"
            allowEditing={false}
            width={180}
            cellRender={({ value }) => value?.substring(0, 8) + "..."}
          />

          <Column
            dataField="OutcomeFinancialDocumentId"
            caption="Dokument"
            lookup={{
              dataSource: documentsStore,
              valueExpr: "OutcomeFinancialDocumentId",
              displayExpr: "FinancialDocumentNumber",
            }}
            width={200}
          />

          <Column
            dataField="ProductId"
            caption="Produkt"
            lookup={{
              dataSource: productsStore,
              valueExpr: "ProductId",
              displayExpr: "ProductName",
            }}
            width={200}
          >
            <RequiredRule message="Wybierz produkt" />
          </Column>

          <Column
            dataField="Quantity"
            caption="Ilość"
            dataType="number"
            width={100}
          >
            <RequiredRule message="Podaj ilość" />
            <PatternRule
              pattern={/^[0-9]+(\.[0-9]{1,2})?$/}
              message="Niepoprawna liczba"
            />
          </Column>

          <Column
            dataField="GrossPrice"
            caption="Cena brutto"
            dataType="number"
            width={120}
          >
            <RequiredRule message="Podaj cenę" />
          </Column>

          <Column
            dataField="CostPrice"
            caption="Cena kosztowa netto"
            dataType="number"
            width={150}
          />

          <Column
            dataField="ProductVatRateId"
            caption="Stawka VAT"
            lookup={{
              dataSource: vatRatesStore,
              valueExpr: "ProductVatRateId",
              displayExpr: "VatRateValue",
            }}
            width={150}
          />

          <Column
            dataField="VatRateValue"
            caption="Wartość VAT (%)"
            dataType="number"
            width={130}
            allowEditing={false}
          />

          <Column
            dataField="NetValue"
            caption="Wartość netto"
            dataType="number"
            format="#,##0.00 zł"
            width={140}
            allowEditing={false}
          />

          <Column
            dataField="GrossValue"
            caption="Wartość brutto"
            dataType="number"
            format="#,##0.00 zł"
            width={140}
            allowEditing={false}
          />

          <Column
            dataField="VatValue"
            caption="Kwota VAT"
            dataType="number"
            format="#,##0.00 zł"
            width={140}
            allowEditing={false}
          />

          <Column
            dataField="CreatedByEmployeeId"
            caption="Utworzył"
            lookup={{
              dataSource: employeesStore,
              valueExpr: "EmployeeId",
              displayExpr: "EmployeeName",
            }}
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
