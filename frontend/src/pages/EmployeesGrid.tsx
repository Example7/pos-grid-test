"use client";

import {
  Column,
  RequiredRule,
  StringLengthRule,
  PatternRule,
} from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";
import CustomStore from "devextreme/data/custom_store";

export default function EmployeesGrid() {
  const apiUrl = "http://localhost:5135/odata/Employees";

  //   const positionsStore = new CustomStore({
  //     key: "EmployeePositionId",
  //     loadMode: "raw",
  //     load: async () => {
  //       const res = await fetch("http://localhost:5135/odata/EmployeesPositions");
  //       if (!res.ok) throw new Error("Błąd ładowania stanowisk");
  //       const json = await res.json();
  //       return json.value ?? json;
  //     },
  //   });

  const supervisorsStore = new CustomStore({
    key: "EmployeeId",
    loadMode: "raw",
    load: async () => {
      const res = await fetch("http://localhost:5135/odata/Employees");
      if (!res.ok) throw new Error("Błąd ładowania przełożonych");
      const json = await res.json();
      return json.value ?? json;
    },
  });

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Pracownicy"
      keyExpr="EmployeeId"
      columns={
        <>
          <Column
            dataField="EmployeeId"
            caption="ID"
            allowEditing={false}
            width={200}
            cellRender={({ value }) => value?.substring(0, 8) + "..."}
          />

          <Column
            dataField="EmployeeLogo"
            caption="Zdjęcie"
            width={100}
            allowSorting={false}
            cellRender={({ value }) =>
              value ? (
                <img
                  src={value}
                  alt="pracownik"
                  style={{
                    width: "40px",
                    height: "40px",
                    objectFit: "cover",
                    borderRadius: "50%",
                  }}
                />
              ) : (
                <span style={{ color: "#999" }}>Brak</span>
              )
            }
          />

          <Column
            dataField="EmployeeName"
            caption="Imię i nazwisko"
            width={220}
          >
            <RequiredRule message="Imię i nazwisko są wymagane" />
            <StringLengthRule min={3} message="Min. 3 znaki" />
          </Column>

          <Column dataField="EmployeeEmail" caption="E-mail" width={220}>
            <PatternRule
              pattern={/^[^\s@]+@[^\s@]+\.[^\s@]+$/}
              message="Niepoprawny e-mail"
            />
          </Column>

          <Column dataField="EmployeePhone" caption="Telefon" width={160}>
            <PatternRule
              pattern={/^[0-9+ ]{7,15}$/}
              message="Niepoprawny numer telefonu"
            />
          </Column>

          {/* <Column
            dataField="EmployeePositionId"
            caption="Stanowisko"
            width={200}
            lookup={{
              dataSource: positionsStore,
              valueExpr: "EmployeePositionId",
              displayExpr: "PositionName",
            }}
          /> */}

          <Column
            dataField="SupervisorEmployeeId"
            caption="Przełożony"
            width={220}
            lookup={{
              dataSource: supervisorsStore,
              valueExpr: "EmployeeId",
              displayExpr: "EmployeeName",
            }}
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
