"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function ProductsCategories1Grid() {
  return (
    <DevExpressGrid
      readUrl="http://localhost:5135/odata/ProductsCategories1View"
      apiUrl="http://localhost:5135/odata/ProductsCategories1"
      title="Kategorie główne"
      keyExpr="ProductCategory1Id"
      columns={
        <>
          <Column
            dataField="ProductCategory1Id"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column
            dataField="Category1Name"
            caption="Nazwa kategorii"
            width={250}
          >
            <RequiredRule message="Podaj nazwę kategorii" />
            <StringLengthRule min={2} max={100} message="Od 2 do 100 znaków" />
          </Column>

          <Column
            dataField="ProductsCount"
            caption="Liczba produktów"
            allowEditing={false}
            width={180}
          />

          <Column
            dataField="SubcategoriesCount"
            caption="Liczba podkategorii"
            allowEditing={false}
            width={180}
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
