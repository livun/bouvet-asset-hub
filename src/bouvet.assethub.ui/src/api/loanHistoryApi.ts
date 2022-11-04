import { LoanHistoryResponseDto } from "../__generated__/api-types";
import { get } from "./genericAxios";


export const getLoanHistoryFn = async () => {
  return await get<LoanHistoryResponseDto[]>(`/loanhistory`)
};

