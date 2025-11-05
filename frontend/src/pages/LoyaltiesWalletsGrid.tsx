"use client";

import { Column, RequiredRule, NumericRule } from "devextreme-react/data-grid";
import DevExpressGrid from "./DevExpress";

export default function LoyaltiesWalletsGrid() {
  const apiUrl = "http://localhost:5135/odata/LoyaltiesWallets";

  return (
    <DevExpressGrid
      apiUrl={apiUrl}
      title="Portfele lojalnościowe"
      keyExpr="LoyaltyWalletId"
      columns={
        <>
          <Column dataField="WalletName" caption="Nazwa portfela" width={250}>
            <RequiredRule message="Nazwa portfela jest wymagana" />
          </Column>

          <Column
            dataField="PointsBalance"
            caption="Saldo punktów"
            dataType="number"
            width={180}
          >
            <NumericRule message="Saldo musi być liczbą" />
          </Column>

          <Column
            dataField="CreatedAt"
            caption="Data utworzenia"
            dataType="datetime"
            allowEditing={false}
            width={200}
          />
        </>
      }
    />
  );
}
