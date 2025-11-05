"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function OrdersRealizationsStatusesGrid() {
  const apiUrl = "http://localhost:5135/odata/OrdersRealizationsStatuses";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Statusy realizacji zamówień"
      keyExpr="OrderRealizationStatusId"
      columns={
        <>
          <Column
            dataField="OrderRealizationStatusId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column
            dataField="OrderRealizationStatusName"
            caption="Nazwa statusu"
            width={250}
          >
            <RequiredRule message="Podaj nazwę statusu" />
            <StringLengthRule min={2} max={100} message="Od 2 do 100 znaków" />
          </Column>

          <Column
            dataField="OrderRealizationStatusLogo"
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
