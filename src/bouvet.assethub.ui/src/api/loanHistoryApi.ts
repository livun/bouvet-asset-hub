import { LoanHistoryResponseDto } from "../_generated/api-types";
import { get } from "./genericAxios";

export const getLoanHistoryFn = async () => {
  return await get<LoanHistoryResponseDto[]>(`/loanhistory`)
};