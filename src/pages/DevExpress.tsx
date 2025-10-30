"use client";

import DataGrid, {
  Column,
  Paging,
  Pager,
  SearchPanel,
  Export,
  Editing,
  Toolbar,
  Item as ToolbarItem,
} from "devextreme-react/data-grid";
import type { Properties as DataGridProps } from "devextreme/ui/data_grid";
import "devextreme/dist/css/dx.material.blue.light.css";

import { useSupabaseProducts } from "@/hooks/useSupabaseProducts";
import { devexpressColumns } from "@/lib/productColumn";
import { updateProduct } from "@/lib/updateProduct";
import { addProduct } from "@/lib/addProduct";
import { deleteProduct } from "@/lib/deleteProduct";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import type { Product } from "@/lib/mockData";

export default function DevExpressGrid() {
  const { data, loading, error, refetch, progress } = useSupabaseProducts();

  const handleRowUpdated: NonNullable<DataGridProps["onRowUpdated"]> = async (
    e
  ) => {
    const success = await updateProduct(e.data as Product);
    if (success) refetch();
    else alert("Błąd zapisu do bazy");
  };

  const handleRowInserted: NonNullable<DataGridProps["onRowInserted"]> = async (
    e
  ) => {
    try {
      await addProduct(
        e.data as Omit<Product, "id" | "created_at" | "updated_at">
      );
      refetch();
    } catch (err) {
      console.error("Błąd dodawania produktu:", err);
      alert("Błąd dodawania produktu");
    }
  };

  const handleRowRemoved: NonNullable<DataGridProps["onRowRemoved"]> = async (
    e
  ) => {
    try {
      const product = e.data as Product;
      const confirmed = confirm(
        `Czy na pewno chcesz usunąć "${product.name}"?`
      );
      if (!confirmed) return;

      const success = await deleteProduct(product.id);
      if (success) refetch();
      else alert("Błąd usuwania produktu");
    } catch (err) {
      console.error(err);
    }
  };

  if (loading)
    return (
      <LoadingSpinner text="Wczytywanie produktów..." progress={progress} />
    );

  if (error)
    return (
      <div className="p-6 text-red-600 font-medium">
        Błąd podczas wczytywania danych: {error}
      </div>
    );

  return (
    <div className="p-6">
      <h2 className="text-xl font-semibold mb-4">Tabela DevExpress</h2>

      <DataGrid
        dataSource={data}
        keyExpr="id"
        showBorders
        rowAlternationEnabled
        allowColumnReordering
        hoverStateEnabled
        columnAutoWidth
        onRowUpdated={handleRowUpdated}
        onRowInserted={handleRowInserted}
        onRowRemoved={handleRowRemoved}
      >
        <Editing mode="row" allowUpdating allowAdding allowDeleting useIcons />

        <SearchPanel
          visible
          highlightCaseSensitive
          placeholder="Szukaj produktu..."
        />

        <Paging defaultPageSize={20} />
        <Pager
          showPageSizeSelector
          allowedPageSizes={[10, 20, 50, 100]}
          showNavigationButtons
          showInfo
          infoText="Strona {0} z {1} ({2} rekordów)"
        />

        <Toolbar>
          <ToolbarItem name="addRowButton" />
          <ToolbarItem name="searchPanel" />
          <ToolbarItem name="exportButton" />
        </Toolbar>

        <Export
          enabled
          allowExportSelectedData
          texts={{ exportAll: "Eksportuj wszystko" }}
        />

        {devexpressColumns.map((col) => (
          <Column key={col.dataField} {...col} />
        ))}
      </DataGrid>
    </div>
  );
}
