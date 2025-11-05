"use client";

import React, { useState, useEffect, useMemo } from "react";
import TreeView from "devextreme-react/tree-view";
import ScrollView from "devextreme-react/scroll-view";
import Toolbar, { Item as ToolbarItem } from "devextreme-react/toolbar";

import CountriesGrid from "./CountriesGrid";
import ProductsGrid from "./ProductsGrid";
import ContractorsGrid from "./ContractorsGrid";
import EmployeesGrid from "./EmployeesGrid";
import OrdersGrid from "./OrdersGrid";
import StoresGrid from "./StoresGrid";
import OrdersItemsGrid from "./OrdersItemsGrid";
import DiscountsGrid from "./DiscountsGrid";
import ProductsVatRatesGrid from "./ProductsVatRatesGrid";
import ProductsQuantityUnitsGrid from "./ProductsQuantityUnitsGrid";
import ProductsCategories1Grid from "./ProductsCategories1Grid";
import ProductsCategories2Grid from "./ProductsCategories2Grid";
import ProductsBarcodesGrid from "./ProductsBarcodesGrid";
import OrdersPaymentsStatusesGrid from "./OrdersPaymentsStatusesGrid";
import OrdersRealizationsStatusesGrid from "./OrdersRealizationsStatusesGrid";
import OrdersRealizationsTypesGrid from "./OrdersRealizationsTypesGrid";
import PaymentMethodsGrid from "./PaymentMethodsGrid";
import OutcomesFinancialDocumentsGrid from "./OutcomesFinancialDocumentsGrid";
import OutcomesFinancialDocumentsItemsGrid from "./OutcomesFinancialDocumentsItemsGrid";
import OutcomesFinancialDocumentsVatSummariesGrid from "./OutcomesFinancialDocumentsVatSummariesGrid";
import OutcomesFinancialDocumentsStatusesGrid from "./OutcomesFinancialDocumentsStatusesGrid";
import ProductsRecipesGrid from "./ProductsRecipesGrid";
import ProductsRecipesItemsGrid from "./ProductsRecipesItemsGrid";
import PosesGrid from "./PosesGrid";
import PosesTypesGrid from "./PosesTypesGrid";
import StoresDocumentsTypesCategoriesGrid from "./StoresDocumentsTypesCategoriesGrid";
import StoresDocumentsTypesGrid from "./StoresDocumentsTypesGrid";
import StoresOrdersTypesGrid from "./StoresOrdersTypesGrid";
import StockGrid from "./StockGrid";
import StockHistoryGrid from "./StockHistoryGrid";
import DiscountsStoresGrid from "./DiscountsStoresGrid";
import DiscountsProductsGrid from "./DiscountsProductsGrid";
import LoyaltiesWalletsGrid from "./LoyaltiesWalletsGrid";
import YearsGrid from "./YearsGrid";
import SyncVersionsGrid from "./SyncVersionsGrid";

