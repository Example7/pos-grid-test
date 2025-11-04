"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
  PatternRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function CountriesGrid() {
  const apiUrl = "http://localhost:5135/odata/Countries";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Kraje"
      keyExpr="CountryId"
      columns={
        <>
          <Column
            dataField="CountryId"
            caption="ID"
            allowEditing={false}
            width={70}
          />

          <Column dataField="CountryName" caption="Nazwa kraju" width={200}>
            <RequiredRule message="Nazwa kraju jest wymagana" />
            <StringLengthRule min={3} max={100} message="Nazwa 3–100 znaków" />
          </Column>

          <Column dataField="CountryCode" caption="Kod kraju (ISO)" width={120}>
            <RequiredRule message="Kod kraju jest wymagany" />
            <PatternRule
              pattern={/^[A-Z]{2,3}$/}
              message="Kod kraju musi być 2–3 wielkimi literami (np. PL, USA)"
            />
          </Column>

          <Column
            dataField="CreatedAt"
            caption="Utworzono"
            dataType="date"
            allowEditing={false}
          />
        </>
      }
    />
  );
}
