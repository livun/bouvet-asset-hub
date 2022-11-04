import { Box, Button, FormControl, Grid, InputLabel, MenuItem, Select, SelectChangeEvent, Typography } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { useLocation } from "react-router-dom";
import { putAssetsFn } from "../api/assetsApi";
import queryClient from "../config/queryClient";
import { routeMapper } from "../utils/mappers";
import { Status, UpdateAssetsByIdCommand } from "../__generated__/api-types";
import AlertBar from "./AlertBar";

export default function TableToolbar(props: { changeStatus: boolean, updateAssetsCommand: UpdateAssetsByIdCommand, removeSelectedModel: () => void  }) {
    const { changeStatus, updateAssetsCommand, removeSelectedModel } = props
    const location = useLocation();
    const pathname = location.pathname
    const [newStatusString, setNewStatusString] = useState("")


    const {
        mutate: updateAssets,
        isError,
      } = useMutation((dto: UpdateAssetsByIdCommand) => putAssetsFn(dto), {
        onError:() => openAlertBar(),
        onSuccess:()=> {
            queryClient.invalidateQueries(["assets"])
            removeSelectedModel()
        }

      });


    const handleChange = (event: SelectChangeEvent) => {
        setNewStatusString(event.target.value);

    };
   
    const handleUpdateStatus = () => {
        const dto : UpdateAssetsByIdCommand = updateAssetsCommand
        const newStatus : Status =  Number(newStatusString) as Status
        dto.status = newStatus
        updateAssets(dto); 
        setNewStatusString("")
    }

    //AlertComponent (if reused, this must be pasted in parent component)
	const [open, setOpen] = useState(false);
	const openAlertBar = () => {
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
            <Grid container sx={{paddingTop:0.5}}>
                <Grid xs={6} item>
                    <Grid container>
                        <Grid item paddingLeft={5}>
                            <Typography variant='h5' gutterBottom>
                                {routeMapper[pathname]}
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
        {isError ? <AlertBar open={open} handleClose={handleClose} message={"Cannot update status, asset is Unavailable."} success={false} /> : <></>}
    </>


}
