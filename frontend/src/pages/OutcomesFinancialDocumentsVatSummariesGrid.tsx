"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function OutcomesFinancialDocumentsVatSummariesGrid() {
  const apiUrl =
    "http://localhost:5135/odata/OutcomesFinancialDocumentsVatSummaries";

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
      title="Podsumowania VAT dokumentów sprzedaży"
      keyExpr="OutcomeFinancialDocumentVatSummaryId"
      columns={
        <>
          <Column
            dataField="OutcomeFinancialDocumentVatSummaryId"
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
          >
            <RequiredRule message="Wybierz dokument" />
          </Column>

          <Column
            dataField="ProductVatRateId"
            caption="Stawka VAT"
            lookup={{
              dataSource: vatRatesStore,
              valueExpr: "ProductVatRateId",
              displayExpr: "VatRateValue",
            }}
            width={150}
          >
            <RequiredRule message="Wybierz stawkę VAT" />
          </Column>

          <Column
            dataField="VatRateValue"
            caption="Wartość VAT (%)"
            dataType="number"
            width={130}
            allowEditing={false}
          />

          <Column
            dataField="FinancialDocumentSummaryNetValue"
            caption="Suma netto"
            dataType="number"
            format="#,##0.00 zł"
            width={150}
            allowEditing={false}
          />

          <Column
            dataField="FinancialDocumentSummaryGrossValue"
            caption="Suma brutto"
            dataType="number"
            format="#,##0.00 zł"
            width={150}
            allowEditing={false}
          />

          <Column
            dataField="FinancialDocumentSummaryVatValue"
            caption="Suma VAT"
            dataType="number"
            format="#,##0.00 zł"
            width={150}
            allowEditing={false}
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
