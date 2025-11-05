"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function OutcomesFinancialDocumentsStatusesGrid() {
  const apiUrl =
    "http://localhost:5135/odata/OutcomesFinancialDocumentsStatuses";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Statusy dokumentów sprzedaży"
      keyExpr="OutcomeFinancialDocumentStatusId"
      columns={
        <>
          <Column
            dataField="OutcomeFinancialDocumentStatusId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column
            dataField="OutcomeFinancialDocumentStatusName"
            caption="Nazwa statusu"
            width={250}
          >
            <RequiredRule message="Podaj nazwę statusu" />
            <StringLengthRule min={3} max={50} message="Od 3 do 50 znaków" />
          </Column>
        </>
      }
    />
  );
}
