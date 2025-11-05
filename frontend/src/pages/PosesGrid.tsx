"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function PosesGrid() {
  const apiUrl = "http://localhost:5135/odata/Poses";

  const posesTypesStore = new CustomStore({
    key: "PosTypeId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/PosesTypes");
      if (!res.ok) throw new Error("Błąd ładowania typów POS");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const storesStore = new CustomStore({
    key: "StoreId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Stores");
      if (!res.ok) throw new Error("Błąd ładowania sklepów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Kasy POS"
      keyExpr="PosId"
      columns={
        <>
          <Column
            dataField="PosId"
            caption="ID kasy"
            allowEditing={false}
            width={120}
          />

          <Column dataField="PosName" caption="Nazwa kasy" width={250}>
            <RequiredRule message="Podaj nazwę kasy POS" />
          </Column>

          <Column
            dataField="PosTypeId"
            caption="Typ POS"
            width={200}
            lookup={{
              dataSource: posesTypesStore,
              valueExpr: "PosTypeId",
              displayExpr: "PosTypeName",
            }}
          />

          <Column
            dataField="StoreId"
            caption="Sklep / Magazyn"
            width={250}
            lookup={{
              dataSource: storesStore,
              valueExpr: "StoreId",
              displayExpr: "StoreName",
            }}
          />

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
