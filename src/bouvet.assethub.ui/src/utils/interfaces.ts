import { GridColDef } from "@mui/x-data-grid"

export interface IReadOnly { isReadOnly: boolean }

export interface ILoanActions { extendLoan: boolean, handInLoan: boolean }

export interface IAssetActions { isReadOnly: boolean, isDelete: boolean }

export interface IFormatInput {
    key: string,
    object?: object
}
export interface IFormatOutput {
    filter: () => boolean
    value: () => GridColDef | undefined
}

export interface IQrScannerProp {
    handleQrGuid: (qrGuid: string) => void
}