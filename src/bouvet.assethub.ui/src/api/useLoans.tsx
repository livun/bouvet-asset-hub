import { CreateLoanCommand, LoanResponseDto, UpdateLoanDto } from "../__generated__/api-types"
import { ApiResponse, useApi, useDeleteApi, useGetApi } from "./useApi"

export function useGetLoans(): ApiResponse<LoanResponseDto[]> {
    return useGetApi<LoanResponseDto[]>("/loans")
};

export function usePostLoans(dto: CreateLoanCommand): ApiResponse<LoanResponseDto> {
    return useApi<CreateLoanCommand, LoanResponseDto>("/loans", dto, "POST")
};

export function usePutLoanById(id: number, dto: UpdateLoanDto): ApiResponse<LoanResponseDto> {
    return useApi<UpdateLoanDto, LoanResponseDto>(`/loans/${id}`, dto, "PUT")
};

export function useGetLoanById(id: number): ApiResponse<LoanResponseDto> {
    return useGetApi<LoanResponseDto>(`/loans/${id}`)
};
export function useGetLoansByEmployeeNumber(number: number): ApiResponse<LoanResponseDto[]> {
    return useGetApi<LoanResponseDto[]>(`/employees/${number}/loans`)
};
export function useGetLoanByAssetId(id: number): ApiResponse<LoanResponseDto> {
    return useGetApi<LoanResponseDto>(`/assets/${id}/loans`)
};
export function useDeleteLoan(id: number): ApiResponse<LoanResponseDto> {
    return useDeleteApi<LoanResponseDto>(`/loans/${id}`)
};