export default function Home() {
  const sections = useMemo(
    () => [
      {
        id: "section_produkty",
        text: "Produkty",
        items: [
          {
            id: "item_produkty",
            text: "Produkty",
            component: <ProductsGrid />,
          },
          {
            id: "item_kat1",
            text: "Kategorie 1",
            component: <ProductsCategories1Grid />,
          },
          {
            id: "item_kat2",
            text: "Kategorie 2",
            component: <ProductsCategories2Grid />,
          },
          {
            id: "item_jm",
            text: "Jednostki miary",
            component: <ProductsQuantityUnitsGrid />,
          },
          {
            id: "item_vat",
            text: "Stawki VAT",
            component: <ProductsVatRatesGrid />,
          },
          {
            id: "item_barcode",
            text: "Kody kreskowe",
            component: <ProductsBarcodesGrid />,
          },
          {
            id: "item_receptury",
            text: "Receptury",
            component: <ProductsRecipesGrid />,
          },
          {
            id: "item_receptury_items",
            text: "Pozycje receptur",
            component: <ProductsRecipesItemsGrid />,
          },
        ],
      },
      {
        id: "section_zamowienia",
        text: "Zamówienia",
        items: [
          { id: "item_orders", text: "Zamówienia", component: <OrdersGrid /> },
          {
            id: "item_orders_items",
            text: "Pozycje zamówień",
            component: <OrdersItemsGrid />,
          },
          {
            id: "item_pay_status",
            text: "Statusy płatności",
            component: <OrdersPaymentsStatusesGrid />,
          },
          {
            id: "item_real_status",
            text: "Statusy realizacji",
            component: <OrdersRealizationsStatusesGrid />,
          },
          {
            id: "item_real_type",
            text: "Typy realizacji",
            component: <OrdersRealizationsTypesGrid />,
          },
        ],
      },
      {
        id: "section_finanse",
        text: "Dokumenty finansowe",
        items: [
          {
            id: "item_outcomes_docs",
            text: "Dokumenty wydatków",
            component: <OutcomesFinancialDocumentsGrid />,
          },
          {
            id: "item_outcomes_items",
            text: "Pozycje dokumentów wydatków",
            component: <OutcomesFinancialDocumentsItemsGrid />,
          },
          {
            id: "item_outcomes_vat",
            text: "Podsumowania VAT dokumentów",
            component: <OutcomesFinancialDocumentsVatSummariesGrid />,
          },
          {
            id: "item_outcomes_status",
            text: "Statusy dokumentów wydatków",
            component: <OutcomesFinancialDocumentsStatusesGrid />,
          },
        ],
      },
      {
        id: "section_magazyn",
        text: "Magazyn",
        items: [
          { id: "item_stores", text: "Magazyny", component: <StoresGrid /> },
          {
            id: "item_doc_types",
            text: "Typy dokumentów",
            component: <StoresDocumentsTypesGrid />,
          },
          {
            id: "item_doc_cats",
            text: "Kategorie typów dokumentów",
            component: <StoresDocumentsTypesCategoriesGrid />,
          },
          {
            id: "item_order_types",
            text: "Typy zamówień magazynowych",
            component: <StoresOrdersTypesGrid />,
          },
          {
            id: "item_stock",
            text: "Stan magazynowy",
            component: <StockGrid />,
          },
          {
            id: "item_stock_history",
            text: "Historia stanów",
            component: <StockHistoryGrid />,
          },
        ],
      },
      {
        id: "section_rabaty",
        text: "Rabaty i lojalność",
        items: [
          {
            id: "item_discounts",
            text: "Rabaty",
            component: <DiscountsGrid />,
          },
          {
            id: "item_discounts_stores",
            text: "Sklepy rabatów",
            component: <DiscountsStoresGrid />,
          },
          {
            id: "item_discounts_products",
            text: "Produkty rabatów",
            component: <DiscountsProductsGrid />,
          },
          {
            id: "item_wallets",
            text: "Portfele lojalnościowe",
            component: <LoyaltiesWalletsGrid />,
          },
        ],
      },
      {
        id: "section_inne",
        text: "Inne dane",
        items: [
          { id: "item_countries", text: "Kraje", component: <CountriesGrid /> },
          {
            id: "item_contractors",
            text: "Kontrahenci",
            component: <ContractorsGrid />,
          },
          {
            id: "item_employees",
            text: "Pracownicy",
            component: <EmployeesGrid />,
          },
          {
            id: "item_methods",
            text: "Metody płatności",
            component: <PaymentMethodsGrid />,
          },
          { id: "item_poses", text: "Kasy POS", component: <PosesGrid /> },
          {
            id: "item_poses_types",
            text: "Typy kas POS",
            component: <PosesTypesGrid />,
          },
          { id: "item_years", text: "Rok", component: <YearsGrid /> },
          {
            id: "item_sync",
            text: "Wersje synchronizacji",
            component: <SyncVersionsGrid />,
          },
        ],
      },
    ],
    []
  );

  const allItems = useMemo(() => sections.flatMap((s) => s.items), [sections]);
  const [selected, setSelected] = useState<React.ReactNode>(null);

  useEffect(() => {
    const saved = localStorage.getItem("selectedGridId");
    if (saved) {
      const found = allItems.find((i) => i.id === saved);
      if (found) setSelected(found.component);
    }
  }, [allItems]);

  const handleSelect = (e: any) => {
    const node = e.node.itemData;
    if (node.component) {
      setSelected(node.component);
      localStorage.setItem("selectedGridId", node.id);
      window.scrollTo({ top: 0, behavior: "smooth" });
    }
  };

  return (
    <div className="flex flex-col h-screen bg-gray-100">
      <Toolbar className="shadow-sm border-b border-gray-200 bg-white">
        <ToolbarItem
          location="before"
          widget="dxButton"
          options={{ text: "Strona główna", onClick: () => setSelected(null) }}
        />
        <ToolbarItem location="before" text="Panel administracyjny" />
        <ToolbarItem
          location="after"
          widget="dxButton"
          options={{
            icon: "user",
            text: "Admin",
            hint: "Profil użytkownika",
            onClick: () => alert("Profil użytkownika – w przygotowaniu"),
          }}
        />
      </Toolbar>

      <div className="flex flex-1">
        <div className="w-72 bg-white border-r border-gray-200">
          <div className="p-4 border-b border-gray-100 text-center font-semibold text-lg">
            Panele danych
          </div>

          <ScrollView height="calc(100vh - 100px)">
            <TreeView
              items={sections}
              dataStructure="tree"
              searchEnabled={true}
              searchMode="contains"
              searchExpr={["text"]}
              onItemClick={handleSelect}
              animationEnabled={true}
              selectByClick={true}
              expandAllEnabled={true}
            />
          </ScrollView>
        </div>

        <div className="flex-1 p-6 overflow-y-auto">
          {selected ? (
            selected
          ) : (
            <div className="flex flex-col items-center justify-center h-full text-gray-600">
              <h2 className="text-3xl font-semibold mb-3">
                Witaj w panelu administracyjnym
              </h2>
              <p className="text-lg">
                Wybierz sekcję z menu po lewej stronie, aby rozpocząć pracę.
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
