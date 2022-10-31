import { GridRowId } from "@mui/x-data-grid";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useGetAssets } from "../api/useAssets";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/Table";
import { AssetResponseDto } from "../__generated__/api-types";



export default function Assets () {
    const {status, statusText, data, error, loading} = useGetAssets();
    const errorMsg = "There is no assets in database"
    const navigate = useNavigate()

    const navigator = () => {
        navigate(`/assets/0`,  {replace: true})
    }

    
   
    return <>
   {status === 404 ? 
        <NotFound message={errorMsg}/> : data !== undefined ? <DataGridTable<AssetResponseDto> rows={data} /> : <CircularLoader />}    
    </>
    
}