import { Box, Button, FormControl, Grid, InputLabel, MenuItem, Select, SelectChangeEvent, Typography } from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import { useState } from "react";
import { useLocation } from "react-router-dom";
import { putAssetsFn } from "../api/assetsApi";
import queryClient from "../config/queryClient";
import {  Status, UpdateAssetsByIdCommand } from "../__generated__/api-types";
import AlertBar from "./AlertBar";
import { TableToolbarProps } from "../utils/props";
import { headerIcons } from "../utils/mappers";

export default function TableToolbar(props: TableToolbarProps) {

    const { changeStatus, updateAssetsIds, removeSelectedModel, headerName } = props
    const location = useLocation();
    const pathname = location.pathname
    const [newStatusString, setNewStatusString] = useState("")
    
    const updateAssets = useMutation((dto: UpdateAssetsByIdCommand) => putAssetsFn(dto), {
        onError: () => {
            openAlertBar("Cannot update status, asset is Unavailable.", false)
        },
        onSuccess: () => {
            queryClient.invalidateQueries(["assets"])
            removeSelectedModel()
            openAlertBar("Assets status successfully updated.", true)
        }
    });

    const handleUpdateStatus = () => {
        const dto: UpdateAssetsByIdCommand = { ids: updateAssetsIds, status: Number(newStatusString) as Status }
        updateAssets.mutate(dto);
        setNewStatusString("")
    }

    //AlertComponent handling (if reused, this must be pasted in parent component)
    const [open, setOpen] = useState(false);
    const [alertBarMsg, setAlertBarMsg] = useState("")
    const [success, setSuccess] = useState(false)
    const openAlertBar = (msg: string, isSuccess: boolean) => {
        setAlertBarMsg(msg)
        setSuccess(isSuccess)
        setOpen(true);

    };
    const handleClose = (event?: React.SyntheticEvent | Event, reason?: string) => {
        if (reason === 'clickaway') {
            return;
        }
        setOpen(false);
    };
    

    return <>
        <Box sx={{ borderBottom: 1, borderColor: 'divider', p: 1 }}>
            <Grid container alignItems="center" sx={{ paddingTop: 0.5 }}>
                <Grid item flexGrow={1}>


                <Grid container alignItems="center">
                <Grid item sx={{paddingTop: 0.5}}>
                                    
                {headerIcons[headerName]}

                        </Grid>
                        <Grid item paddingLeft={2}>
                            <Typography variant='h4'>
                            {headerName}
                            </ Typography>
                        </Grid>
                        

                    </Grid>
                </Grid>

                <Grid item>
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
                                        onChange={(event: SelectChangeEvent) => setNewStatusString(event.target.value)}
                                    >
                                        <MenuItem value={0}>Registered</MenuItem>
                                        <MenuItem value={1}>Available</MenuItem>
                                        <MenuItem value={3}>Discontinued</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item>
                                <Button variant="outlined" onClick={handleUpdateStatus}> Update Status</Button>
                            </Grid>
                        </Grid>
                        : <></>}
                </Grid>
            </Grid>
        </Box>
    
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />
    </>
}
