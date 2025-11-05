import { BrowserRouter, Routes, Route } from "react-router-dom";
import "devextreme/dist/css/dx.light.css";
import Home from "./pages/Home";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/*" element={<Home />} />
      </Routes>
    </BrowserRouter>
  );
}
