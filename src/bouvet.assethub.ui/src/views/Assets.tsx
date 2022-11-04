import { GridRowId } from "@mui/x-data-grid";
import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getAssetsFn } from "../api/assetsApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/Table";
import apiClient from "../config/apiClient";
import { AssetResponseDto } from "../__generated__/api-types";



export default function Assets() {
    const { isLoading, isSuccess, isError, error, data} = useQuery<AssetResponseDto[], Error>(["assets"], getAssetsFn)

    return <>
        { isLoading 
        ? <CircularLoader /> 
        :  isError && axios.isAxiosError(error)
        ? <NotFound message={error?.response?.data} />  
        : isSuccess 
        ? <DataGridTable<AssetResponseDto> rows={data} />
        : <></>
        }
    </>   
}

