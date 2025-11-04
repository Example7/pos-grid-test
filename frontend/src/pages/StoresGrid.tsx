"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function StoresGrid() {
  const apiUrl = "http://localhost:5135/odata/Stores";

  // Lookup do krajów
  const countriesStore = new CustomStore({
    key: "CountryId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Countries");
      if (!res.ok) throw new Error("Błąd ładowania krajów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

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
            <StringLengthRule min={3} message="Min. 3 znaki" />
          </Column>

          <Column dataField="StoreAddress" caption="Adres" width={300} />

          <Column
            dataField="StoreCountryId"
            caption="Kraj"
            width={160}
            lookup={{
              dataSource: countriesStore,
              valueExpr: "CountryId",
              displayExpr: "CountryName",
            }}
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
