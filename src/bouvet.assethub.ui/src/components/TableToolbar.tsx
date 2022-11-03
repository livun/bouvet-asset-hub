import { Box, Button, FormControl, Grid, InputLabel, MenuItem, Select, SelectChangeEvent, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { usePutAssets } from "../api/useAssets";
import { capitalizeAndSplit, removeFirstCharacter } from "../utils/regex";
import { Status, UpdateAssetsByIdCommand } from "../__generated__/api-types";

export default function TableToolbar(props: { changeStatus: boolean, updateAssetsCommand: UpdateAssetsByIdCommand   }) {
    const { changeStatus, updateAssetsCommand } = props
    const location = useLocation();
    const pathname = location.pathname
    const [newStatusString, setNewStatusString] = useState("")
    const [ newUpdateAssetsCommand, setNewUpdateAssetsCommand] = useState<UpdateAssetsByIdCommand>({})

    

    const handleChange = (event: SelectChangeEvent) => {
        setNewStatusString(event.target.value);

    };
   
    const useHandleUpdateStatus = () => {
        const dto : UpdateAssetsByIdCommand = updateAssetsCommand
        const newStatus : Status =  Number(newStatusString) as Status
        dto.status = newStatus
        const {status, statusText, data, error, loading} = usePutAssets(dto);
        console.log(status, statusText, error )

       
        
       
    }
    useEffect(() => {
        console.log("hhelpooo", updateAssetsCommand)
    }, [updateAssetsCommand])

    return <>
        <Box sx={{ borderBottom: 1, borderColor: 'divider', p: 1 }}>
            <Grid container>
                <Grid xs={6} item>
                    <Grid container>
                        <Grid item paddingLeft={5}>
                            <Typography variant='h5' gutterBottom>
                                {capitalizeAndSplit(removeFirstCharacter(pathname))}
                            </ Typography>
                        </Grid>
                    </Grid>
                </Grid>
                <Grid xs={6} item>
                    {pathname === "/assets" && changeStatus === true ?
                        <Grid container justifyContent="flex-end" spacing={1} >
                            <Grid item>
                                <FormControl sx={{ ml: 1, minWidth: 120 }} size="small">
                                    <InputLabel >Status</InputLabel>
                                    <Select
                                        size='small'
                                        sx={{ height: "38px" }}
                                        value={newStatusString}
                                        label="Status"
                                        onChange={handleChange}
                                    >
                                        <MenuItem value={0}>Registred</MenuItem>
                                        <MenuItem value={1}>Available</MenuItem>
                                        <MenuItem value={2}>Unavailable</MenuItem>
                                        <MenuItem value={3}>Deprecated</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item>
                                <Button variant="outlined" onClick={useHandleUpdateStatus}> Update Status</Button>
                            </Grid>
                        </Grid>
                        : <></>}
                </Grid>
            </Grid>
        </Box>
    </>


}
