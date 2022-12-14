import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { getLoansFn } from "../api/loansApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/DataGridTable";
import { LoanResponseDto } from "../_generated/api-types";
import AddItemsFAB from "../components/AddItemsFAB";

export default function Loans () {
    const { isLoading, isSuccess, isError, error, data} = useQuery<LoanResponseDto[], Error>(["loans"], getLoansFn)

    return <>
        { isLoading 
        ? <CircularLoader /> 
        :  isError && axios.isAxiosError(error)
        ? <NotFound message={error?.response?.data} />  
        : isSuccess 
        ? <DataGridTable<LoanResponseDto> rows={data} headerName="Loans" />
        : <></>
        }
        <AddItemsFAB />
    </>   
}