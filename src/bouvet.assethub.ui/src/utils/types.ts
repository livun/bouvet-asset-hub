import { GridColDef } from "@mui/x-data-grid"
import Asset from "../views/Asset"
import { AssetResponseDto, CategoryResponseDto, LoanHistoryResponseDto, LoanResponseDto } from "../__generated__/api-types"

export type formatResult = {
    filter : () => boolean,
    value :  () => GridColDef

}


export const StatusArray = ["Registered", "Available", "Discontinued"]

export type testType = {category: true, status: true}
export interface IReadOnly {isReadOnly: boolean}
export interface ILoanActions{extendLoan: boolean, handInLoan: boolean}