import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { getLoanHistoryFn,  } from "../api/loanHistoryApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/DataGridTable";
import { LoanHistoryResponseDto, LoanResponseDto } from "../__generated__/api-types";

export default function LoanHistory () {
    const { isLoading, isSuccess, isError, error, data} = useQuery<LoanHistoryResponseDto[], Error>(["loanHistory"], getLoanHistoryFn)


   
    return <>
    { isLoading 
    ? <CircularLoader /> 
    :  isError && axios.isAxiosError(error)
    ? <NotFound message={error?.response?.data} />  
    : isSuccess 
    ? <DataGridTable<LoanResponseDto> rows={data} headerName="Loan History"/>
    : <></>
    }
</>   
    
}