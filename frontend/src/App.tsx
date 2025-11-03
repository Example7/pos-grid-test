import { BrowserRouter, Routes, Route, NavLink } from "react-router-dom";
import { Home, Mui, Shadcn, AgGrid, DevExpress } from "./pages";
import { Table, Grid, HomeIcon } from "lucide-react";
import ShopsGrid from "./pages/ShopsGrid";
import DevicesGrid from "./pages/DevicesGrid";
import UsersGrid from "./pages/UsersGrid";
import "devextreme/dist/css/dx.light.css";
import "./custom-theme.css";

export default function App() {
  const links = [
    { to: "/", label: "Home", icon: <HomeIcon size={18} /> },
    //{ to: "/mui", label: "MUI DataGrid", icon: <Grid size={18} /> },
    //{ to: "/shadcn", label: "Shadcn Table", icon: <Table size={18} /> },
    //{ to: "/aggrid", label: "AG Grid", icon: <Grid size={18} /> },
    { to: "/devexpress", label: "DevExpress", icon: <Grid size={18} /> },
    { to: "/shops", label: "Shops", icon: <Grid size={18} /> },
    { to: "/devices", label: "Devices", icon: <Grid size={18} /> },
    { to: "/users", label: "Users", icon: <Grid size={18} /> },
  ];

  return (
    <BrowserRouter>
      <nav className="bg-white border-b shadow-sm">
        <div className="mx-auto flex max-w-6xl items-center justify-between p-3">
          <h1 className="text-lg font-semibold text-gray-700">Grid Showcase</h1>
          <ul className="flex gap-4">
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
          <Route path="/mui" element={<Mui />} />
          <Route path="/shadcn" element={<Shadcn />} />
          <Route path="/aggrid" element={<AgGrid />} />
          <Route path="/devexpress" element={<DevExpress />} />
          <Route path="/shops" element={<ShopsGrid />} />
          <Route path="/devices" element={<DevicesGrid />} />
          <Route path="/users" element={<UsersGrid />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}
