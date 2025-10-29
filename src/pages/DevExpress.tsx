import DataGrid, {
  Column,
  Paging,
  Pager,
  SearchPanel,
  Export,
  Editing,
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";

import { useSupabaseProducts } from "@/hooks/useSupabaseProducts";
import { devexpressColumns } from "@/lib/productColumn";
import { updateProduct } from "@/lib/updateProduct";

export default function DevExpressGrid() {
  const { data, loading, error, refetch } = useSupabaseProducts();

  const handleRowUpdated = async (e: any) => {
    const success = await updateProduct(e.data);
    if (success) refetch();
    else alert("Błąd zapisu do bazy");
  };

  if (loading) return <p>Ładowanie...</p>;
  if (error) return <p>Błąd: {error}</p>;

  return (
    <div className="p-6">
      <DataGrid
        dataSource={data}
        showBorders
        columnAutoWidth
        onRowUpdated={handleRowUpdated}
      >
        <Editing mode="row" allowUpdating={true} useIcons={true} />
        <SearchPanel visible highlightCaseSensitive />
        <Paging defaultPageSize={20} />
        <Pager
          showPageSizeSelector
          allowedPageSizes={[10, 20, 50]}
          showNavigationButtons
        />
        <Export enabled allowExportSelectedData />
        {devexpressColumns.map((col) => (
          <Column key={col.dataField} {...col} />
        ))}
      </DataGrid>
    </div>
  );
}
