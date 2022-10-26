import { LoanHistoryResponseDto } from "../__generated__/api-types";
import { ApiResponse, useGetApi } from "./useApi";

export function useGetLoanHistory(): ApiResponse<LoanHistoryResponseDto[]> {
    return useGetApi<LoanHistoryResponseDto[]>("/loanhistory")
};
