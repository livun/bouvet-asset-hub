import { DataGrid, GridRowId, GridRowParams, GridSelectionModel } from '@mui/x-data-grid';
import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { TableProps } from '../utils/props';
import { formatGridColumnsDefinition } from '../utils/tableFormatter';
import { UpdateAssetsByIdCommand } from '../__generated__/api-types';
import AlertBar from './AlertBar';
import CircularLoader from './CircularLoader';
import TableToolbar from './TableToolbar';

export default function DataGridTable<T extends Object>(props: TableProps<T>) {
    const { rows } = props
    const location = useLocation();
    const pathname = location.pathname
    
    const [selectedIds, setSelectedIds] = useState<GridRowId[]>()
    const [selectionModel, setSelectionModel] = useState(selectedIds);
    const [changeStatus, setChangeStatus] = useState(false);
    const [updateAssetsCommand, setUpdateAssetsCommand] = useState<UpdateAssetsByIdCommand>({})
    
    const [pageSize, setPageSize] = useState<number>(10);
    const [gridColDef, status] = formatGridColumnsDefinition(rows[0], pathname)

    const handleChange = () => {
        if (selectedIds !== undefined && selectedIds?.length > 0) {
            const ids: number[] = selectedIds.map(id => { return Number(id) })
            const command: UpdateAssetsByIdCommand = {
                ids: ids,
            }
            setUpdateAssetsCommand(command)
        }
    };

    useEffect(() => {
        handleChange()
        if (selectedIds !== undefined && selectedIds?.length > 0) {
            setChangeStatus(true)
        } else {
            setChangeStatus(false)
        }
        if (status === 200){
            openAlertBar("Asset was successfully deleted", true)    
        } 
        if (status === 404) {
            openAlertBar("Asset could not be deleted, used in loan!", false)
        }
    }, [selectedIds, pathname, status])

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

    //AlertComponent (if reused, this must be pasted in parent component)
	const [open, setOpen] = useState(false);
    const [alertBarMsg, setAlertBarMsg] = useState("")
    const [success, setSuccess] = useState(false)
	const openAlertBar = (msg: string, isSuccess: boolean) => {
        setAlertBarMsg(msg)
        setOpen(true);
        setSuccess(isSuccess)
	};
	const handleClose = (event?: React.SyntheticEvent | Event, reason?: string) => {
		if (reason === 'clickaway') {
			return;
		}
		setOpen(false);
	};

    return <>{pathname !== undefined ?
        <div style={{ display: "flex", height: "100%" }}>
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
                    isRowSelectable={(params: GridRowParams) => params.row.status !== 2} // 2 is Unavailable
                    components={{ Toolbar: TableToolbar }}
                    componentsProps={{ toolbar: { changeStatus, updateAssetsCommand, removeSelectedModel } }}
                />
            </div>
        </div> : <CircularLoader />}
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />
    </>
};