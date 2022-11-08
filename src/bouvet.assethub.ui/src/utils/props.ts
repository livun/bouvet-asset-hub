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