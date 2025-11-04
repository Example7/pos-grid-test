"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
  PatternRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function ProductsGrid() {
  const apiUrl = "http://localhost:5135/odata/Products";

  const categories1Store = new CustomStore({
    key: "ProductCategory1Id",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/ProductsCategories1"
      );
      if (!res.ok) throw new Error("Błąd ładowania kategorii 1");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const categories2Store = new CustomStore({
    key: "ProductCategory2Id",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/ProductsCategories2"
      );
      if (!res.ok) throw new Error("Błąd ładowania kategorii 2");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const quantityUnitsStore = new CustomStore({
    key: "ProductQuantityUnitId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/ProductsQuantityUnits"
      );
      if (!res.ok) throw new Error("Błąd ładowania jednostek miary");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const vatRatesStore = new CustomStore({
    key: "ProductVatRateId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/ProductsVatRates");
      if (!res.ok) throw new Error("Błąd ładowania stawek VAT");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const suppliersStore = new CustomStore({
    key: "SupplierId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Contractors");
      if (!res.ok) throw new Error("Błąd ładowania dostawców");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Produkty"
      keyExpr="ProductId"
      columns={
        <>
          <Column
            dataField="ProductId"
            caption="ID"
            allowEditing={false}
            width={70}
          />

          <Column dataField="ProductName" caption="Nazwa produktu" width={200}>
            <RequiredRule message="Nazwa produktu jest wymagana" />
            <StringLengthRule min={3} message="Min. 3 znaki" />
          </Column>

          <Column dataField="ProductDescription" caption="Opis" width={150}>
            <StringLengthRule max={500} message="Maks. 500 znaków" />
          </Column>

          <Column
            dataField="ProductPrice"
            caption="Cena"
            dataType="number"
            width={120}
          >
            <RequiredRule message="Cena jest wymagana" />
            <PatternRule
              pattern={/^[0-9]+(\.[0-9]{1,2})?$/}
              message="Podaj poprawną liczbę"
            />
          </Column>

          <Column
            dataField="OriginalPrice"
            caption="Cena orginalna"
            dataType="number"
            width={120}
          />

          <Column
            dataField="DiscountedPrice"
            caption="Cena po zniżce"
            dataType="number"
            width={120}
          />

          <Column
            dataField="ProductCategory1Id"
            caption="Kategoria 1"
            lookup={{
              dataSource: categories1Store,
              valueExpr: "ProductCategory1Id",
              displayExpr: "Category1Name",
            }}
            width={160}
          />

          <Column
            dataField="ProductCategory2Id"
            caption="Kategoria 2"
            lookup={{
              dataSource: categories2Store,
              valueExpr: "ProductCategory2Id",
              displayExpr: "Category2Name",
            }}
            width={160}
          />

          <Column
            dataField="ProductQuantityUnitId"
            caption="Jednostka"
            lookup={{
              dataSource: quantityUnitsStore,
              valueExpr: "ProductQuantityUnitId",
              displayExpr: "UnitName",
            }}
            width={140}
          />

          <Column
            dataField="ProductVatRateId"
            caption="Stawka VAT"
            lookup={{
              dataSource: vatRatesStore,
              valueExpr: "ProductVatRateId",
              displayExpr: "VatRateValue",
            }}
            width={140}
          />

          <Column
            dataField="SupplierId"
            caption="Dostawca"
            lookup={{
              dataSource: suppliersStore,
              valueExpr: "SupplierId",
              displayExpr: "ContractorName",
            }}
            width={180}
          />

          <Column
            dataField="MinStockLevel"
            caption="Min. stan magazynowy"
            dataType="number"
            width={160}
          />

          <Column
            dataField="IsSellable"
            caption="Sprzedawalny"
            dataType="boolean"
            width={120}
          />
          <Column
            dataField="IsVisible"
            caption="Widoczny"
            dataType="boolean"
            width={120}
          />
          <Column
            dataField="IsComposite"
            caption="Złożony"
            dataType="boolean"
            width={120}
          />

          <Column
            dataField="CreatedAt"
            caption="Utworzono"
            dataType="date"
            allowEditing={false}
            width={150}
          />
          <Column
            dataField="UpdatedAt"
            caption="Zaktualizowano"
            dataType="date"
            allowEditing={false}
            width={150}
          />

          <Column
            caption="Ilość receptur"
            calculateCellValue={(data: any) =>
              data.ProductsRecipes?.length ?? 0
            }
            allowEditing={false}
            width={150}
          />

          <Column
            caption="Ilość kodów kreskowych"
            calculateCellValue={(data: any) =>
              data.ProductsBarcodes?.length ?? 0
            }
            allowEditing={false}
            width={180}
          />

          <Column
            caption="Ilość zamówień"
            calculateCellValue={(data: any) => data.OrdersItems?.length ?? 0}
            allowEditing={false}
            width={150}
          />
        </>
      }
    />
  );
}
