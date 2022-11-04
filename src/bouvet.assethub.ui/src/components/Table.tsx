import { Box, Button, FormControl, Grid, InputLabel, MenuItem, OutlinedInput, Select, SelectChangeEvent, TextField, Typography } from '@mui/material';
import { DataGrid, GridFooter, GridFooterContainer, GridRowId, GridRowParams, GridSelectionModel } from '@mui/x-data-grid';
import { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import { StatusEnum } from '../utils/enums';
import { TableProps } from '../utils/props';
import { capitalizeAndSplit, removeFirstCharacter } from '../utils/regex';
import { formatGridColumnsDefinition } from '../utils/tableFormatter';
import { UpdateAssetsByIdCommand } from '../__generated__/api-types';
import CircularLoader from './CircularLoader';
import TableToolbar from './TableToolbar';


export default function DataGridTable<T extends Object>(props: TableProps<T>) {
    const { rows } = props
    const [selectedIds, setSelectedIds] = useState<GridRowId[]>()
    const [changeStatus, setChangeStatus] = useState(false);
    const location = useLocation();
    const pathname = location.pathname
    const [updateAssetsCommand, setUpdateAssetsCommand] = useState<UpdateAssetsByIdCommand>({})
    const [pageSize, setPageSize] = useState<number>(10);
    const [selectionModel, setSelectionModel] = useState(selectedIds);
    const gridColDef = formatGridColumnsDefinition(rows[0], pathname)


    const handleChange = () => {
        if (selectedIds !== undefined && selectedIds?.length > 0) {
            const ids : number[] = selectedIds.map(id => {return Number(id)})
            const command : UpdateAssetsByIdCommand = {
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
        
        }, [selectedIds, pathname])

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
                    componentsProps={{ toolbar: { changeStatus, updateAssetsCommand, removeSelectedModel }}}
                    
                />
            </div>
        </div> : <CircularLoader />}
    </>
};