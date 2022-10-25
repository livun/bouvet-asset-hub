import { useState } from "react"
import { Client, CreateLoanCommand, LoanResponseDto, UpdateLoanDto } from "../__generated__/api-client";
import { client } from "./Client";

const useLoans = () => {

    const [loans, setLoans] = useState<LoanResponseDto[]>();
    const [loan, setLoan] = useState<LoanResponseDto>();
    const [loansByAsset, setLoansByAsset] = useState<LoanResponseDto>();
    const [loansByEmployee, setLoansByEmployee] = useState<LoanResponseDto[]>();
    
    
    const getLoans = async () => {
        const data: LoanResponseDto[] = await client.loansAll2();
        setLoans(data);

    };
    const getLoanByAssetId = async (id: number) => {
        const data : LoanResponseDto = await client.loansGET(id)
        setLoansByAsset(data);
        
    }
    const getLoanByEmployeeId = async (number: number) => {
        const data : LoanResponseDto[] = await client.loansAll(number)
        setLoansByEmployee(data);
        
    }
    const postLoan = async (dto : CreateLoanCommand) => {
        return await client.loansPOST(dto);
    }
    
    const putLoan = async (id: number, dto : UpdateLoanDto) => {
        return await client.loansPUT(id, dto);
    }
    const getLoanById = async (id: number) => {
        const data: LoanResponseDto = await client.loansGET2(id);
        setLoan(data);
    }
 
    const deleteLoan = async (id : number) => {
        return await client.loansDELETE(id);
    }
    

    return {loans, loan, loansByAsset, loansByEmployee, getLoans, getLoanByAssetId, getLoanByEmployeeId, postLoan, getLoanById, putLoan, deleteLoan };

};

export default useLoans;

