import { useQuery } from "@tanstack/react-query";
import { useParams } from "react-router-dom"
import { getLoanByIdFn } from "../api/loansApi";
import { LoanResponseDto } from "../__generated__/api-types";

export default function Loan () {
   
    const params = useParams()
   
 
    const id : number = +params.id! ?? 0
  
    const { isLoading, isSuccess, isError, error, data} = useQuery<LoanResponseDto, Error>(["loan"], () => getLoanByIdFn(id))


    
    
return <>
     <div> hello from loan, id is {params.id} </div>
</>   
}