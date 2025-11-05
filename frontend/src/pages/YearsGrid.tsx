"use client";

import { Column, RequiredRule, NumericRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function YearsGrid() {
  const apiUrl = "http://localhost:5135/odata/Years";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Lata magazynowe"
      keyExpr="YearId"
      columns={
        <>
          <Column
            dataField="YearNumber"
            caption="Rok"
            dataType="number"
            width={150}
          >
            <RequiredRule message="Rok jest wymagany" />
            <NumericRule message="Rok musi być liczbą" />
          </Column>

          <Column
            dataField="CreatedAt"
            caption="Data utworzenia"
            dataType="datetime"
            allowEditing={false}
            width={200}
          />
        </>
      }
    />
  );
}
