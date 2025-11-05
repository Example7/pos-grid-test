"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function StoresOrdersTypesGrid() {
  const apiUrl = "http://localhost:5135/odata/StoresOrdersTypes";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Typy zamówień magazynowych"
      keyExpr="StoreOrderTypeId"
      columns={
        <>
          <Column
            dataField="StoreOrderTypeId"
            caption="ID typu"
            allowEditing={false}
            width={120}
          />

          <Column
            dataField="StoreOrderTypeName"
            caption="Nazwa typu zamówienia"
            width={300}
          >
            <RequiredRule message="Podaj nazwę typu zamówienia" />
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
