import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { getLoansFn } from "../api/loansApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/DataGridTable";
import { LoanResponseDto } from "../__generated__/api-types";
import SpeedDialAddItemsMenu from "../components/SpeedDialAddItemsMenu";


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
        <SpeedDialAddItemsMenu />
    </>   
}