"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function StoresGrid() {
  const apiUrl = "http://localhost:5135/odata/Stores";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Magazyny"
      keyExpr="StoreId"
      columns={
        <>
          <Column
            dataField="StoreId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column dataField="StoreName" caption="Nazwa magazynu" width={250}>
            <RequiredRule message="Nazwa magazynu jest wymagana" />
          </Column>

          <Column dataField="StoreAddress" caption="Adres" width={250} />

          <Column dataField="StoreCountryId" caption="Kraj" width={120} />

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
