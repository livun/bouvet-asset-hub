import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { getLoansFn } from "../api/loansApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/DataGridTable";
import { AssetResponseDto, LoanHistoryResponseDto, LoanResponseDto } from "../__generated__/api-types";

export default function Loans () {
    const { isLoading, isSuccess, isError, error, data} = useQuery<LoanResponseDto[], Error>(["loans"], getLoansFn)

      
    return <>
        { isLoading 
        ? <CircularLoader /> 
        :  isError && axios.isAxiosError(error)
        ? <NotFound message={error?.response?.data} />  
        : isSuccess 
        ? <DataGridTable<LoanHistoryResponseDto> rows={data} headerName="Loans" />
        : <></>
        }
    </>   
}