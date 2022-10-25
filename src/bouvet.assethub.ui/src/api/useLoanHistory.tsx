import { useState } from "react"
import { Client, LoanHistoryResponseDto } from "../__generated__/api-client";
import { client } from "./Client";

const useLoanHistory = () => {

    const [loanHistory, setLoanHistory] = useState<LoanHistoryResponseDto[]>();
        
    const getLoanHistory = async () => {
        const data: LoanHistoryResponseDto[] = await client.loanhistory();
        setLoanHistory(data);

    };

    return {loanHistory, getLoanHistory};

};

export default useLoanHistory;

