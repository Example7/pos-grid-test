"use client";

import { useState } from "react";
import SelectBox from "devextreme-react/select-box";
import CountriesGrid from "./CountriesGrid";
import ProductsGrid from "./ProductsGrid";
import ContractorsGrid from "./ContractorsGrid";
import EmployeesGrid from "./EmployeesGrid";
import OrdersGrid from "./OrdersGrid";
import StoresGrid from "./StoresGrid";
import OrdersItemsGrid from "./OrdersItemsGrid";

export default function Home() {
  const [selectedGrid, setSelectedGrid] = useState<string | null>(null);

  const grids = [
    { name: "Countries", label: "Kraje", component: <CountriesGrid /> },
    { name: "Products", label: "Produkty", component: <ProductsGrid /> },
    {
      name: "Contractors",
      label: "Kontrahenci",
      component: <ContractorsGrid />,
    },
    { name: "Employees", label: "Pracownicy", component: <EmployeesGrid /> },
    { name: "Orders", label: "Zamówienia", component: <OrdersGrid /> },
    {
      name: "OrdersItems",
      label: "Pozycje zamówień",
      component: <OrdersItemsGrid />,
    },
    { name: "Stores", label: "Magazyny", component: <StoresGrid /> },
  ];

  const selected = grids.find((g) => g.name === selectedGrid)?.component;

  return (
    <div>
      <div className="flex justify-center mb-10">
        <SelectBox
          dataSource={grids}
          displayExpr="label"
          valueExpr="name"
          value={selectedGrid}
          onValueChanged={(e) => setSelectedGrid(e.value)}
          placeholder="Wybierz tabelę..."
          width={300}
        />
      </div>

      <div className="bg-white rounded-2xl shadow-lg p-6 border border-gray-200">
        {selected ? (
          selected
        ) : (
          <p className="text-gray-500 text-center">
            Wybierz tabelę z listy powyżej, aby wyświetlić dane.
          </p>
        )}
      </div>
    </div>
  );
}
