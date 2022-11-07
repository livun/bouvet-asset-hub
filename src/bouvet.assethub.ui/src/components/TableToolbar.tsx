import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, FormControlLabel, Grid, IconButton, InputLabel, MenuItem, Select, SelectChangeEvent, Stack, Switch, TextField, Tooltip, Typography } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { useLocation } from "react-router-dom";
import { getAssetsFn, postAssetsFn, putAssetsFn } from "../api/assetsApi";
import queryClient from "../config/queryClient";
import { routeMapper, statusChecker, statusMapper } from "../utils/mappers";
import { AssetResponseDto, CategoryResponseDto, CreateAssetCommand, CreateLoanCommand, Status, UpdateAssetsByIdCommand } from "../__generated__/api-types";
import AlertBar from "./AlertBar";
import AddIcon from '@mui/icons-material/Add';
import { getCategoriesFn } from "../api/categoriesApi";
import CircularLoader from "./CircularLoader";
import { postLoansFn } from "../api/loansApi";
import { StatusEnum } from "../utils/enums";
import { DesktopDatePicker } from "@mui/x-date-pickers";


export default function TableToolbar(props: { changeStatus: boolean, updateAssetsCommand: UpdateAssetsByIdCommand, removeSelectedModel: () => void }) {
    const today = new Date().toISOString()
    const { changeStatus, updateAssetsCommand, removeSelectedModel } = props
    const location = useLocation();
    const pathname = location.pathname
    const [newStatusString, setNewStatusString] = useState("")
    const [assetForm, setAssetForm] = useState<CreateAssetCommand>({})
    const [loanForm, setLoanForm] = useState<CreateLoanCommand>({...assetForm, intervalStart: today})

    // Queries
    const assetsQuery = useQuery<AssetResponseDto[], Error>(["assets"], getAssetsFn, {
        select: (assets) => assets.filter((asset) => asset.status !== 2 && asset.status !== 3),
    })
    const categoriesQuery = useQuery<CategoryResponseDto[], Error>(["categories"], getCategoriesFn)

    // Mutations
    const addAsset = useMutation(() => postAssetsFn(assetForm), {
        onError: () => {
            openAlertBar("Cannot add asset.", false)
        },
        onSuccess: () => {
            queryClient.invalidateQueries(["assets"])
            setAssetForm({})
            setOpenAddAsset(false)
            openAlertBar("Asset is added.", true)
        }
    });
    const addLoan = useMutation(() => postLoansFn(loanForm), {
        onError: () => {
            openAlertBar("Cannot add loan.", false)
        },
        onSuccess: () => {
            queryClient.invalidateQueries(["loans"])
            setLoanForm({...assetForm, intervalStart: today})
            setOpenAddLoan(false)
            openAlertBar("Loan is added.", true)
        }
    });
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
        const dto: UpdateAssetsByIdCommand = updateAssetsCommand
        const newStatus: Status = Number(newStatusString) as Status
        dto.status = newStatus
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


    // Dialogs
    const [openAddAsset, setOpenAddAsset] = useState(false);
    const [openAddLoan, setOpenAddLoan] = useState(false);
    const handleOpenDialogs = () => {
        if (location.pathname === "/assets") {
            setOpenAddAsset(true)
        }
        if (location.pathname === "/loans") {
            setOpenAddLoan(true)
        }
    }
    const handleStartDateChange = (newValue: string | null) => {
        if(newValue) {
            console.log("startdate:", newValue)
            setLoanForm({...loanForm, intervalStart: newValue})
        }
    };
    const handleStopDateChange = (newValue: string | null) => {
        if(newValue) {
            console.log("startDate : " , newValue)
            console.log(typeof(newValue))
            setLoanForm({...loanForm, intervalStop: newValue})
        }
    };
   

    return <>
        <Box sx={{ borderBottom: 1, borderColor: 'divider', p: 1 }}>
            <Grid container alignItems="center" sx={{ paddingTop: 0.5 }}>
                <Grid item flexGrow={1}>
                    <Grid container alignItems="center">
                        <Grid item paddingRight={2}>
                            <Typography variant='h4'>
                                {routeMapper[pathname]}

                            </ Typography>
                        </Grid>
                        <Grid item>
                            <Tooltip title={`Add ${routeMapper[location.pathname]}`}>
                                <IconButton onClick={() => handleOpenDialogs()} aria-label="edit" size="large">
                                    <AddIcon sx={{ color: "black" }} fontSize="large" />
                                </IconButton>
                            </Tooltip>
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
        {categoriesQuery !== undefined
            ? <Dialog open={openAddAsset} onClose={() => setOpenAddAsset(false)}>
                <DialogTitle>Add asset</DialogTitle>
                <DialogContent>
                    <Stack spacing={3} paddingTop={2} component="form" autoComplete="off" width={400} >
                        <TextField
                            fullWidth
                            type="number"
                            label="Serial Number"
                            value={assetForm.serialNumberValue}
                            onChange={(event) => setAssetForm({ ...assetForm, serialNumberValue: Number(event?.target.value) })}
                        />
                        <TextField
                            fullWidth
                            select
                            label="Category"
                            value={assetForm.categoryId}
                            onChange={(event) => setAssetForm({ ...assetForm, categoryId: Number(event?.target.value) })}
                        >
                            {categoriesQuery.data?.map((cat) => (
                                <MenuItem key={cat.id} value={cat.id}>
                                    {cat.name}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenAddAsset(false)}>Cancel</Button>
                    <Button onClick={() => addAsset.mutate()}>Save</Button>
                </DialogActions>
            </Dialog>
            : <CircularLoader />}
        {assetsQuery !== undefined
            ?
            <Dialog open={openAddLoan} onClose={() => setOpenAddLoan(false)}>
                <DialogTitle>Add loan</DialogTitle>
                <DialogContent>
                    <Stack spacing={3} paddingTop={2} width={400} component="form" autoComplete="off">
                        <DesktopDatePicker
                            inputFormat="DD/MM/YYYY"
                            label="Start date"
                            value={loanForm.intervalStart}
                            minDate={new Date().toISOString()}
                            onChange={handleStartDateChange} 
                            renderInput={(params) => <TextField {...params} />}
                        />
                        {!loanForm.intervalIsLongterm ? <DesktopDatePicker
                            inputFormat="DD/MM/YYYY"
                            value={loanForm.intervalStop}
                            label="Stop date"
                            minDate={loanForm.intervalStart}
                            onChange={handleStopDateChange}
                            renderInput={(params) => <TextField {...params} />}
                        /> : <></>}
                        <FormControlLabel control={
                            <Switch
                                checked={loanForm.intervalIsLongterm}
                                onChange={(event: React.ChangeEvent<HTMLInputElement>) => setLoanForm({ ...loanForm, intervalStop: undefined, intervalIsLongterm: event?.target.checked })} />}
                            label="Longterm" />
                        <TextField
                            fullWidth
                            label="Assigned to"
                            type="number"
                            value={loanForm.assignedToValue}
                            onChange={(event) => setLoanForm({ ...loanForm, assignedToValue: Number(event?.target.value) })}
                        />
                        <TextField
                            fullWidth
                            select
                            label="Asset"
                            value={loanForm.assetId}
                            onChange={(event) => setLoanForm({ ...loanForm, assetId: Number(event?.target.value) })}
                        >
                            {assetsQuery.data?.map((asset) => (
                                <MenuItem key={asset.id} value={asset.id}>
                                    {asset.id}, {asset.categoryName}, {StatusEnum[statusChecker(asset.status)]}
                                </MenuItem>
                            ))}
                        </TextField>
                        <TextField
                            fullWidth
                            label="BSD Reference"
                            value={loanForm.bsdReference}
                            onChange={(event) => setLoanForm({ ...loanForm, bsdReference: event?.target.value })}
                        />
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenAddLoan(false)}>Cancel</Button>
                    <Button onClick={() => addLoan.mutate()}>Save</Button>
                </DialogActions>
            </Dialog>
            : <CircularLoader />}
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />
    </>


}
