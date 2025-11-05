"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function OrdersRealizationsTypesGrid() {
  const apiUrl =
    "http://localhost:5135/odata/OrdersRealizationsTypes?$expand=Orders";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Typy realizacji zamówień"
      keyExpr="OrderRealizationTypeId"
      columns={
        <>
          <Column
            dataField="OrderRealizationTypeId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column
            dataField="RealizationTypeName"
            caption="Nazwa typu"
            width={250}
          >
            <RequiredRule message="Podaj nazwę typu" />
            <StringLengthRule min={2} max={100} message="Od 2 do 100 znaków" />
          </Column>

          <Column
            caption="Liczba zamówień"
            calculateCellValue={(data: any) => data.Orders?.length ?? 0}
            allowEditing={false}
            width={160}
          />

          <Column
            caption="Ścieżki statusów"
            calculateCellValue={(data: any) =>
              data.OrdersRealizationsTypesStatusesPaths?.length ?? 0
            }
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
