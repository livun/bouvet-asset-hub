import { useGetAssets } from "../api/useAssets";
import { useGetLoans } from "../api/useLoans";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import DataGridTable from "../components/Table";
import { AssetResponseDto, LoanResponseDto } from "../__generated__/api-types";

export default function Loans () {
    const {status, statusText, data, error, loading} = useGetLoans();
    const errorMsg = "There is no loans in database"
      
    return <>
    {status === 404 ? 
        <NotFound message={errorMsg}/> : data !== undefined ? <DataGridTable<LoanResponseDto> rows={data} /> : <CircularLoader />}
    {/* {data !== undefined ? <DataGridTable<LoanResponseDto> rows={data} /> : <CircularLoader />} */}
    
    
    </>
}