import { GridColDef } from "@mui/x-data-grid"

export type TableProps<T extends Object> = {
    rows: T[],
    headerName: string
}

export type TableToolbarProps = {
    changeStatus: boolean,
    updateAssetsIds: number[],
    removeSelectedModel: () => void
    headerName: string
}

export interface DeleteObject {
    confirmDelete: boolean,
    id: number
}

export interface TableButtons {
    col: any,
    deleteObject: DeleteObject
}

export interface ColumnsDefinition {
    colDef: GridColDef[],
    deleteObject: DeleteObject
}