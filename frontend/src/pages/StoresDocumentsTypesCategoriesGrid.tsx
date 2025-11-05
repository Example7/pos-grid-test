"use client";

import { Column, RequiredRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function StoresDocumentsTypesCategoriesGrid() {
  const apiUrl = "http://localhost:5135/odata/StoresDocumentsTypesCategorys";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Kategorie typów dokumentów magazynowych"
      keyExpr="StoreDocumentTypeCategoryId"
      columns={
        <>
          <Column
            dataField="StoreDocumentTypeCategoryId"
            caption="ID kategorii"
            allowEditing={false}
            width={150}
          />

          <Column
            dataField="CategoryName"
            caption="Nazwa kategorii"
            width={300}
          >
            <RequiredRule message="Podaj nazwę kategorii" />
          </Column>
        </>
      }
    />
  );
}
