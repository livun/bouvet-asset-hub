import { Typography } from "@mui/material";
import { useGetAssets } from "../api/useAssets";
import { useGetLoanHistory } from "../api/useLoanHistory";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/Table";
import { LoanHistoryResponseDto, LoanResponseDto } from "../__generated__/api-types";

export default function LoanHistory () {
    const {status, statusText, data, error, loading} = useGetLoanHistory();
    const errorMsg = "Loanhistory table is empty"
   

   
    return <>
    {status === 404 ? 
        <NotFound message={errorMsg}/> : data !== undefined ? <DataGridTable<LoanHistoryResponseDto> rows={data} /> : <CircularLoader />}    
    </>
    
}