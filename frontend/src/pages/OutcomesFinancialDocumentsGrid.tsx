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
import CustomStore from "devextreme/data/custom_store";

export default function OutcomesFinancialDocumentsGrid() {
  const apiUrl = "http://localhost:5135/odata/OutcomesFinancialDocuments";
  const readUrl =
    "http://localhost:5135/odata/OutcomesFinancialDocuments?$expand=OutcomesFinancialDocumentsItems($expand=Product)";

  // Lookup stores
  const contractorsStore = new CustomStore({
    key: "ContractorId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Contractors");
      if (!res.ok) throw new Error("Błąd ładowania kontrahentów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const docTypesStore = new CustomStore({
    key: "FinancialDocumentTypeId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/FinancialDocumentsTypes"
      );
      if (!res.ok) throw new Error("Błąd ładowania typów dokumentów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  const docStatusesStore = new CustomStore({
    key: "FinancialDocumentStatusId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch(
        "http://localhost:5135/odata/OutcomesFinancialDocumentsStatuses"
      );
      if (!res.ok) throw new Error("Błąd ładowania statusów dokumentów");
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

  const posesStore = new CustomStore({
    key: "PosId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Poses");
      if (!res.ok) throw new Error("Błąd ładowania POS-ów");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      readUrl={readUrl}
      title="Dokumenty finansowe sprzedaży"
      keyExpr="OutcomeFinancialDocumentId"
      columns={
        <>
          <Column
            dataField="OutcomeFinancialDocumentId"
            caption="ID"
            allowEditing={false}
            width={180}
            cellRender={({ value }) => value?.substring(0, 8) + "..."}
          />

          <Column
            dataField="FinancialDocumentNumber"
            caption="Numer dokumentu"
            width={200}
          >
            <RequiredRule message="Podaj numer dokumentu" />
            <StringLengthRule min={3} max={100} message="Od 3 do 100 znaków" />
          </Column>

          <Column
            dataField="StoreId"
            caption="Sklep"
            lookup={{
              dataSource: storesStore,
              valueExpr: "StoreId",
              displayExpr: "StoreName",
            }}
            width={150}
          />

          <Column
            dataField="PosId"
            caption="Kasa POS"
            lookup={{
              dataSource: posesStore,
              valueExpr: "PosId",
              displayExpr: "PosName",
            }}
            width={180}
          />

          <Column
            dataField="FinancialDocumentTypeId"
            caption="Typ dokumentu"
            lookup={{
              dataSource: docTypesStore,
              valueExpr: "FinancialDocumentTypeId",
              displayExpr: "TypeName",
            }}
            width={200}
          >
            <RequiredRule message="Wybierz typ dokumentu" />
          </Column>

          <Column
            dataField="FinancialDocumentStatusId"
            caption="Status"
            lookup={{
              dataSource: docStatusesStore,
              valueExpr: "FinancialDocumentStatusId",
              displayExpr: "OutcomeFinancialDocumentStatusName",
            }}
            width={180}
          />

          <Column
            dataField="ContractorId"
            caption="Kontrahent"
            lookup={{
              dataSource: contractorsStore,
              valueExpr: "ContractorId",
              displayExpr: "ContractorName",
            }}
            width={220}
          />

          <Column
            dataField="CustomerName"
            caption="Nazwa klienta"
            width={200}
          />

          <Column dataField="CustomerNip" caption="NIP klienta" width={150}>
            <PatternRule
              pattern={/^[0-9]{10}$/}
              message="NIP musi mieć 10 cyfr"
            />
          </Column>

          <Column
            dataField="FinancialDocumentDate"
            caption="Data dokumentu"
            dataType="date"
            width={150}
          />

          <Column
            dataField="TotalGrossValue"
            caption="Wartość brutto"
            dataType="number"
            format="#,##0.00 zł"
            width={150}
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
              const items = data.data.OutcomesFinancialDocumentsItems || [];
              if (!items.length) {
                return (
                  <div className="p-4 text-gray-500 italic">
                    Brak pozycji w tym dokumencie.
                  </div>
                );
              }

              return (
                <div className="p-4">
                  <h4 className="text-lg font-semibold mb-2">
                    Pozycje dokumentu {data.data.FinancialDocumentNumber}
                  </h4>
                  <DataGrid
                    dataSource={items}
                    showBorders
                    columnAutoWidth
                    rowAlternationEnabled
                    hoverStateEnabled
                  >
                    <Column
                      caption="ID produktu"
                      width={120}
                      cellRender={({ data }) =>
                        data?.Product?.ProductId || data?.ProductId
                      }
                    />

                    <Column
                      caption="Nazwa produktu"
                      width={250}
                      cellRender={({ data }) =>
                        data?.Product?.ProductName || "(brak nazwy)"
                      }
                    />

                    <Column
                      dataField="Quantity"
                      caption="Ilość"
                      dataType="number"
                      width={100}
                    />

                    <Column
                      caption="Cena netto"
                      calculateCellValue={(rowData: any) =>
                        rowData.NetValue
                          ? (rowData.NetValue / rowData.Quantity).toFixed(2)
                          : ""
                      }
                      dataType="number"
                      format="#,##0.00 zł"
                      width={120}
                    />

                    <Column
                      dataField="GrossPrice"
                      caption="Cena brutto"
                      dataType="number"
                      format="#,##0.00 zł"
                      width={120}
                    />

                    <Column
                      caption="VAT (%)"
                      calculateCellValue={(rowData: any) =>
                        rowData.VatRateValue ?? ""
                      }
                      dataType="number"
                      width={80}
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
