"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function OrdersPaymentsStatusesGrid() {
  const apiUrl = "http://localhost:5135/odata/OrdersPaymentsStatuses";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Statusy płatności zamówień"
      keyExpr="OrderPaymentStatusId"
      columns={
        <>
          <Column
            dataField="OrderPaymentStatusId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column
            dataField="OrderPaymentStatusName"
            caption="Nazwa statusu"
            width={250}
          >
            <RequiredRule message="Podaj nazwę statusu" />
            <StringLengthRule min={2} max={100} message="Od 2 do 100 znaków" />
          </Column>

          <Column
            dataField="OrderPaymentStatusLogo"
            caption="Logo (URL)"
            width={250}
          />

          <Column
            caption="Liczba zamówień"
            calculateCellValue={(data: any) => data.Orders?.length ?? 0}
            allowEditing={false}
            width={160}
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
