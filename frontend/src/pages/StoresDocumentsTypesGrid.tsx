"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function StoresDocumentsTypesGrid() {
  const apiUrl = "http://localhost:5135/odata/StoresDocumentsTypes";

  const categoriesStore = new CustomStore({
    key: "StoreDocumentTypeCategoryId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/StoresDocumentsTypesCategorys"
      );
      if (!res.ok) throw new Error("Błąd ładowania kategorii");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const orderTypesStore = new CustomStore({
    key: "StoreOrderTypeId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/StoresOrdersTypes");
      if (!res.ok) throw new Error("Błąd ładowania typów zamówień");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Typy dokumentów magazynowych"
      keyExpr="StoreDocumentTypeId"
      columns={
        <>
          <Column
            dataField="StoreDocumentTypeId"
            caption="ID dokumentu"
            allowEditing={false}
            width={180}
          />

          <Column
            dataField="StoreDocumentTypeName"
            caption="Nazwa typu dokumentu"
            width={300}
          >
            <RequiredRule message="Podaj nazwę dokumentu" />
          </Column>

          <Column
            dataField="StoreDocumentTypeDescription"
            caption="Opis"
            width={300}
          />

          <Column
            dataField="StoreDocumentTypeCategoryId"
            caption="Kategoria"
            width={220}
            lookup={{
              dataSource: categoriesStore,
              valueExpr: "StoreDocumentTypeCategoryId",
              displayExpr: "CategoryName",
            }}
          />

          <Column
            dataField="StoreOrderTypeId"
            caption="Typ zamówienia"
            width={220}
            lookup={{
              dataSource: orderTypesStore,
              valueExpr: "StoreOrderTypeId",
              displayExpr: "StoreOrderTypeName",
            }}
          />
        </>
      }
    />
  );
}
