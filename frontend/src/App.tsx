import { BrowserRouter, Routes, Route, NavLink } from "react-router-dom";
import {
  HomeIcon,
  ShoppingCart,
  Users,
  Building2,
  Package,
} from "lucide-react";
import Home from "./pages/Home";
import ProductsGrid from "./pages/ProductsGrid";
import ContractorsGrid from "./pages/ContractorsGrid";
import EmployeesGrid from "./pages/EmployeesGrid";
import OrdersGrid from "./pages/OrdersGrid";
import StoresGrid from "./pages/StoresGrid";
import "devextreme/dist/css/dx.light.css";
import OrdersItemsGrid from "./pages/OrdersItemsGrid";

export default function App() {
  const links = [
    { to: "/", label: "Home", icon: <HomeIcon size={18} /> },
    { to: "/products", label: "Produkty", icon: <Package size={18} /> },
    { to: "/contractors", label: "Kontrahenci", icon: <Users size={18} /> },
    { to: "/employees", label: "Pracownicy", icon: <Users size={18} /> },
    { to: "/orders", label: "Zamówienia", icon: <ShoppingCart size={18} /> },
    {
      to: "/orders-items",
      label: "Pozycje zamówień",
      icon: <ShoppingCart size={18} />,
    },
    { to: "/stores", label: "Magazyny", icon: <Building2 size={18} /> },
  ];

  return (
    <BrowserRouter>
      <nav className="bg-white border-b shadow-sm">
        <div className="mx-auto flex max-w-6xl items-center justify-between p-3">
          <h1 className="text-lg font-semibold text-gray-700">
            Panel zarządzania
          </h1>
          <ul className="flex gap-3">
            {links.map(({ to, label, icon }) => (
              <li key={to}>
                <NavLink
                  to={to}
                  className={({ isActive }) =>
                    `flex items-center gap-2 px-3 py-2 rounded-md text-sm font-medium transition ${
                      isActive
                        ? "bg-blue-600 text-white shadow-sm"
                        : "text-gray-600 hover:bg-gray-100"
                    }`
                  }
                >
                  {icon}
                  {label}
                </NavLink>
              </li>
            ))}
          </ul>
        </div>
      </nav>

      <main className="mx-auto max-w-6xl p-6">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/products" element={<ProductsGrid />} />
          <Route path="/contractors" element={<ContractorsGrid />} />
          <Route path="/employees" element={<EmployeesGrid />} />
          <Route path="/orders" element={<OrdersGrid />} />
          <Route path="/orders-items" element={<OrdersItemsGrid />} />
          <Route path="/stores" element={<StoresGrid />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}
