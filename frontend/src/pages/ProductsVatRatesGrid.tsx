"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
  PatternRule,
  MasterDetail,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import DataGrid from "devextreme-react/data-grid";

export default function ProductsVatRatesGrid() {
  const apiUrl = "http://localhost:5135/odata/ProductsVatRates";
  const readUrl =
    "http://localhost:5135/odata/ProductsVatRates?$expand=Products";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      readUrl={readUrl}
      title="Stawki VAT"
      keyExpr="ProductVatRateId"
      columns={
        <>
          <Column
            dataField="ProductVatRateId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column dataField="VatRateName" caption="Nazwa stawki" width={220}>
            <RequiredRule message="Podaj nazwę stawki" />
            <StringLengthRule min={2} max={50} message="Od 2 do 50 znaków" />
          </Column>

          <Column
            dataField="VatRateValue"
            caption="Wartość VAT (%)"
            dataType="number"
            width={150}
          >
            <RequiredRule message="Podaj wartość VAT" />
            <PatternRule
              pattern={/^(?:100|[0-9]{1,2})(?:\.[0-9]{1,2})?$/}
              message="Niepoprawny format liczby (0–100)"
            />
          </Column>

          <Column
            caption="Liczba produktów"
            calculateCellValue={(data: any) => data.Products?.length ?? 0}
            allowEditing={false}
            width={160}
          />

          <Column
            dataField="CreatedAt"
            caption="Utworzono"
            dataType="date"
            allowEditing={false}
            width={160}
          />

          <MasterDetail
            enabled={true}
            component={({ data }: any) => {
              const products = data.data.Products || [];
              if (!products.length) {
                return (
                  <div className="p-4 text-gray-500 italic">
                    Brak produktów dla tej stawki VAT.
                  </div>
                );
              }

              return (
                <div className="p-4">
                  <h4 className="text-lg font-semibold mb-2">
                    Produkty ze stawką VAT {data.data.VatRateName}
                  </h4>
                  <DataGrid
                    dataSource={products}
                    showBorders
                    columnAutoWidth
                    rowAlternationEnabled
                    hoverStateEnabled
                  >
                    <Column dataField="ProductId" caption="ID" width={70} />
                    <Column
                      dataField="ProductName"
                      caption="Nazwa produktu"
                      width={250}
                    />
                    <Column
                      dataField="ProductPrice"
                      caption="Cena"
                      dataType="number"
                      width={120}
                    />
                    <Column
                      dataField="CreatedAt"
                      caption="Utworzono"
                      dataType="date"
                      width={140}
                    />
                  </DataGrid>
                </div>
              );
            }}
          />
        </>
      }
    />
  );
}
