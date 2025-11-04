"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
  PatternRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function ContractorsGrid() {
  const apiUrl = "http://localhost:5135/odata/Contractors";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Kontrahenci"
      keyExpr="ContractorId"
      columns={
        <>
          <Column
            dataField="ContractorId"
            caption="ID"
            allowEditing={false}
            width={200}
            cellRender={({ value }) => value?.substring(0, 8) + "..."}
          />

          <Column
            dataField="ContractorLogo"
            caption="Logo"
            width={100}
            allowSorting={false}
            cellRender={({ value }) =>
              value ? (
                <img
                  src={value}
                  alt="logo"
                  style={{
                    width: "40px",
                    height: "40px",
                    objectFit: "contain",
                    borderRadius: "8px",
                  }}
                />
              ) : (
                <span style={{ color: "#999" }}>Brak</span>
              )
            }
          />

          <Column dataField="ContractorName" caption="Nazwa" width={250}>
            <RequiredRule message="Nazwa jest wymagana" />
            <StringLengthRule min={3} message="Min. 3 znaki" />
          </Column>

          <Column dataField="ContractorTaxid" caption="NIP" width={150}>
            <PatternRule
              pattern={/^[0-9]{10}$/}
              message="Niepoprawny format NIP"
            />
          </Column>

          <Column
            dataField="ContractorAddressCiti"
            caption="Miasto"
            width={150}
          />
          <Column
            dataField="ContractorAddressStreet"
            caption="Ulica"
            width={200}
          />
          <Column
            dataField="ContractorAddressPostalCode"
            caption="Kod pocztowy"
            width={120}
          />

          <Column
            dataField="ContractorPurchasesPhone"
            caption="Telefon (zakupy)"
            width={150}
          />
          <Column
            dataField="ContractorPurchasesEmail"
            caption="E-mail (zakupy)"
            width={200}
          />

          <Column
            dataField="ContractorSalesPhone"
            caption="Telefon (sprzedaż)"
            width={150}
          />
          <Column
            dataField="ContractorSalesEmail"
            caption="E-mail (sprzedaż)"
            width={200}
          />

          <Column
            dataField="ContractorPurchasesNotes"
            caption="Uwagi (zakupy)"
            width={200}
          />
          <Column
            dataField="ContractorSalesNotes"
            caption="Uwagi (sprzedaż)"
            width={200}
          />

          <Column
            dataField="ContractorIsSupplier"
            caption="Dostawca"
            dataType="boolean"
            width={120}
          />
          <Column
            dataField="ContractorIsCustomer"
            caption="Klient"
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
        </>
      }
    />
  );
}
