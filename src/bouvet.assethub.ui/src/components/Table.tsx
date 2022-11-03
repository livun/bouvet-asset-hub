import { Box, Button, FormControl, Grid, InputLabel, MenuItem, OutlinedInput, Select, SelectChangeEvent, TextField, Typography } from '@mui/material';
import { DataGrid, GridRowId } from '@mui/x-data-grid';
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
    const gridColDef = formatGridColumnsDefinition(rows[0])
    const [selectedIds, setSelectedIds] = useState<GridRowId[]>()
    const [changeStatus, setChangeStatus] = useState(false);
    const location = useLocation();
    const pathname = location.pathname
    const [updateAssetsCommand, setUpdateAssetsCommand] = useState<UpdateAssetsByIdCommand>({})

    const handleChange = () => {
        console.log("runs now")
        if (selectedIds !== undefined && selectedIds?.length > 0) {
            console.log(selectedIds)
            const ids : number[] = selectedIds.map(id => {return Number(id)})
            const command : UpdateAssetsByIdCommand = {
                ids: ids,
            }
            setUpdateAssetsCommand(command)
            setChangeStatus(true)
            console.log("tables", updateAssetsCommand)


        }

    };
    useEffect(() => {
        handleChange()
        
        if (selectedIds !== undefined && selectedIds?.length > 0) {
            setChangeStatus(true)
        } else {
            setChangeStatus(false)
        }
        
        console.log("selected ids", selectedIds)
    }, [selectedIds])

 

    return <>{pathname !== undefined ?
        <div style={{ display: "flex", height: "100%" }}>
            <div style={{ flexGrow: 1 }}>
                <DataGrid
                    //sx={{paddingRight: 2}}
                    rows={rows}
                    rowHeight={40}
                    columns={gridColDef}
                    pageSize={30}
                    rowsPerPageOptions={[30]}
                    checkboxSelection={pathname === "/assets" ? true : false}
                    onSelectionModelChange={(ids) => { setSelectedIds(ids) }}
                    components={{ Toolbar: TableToolbar }}
                    componentsProps={
                        { toolbar: { changeStatus, updateAssetsCommand }}
                    }
                    
                />
            </div>
        </div> : <CircularLoader />}
    </>
};