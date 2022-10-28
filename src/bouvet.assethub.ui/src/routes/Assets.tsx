import { useGetAssets } from "../api/useAssets";
import CircularLoader from "../components/CircularLoader";
import DataGridTable from "../components/Table";
import { AssetResponseDto } from "../__generated__/api-types";

export default function Assets () {
    const {status, statusText, data, error, loading} = useGetAssets();
    
   
    return <>
    {data !== undefined ? <DataGridTable<AssetResponseDto> rows={data} /> : <CircularLoader />}
    
    
    </>
}