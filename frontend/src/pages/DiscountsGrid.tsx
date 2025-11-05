"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
  PatternRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function DiscountsGrid() {
  const apiUrl = "http://localhost:5135/odata/Discounts";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Rabaty"
      keyExpr="DiscountId"
      columns={
        <>
          <Column
            dataField="DiscountId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column dataField="DiscountName" caption="Nazwa rabatu" width={220}>
            <RequiredRule message="Podaj nazwę rabatu" />
            <StringLengthRule min={3} max={100} message="Od 3 do 100 znaków" />
          </Column>

          <Column dataField="DiscountType" caption="Typ rabatu" width={180}>
            <RequiredRule message="Wymagany typ rabatu" />
          </Column>

          <Column
            dataField="DiscountValue"
            caption="Wartość rabatu"
            dataType="number"
            width={150}
          >
            <RequiredRule message="Podaj wartość rabatu" />
            <PatternRule
              pattern={/^[0-9]+(\.[0-9]{1,2})?$/}
              message="Niepoprawny format liczby"
            />
          </Column>

          <Column
            caption="Produkty objęte"
            calculateCellValue={(data: any) =>
              data.DiscountsProducts?.length ?? 0
            }
            allowEditing={false}
            width={160}
          />

          <Column
            caption="Sklepy objęte"
            calculateCellValue={(data: any) =>
              data.DiscountsStores?.length ?? 0
            }
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
