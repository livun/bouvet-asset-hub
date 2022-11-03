import { useParams } from "react-router-dom"
import { useGetAssetById } from "../api/useAssets";
import { useGetLoanById } from "../api/useLoans";

export default function Loan () {
   
    const params = useParams()
   
 
    const id : number = +params.id! ?? 0
  
    const {status, statusText, data, error, loading} = useGetLoanById(id);

    
    
return <>
     <div> hello from loan, id is {params.id} </div>
</>   
}