"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
  MasterDetail,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import DataGrid from "devextreme-react/data-grid";

export default function ProductsQuantityUnitsGrid() {
  const readUrl =
    "http://localhost:5135/odata/ProductsQuantityUnits?$expand=Products";
  const apiUrl = "http://localhost:5135/odata/ProductsQuantityUnits";

  return (
    <DevExpressGrid
      readUrl={readUrl}
      apiUrl={apiUrl}
      title="Jednostki miary"
      keyExpr="ProductQuantityUnitId"
      columns={
        <>
          <Column
            dataField="ProductQuantityUnitId"
            caption="ID"
            allowEditing={false}
            width={100}
          />

          <Column dataField="UnitName" caption="Nazwa jednostki" width={220}>
            <RequiredRule message="Podaj nazwę jednostki" />
            <StringLengthRule min={2} max={100} message="Od 2 do 100 znaków" />
          </Column>

          <Column dataField="UnitSymbol" caption="Symbol" width={150}>
            <StringLengthRule max={10} message="Maks. 10 znaków" />
          </Column>

          <Column
            caption="Liczba produktów"
            calculateCellValue={(data: any) => data.Products?.length ?? 0}
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

          <MasterDetail
            enabled={true}
            component={({ data }: any) => {
              const products = data.data.Products || [];
              if (!products.length) {
                return (
                  <div className="p-4 text-gray-500 italic">
                    Brak produktów powiązanych z tą jednostką.
                  </div>
                );
              }

              return (
                <div className="p-4">
                  <h4 className="text-lg font-semibold mb-2">
                    Produkty w jednostce: {data.data.UnitName}
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
