"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function SyncVersionsGrid() {
  const apiUrl = "http://localhost:5135/odata/SyncVersions";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Wersje synchronizacji"
      keyExpr="Version"
      columns={
        <>
          <Column
            dataField="Version"
            caption="Wersja"
            dataType="number"
            width={150}
          >
            <RequiredRule message="Numer wersji jest wymagany" />
          </Column>

          <Column
            dataField="SyncedAt"
            caption="Data synchronizacji"
            dataType="datetime"
            width={220}
          >
            <RequiredRule message="Data synchronizacji jest wymagana" />
          </Column>
        </>
      }
    />
  );
}
