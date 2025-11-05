"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function PosesTypesGrid() {
  const apiUrl = "http://localhost:5135/odata/PosesTypes";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Typy kas POS"
      keyExpr="PosTypeId"
      columns={
        <>
          <Column
            dataField="PosTypeId"
            caption="ID typu"
            allowEditing={false}
            width={120}
          />

          <Column dataField="PosTypeName" caption="Nazwa typu" width={300}>
            <RequiredRule message="Podaj nazwę typu POS" />
          </Column>

          <Column
            dataField="CreatedAt"
            caption="Data utworzenia"
            dataType="date"
            allowEditing={false}
            width={180}
          />
        </>
      }
    />
  );
}
