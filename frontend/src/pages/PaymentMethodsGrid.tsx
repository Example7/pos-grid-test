"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function PaymentsMethodsGrid() {
  const apiUrl = "http://localhost:5135/odata/PaymentsMethods";
  const readUrl = "http://localhost:5135/odata/PaymentsMethods?$expand=Orders";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      readUrl={readUrl}
      title="Metody płatności"
      keyExpr="PaymentMethodId"
      columns={
        <>
          <Column
            dataField="PaymentMethodId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column
            dataField="PaymentMethodName"
            caption="Nazwa metody płatności"
            width={300}
          >
            <RequiredRule message="Podaj nazwę metody płatności" />
            <StringLengthRule min={2} max={100} message="Od 2 do 100 znaków" />
          </Column>

          <Column
            caption="Liczba zamówień"
            calculateCellValue={(data: any) => data.Orders?.length ?? 0}
            allowEditing={false}
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
