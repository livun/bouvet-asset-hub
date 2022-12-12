import { DataGrid, GridRowId, GridRowParams, GridSelectionModel } from '@mui/x-data-grid';
import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { TableProps } from '../utils/props';
import { formatGridColumnsDefinition } from '../utils/columnFormatter';
import CircularLoader from './CircularLoader';
import TableToolbar from './TableToolbar';
import { Status } from '../_generated/api-types';

export default function DataGridTable<T extends Object>(props: TableProps<T>) {
    const { rows, headerName } = props
    const location = useLocation();
    const pathname = location.pathname
    const [selectedIds, setSelectedIds] = useState<GridRowId[]>()
    const [selectionModel, setSelectionModel] = useState(selectedIds);
    const [changeStatus, setChangeStatus] = useState(false);
    const [updateAssetsIds, setUpdateAssetsIds] = useState<number[]>([])
    const [pageSize, setPageSize] = useState<number>(30);
    const gridColDef = formatGridColumnsDefinition(rows[0], pathname)

    const handleChange = () => {
        if (selectedIds !== undefined && selectedIds?.length > 0) {
            const ids: number[] = selectedIds.map(id => { return Number(id) })
            setUpdateAssetsIds(ids)
        }
    };

    useEffect(() => {
        handleChange()
        if (selectedIds !== undefined && selectedIds.length > 0) {
            setChangeStatus(true)
        } else {
            setUpdateAssetsIds([])
            setChangeStatus(false)
        }
       
    }, [selectedIds, pathname])

    // Handle the SelectionModel (GridRowId[])
    const removeSelectedModel = () => {
        handleSelection(null)
    }
    const handleSelection = (newSelection: GridSelectionModel | null) => {
        if (newSelection) {
            setSelectionModel(newSelection)
        }
        else {
            setSelectionModel([])
        }
    }
    return <>
        {pathname !== undefined
            ? <div style={{ display: "flex", height: "100%" }}>
                <div style={{ flexGrow: 1 }}>
                    <DataGrid
                        rows={rows}
                        rowHeight={40}
                        columns={gridColDef}
                        pageSize={pageSize}
                        onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
                        rowsPerPageOptions={[10, 30, 50, 70, 100]}
                        checkboxSelection={pathname === "/assets" ? true : false}
                        onSelectionModelChange={(selectionModel) => {
                            setSelectedIds(selectionModel)
                            handleSelection(selectionModel)
                        }}
                        selectionModel={selectionModel}
                        isRowSelectable={(params: GridRowParams) => params.row.status !== Status.Unavailable}
                        components={{ Toolbar: TableToolbar }}
                        componentsProps={{
                            toolbar: {
                                changeStatus: changeStatus,
                                updateAssetsIds: updateAssetsIds,
                                removeSelectedModel: removeSelectedModel,
                                headerName: headerName
                            }
                        }}
                    />
                </div>
            </div> 
            : <CircularLoader />
        }
    </>
};