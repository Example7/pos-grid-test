"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
  PatternRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function ProductsBarcodesGrid() {
  const apiUrl = "http://localhost:5135/odata/ProductsBarcodes?$expand=Product";

  // lookup z produktami
  const productsStore = new CustomStore({
    key: "ProductId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Products");
      if (!res.ok) throw new Error("Błąd ładowania produktów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Kody kreskowe produktów"
      keyExpr="ProductBarcodeId"
      columns={
        <>
          <Column
            dataField="ProductBarcodeId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column
            dataField="ProductId"
            caption="Produkt"
            lookup={{
              dataSource: productsStore,
              valueExpr: "ProductId",
              displayExpr: "ProductName",
            }}
            width={250}
          >
            <RequiredRule message="Wybierz produkt" />
          </Column>

          <Column dataField="Barcode" caption="Kod kreskowy" width={220}>
            <RequiredRule message="Podaj kod kreskowy" />
            <StringLengthRule min={5} max={50} message="Od 5 do 50 znaków" />
            <PatternRule
              pattern={/^[0-9A-Za-z\-]+$/}
              message="Dozwolone tylko cyfry, litery i myślniki"
            />
          </Column>

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
