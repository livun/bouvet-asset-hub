import apiClient from "../config/apiClient";
import { LoanResponseDto, CreateLoanCommand, UpdateLoanDto} from "../__generated__/api-types";
import { deleteItem, formHeaders, get, postItem, putItem, regularHeaders } from "./genericAxios";


export const getLoansFn = async () => {
  return await get<LoanResponseDto[]>(`/loans`)
};
export const getLoanByIdFn = async (id: number) => {
  return await get<LoanResponseDto>(`/loans/${id}`)
};
export const getLoanByEmployeeNumberFn = async (number: number) => {
  return await get<LoanResponseDto[]>(`/employees/${number}/loans`)
};
export const getLoanByAssetIdFn = async (id: number) => {
  return await get<LoanResponseDto>(`/assets/${id}/loans`)
};
export const postLoansFn = async (dto: CreateLoanCommand) => {
  return await postItem<CreateLoanCommand, LoanResponseDto>(`/loans`, dto, regularHeaders)
};

export const putLoanFn = async (id: number, dto: UpdateLoanDto) => {
  return await putItem<UpdateLoanDto, LoanResponseDto>(`/loans/${id}`, dto, regularHeaders)
};

export const deleteLoandFn = async (id: number) => {
  return await deleteItem<LoanResponseDto>(`/loans/${id}`, regularHeaders)
};
