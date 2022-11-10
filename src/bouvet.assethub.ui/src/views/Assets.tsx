import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { getAssetsFn } from "../api/assetsApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/DataGridTable";
import SpeedDialAddItemsMenu from "../components/SpeedDialAddItemsMenu";
import { AssetResponseDto } from "../_generated/api-types";

export default function Assets() {
    const { isLoading, isSuccess, isError, error, data} = useQuery<AssetResponseDto[], Error>(["assets"], getAssetsFn)
    return <>
        { isLoading 
        ? <CircularLoader /> 
        :  isError && axios.isAxiosError(error)
        ? <NotFound message={error?.response?.data} />  
        : isSuccess 
        ? <DataGridTable<AssetResponseDto> rows={data} headerName="Assets" />
        : <></>
        }
        <SpeedDialAddItemsMenu />

    </>   
}

