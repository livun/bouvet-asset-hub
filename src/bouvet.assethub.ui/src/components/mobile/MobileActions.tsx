import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControlLabel, Grid, MenuItem, Stack, Switch, TextField } from "@mui/material";
import { AssetResponseDto, CreateLoanDto, Status, UpdateAssetDto, UpdateLoanDto, LoanResponseDto } from "../../_generated/api-types";
import GridViewSharpIcon from '@mui/icons-material/GridViewSharp';
import { DesktopDatePicker, MobileDatePicker } from "@mui/x-date-pickers";
import { useState } from "react";
import { useMutation, useQuery } from "@tanstack/react-query";
import { deleteLoanFn, postLoansFn, getLoanByAssetIdFn, putLoanFn } from "../../api/loansApi";
import AlertBar from "../AlertBar";
import queryClient from "../../config/queryClient";
import { getAssetByIdFn, putAssetByIdFn } from "../../api/assetsApi";
import CircularLoader from "../CircularLoader";
import { useNavigate } from "react-router-dom";

export default function MobileActions(prop: { assetId: number }) {
    const { assetId } = prop
    const [loanId, setLoanId] = useState<number>()
    const today = new Date().toISOString()
    const navigate = useNavigate()

    const [openAddLoan, setOpenAddLoan] = useState(false)
    const [openExtendLoan, setOpenExtendLoan] = useState(false)
    const [openUpdateStatus, setOpenUpdateStatus] = useState(false)
    const [openHandInLoan, setOpenHandInLoan] = useState(false)
    const [loanForm, setLoanForm] = useState<CreateLoanDto>({ intervalStart: today, assetId: assetId })
    const [assetForm, setAssetForm] = useState<UpdateAssetDto>({})
    const [stopDate, setStopDate] = useState<Date | null>(new Date())

    //queries
    const { isLoading, isSuccess, isError, error, data } = useQuery<AssetResponseDto, Error>(["asset", assetId], () => getAssetByIdFn(assetId))

    const loanQuery = useQuery<LoanResponseDto, Error>(["loan", assetId], () => getLoanByAssetIdFn(assetId), {
        onSuccess: (data) => setLoanId(data.id),
        enabled: !!assetId

    })
    //mutations 
    const addLoan = useMutation(() => postLoansFn(loanForm), {
        onError: () => {
            openAlertBar("Cannot add loan.", false)
        },
        onSuccess: (data) => {
            queryClient.invalidateQueries(["asset", assetId])
            queryClient.invalidateQueries(["loan", assetId])
            setLoanForm({ ...loanForm, intervalStart: today })
            setOpenAddLoan(false)
            openAlertBar("Loan is added.", true)

        }
    });

    const updateAsset = useMutation(() => putAssetByIdFn(assetId, assetForm), {
        onError: () => {
            openAlertBar("Cannot update asset, asset is Unavailable.", false)
        },
        onSuccess: () => {
            queryClient.invalidateQueries(["asset", assetId])
            openAlertBar("Asset successfully updated.", true)
            setOpenUpdateStatus(false)
        }
    });

    const extendLoan = useMutation((dto: UpdateLoanDto) => putLoanFn(loanId!, dto), {
        onError: () => openAlertBar("Loan cannot be extended", false),
        onSuccess: () => {
            queryClient.invalidateQueries(["loan", assetId])
            openAlertBar("Loan is sucessfully extended.", true)
        }
    });
    const deleteLoan = useMutation(() => deleteLoanFn(loanId!), {
        onError: () => openAlertBar("Cannot delete loan, an error occurred.", false),
        onSuccess: () => {
            queryClient.invalidateQueries(["loans"])
            openAlertBar("Loan is successfully handed in.", true)
            setTimeout(() => {
                navigate(0)
            }, 1500)
        }
    });



    const handleStartDateChange = (newValue: string | null) => {
        if (newValue) {
            setLoanForm({ ...loanForm, intervalStart: newValue })
        }
    };
    const handleStopDateChange = (newValue: string | null) => {
        if (newValue) {
            setLoanForm({ ...loanForm, intervalStop: newValue })
        }
    };

    const handleDateChange = (newValue: Date | null) => {
        if (newValue) {
            setStopDate(newValue)
        }
    };
    const handleExtendLoan = () => {
        if (stopDate && loanId) {
            extendLoan.mutate({ intervalStop: stopDate.toISOString() })
        }
        setOpenExtendLoan(false)
    }

    const handleHandInLoan = () => {
        if (loanId) {
            deleteLoan.mutate()
            setOpenHandInLoan(false)
        }

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
        {isLoading ?? <CircularLoader />}
        {isSuccess && data !== undefined ? <Grid container spacing={2} direction="column" alignItems="center"  >
            <Grid width={250} textAlign="center" item>
                <GridViewSharpIcon color="primary" sx={{ fontSize: 150 }} />
            </Grid>
            <Grid width={250} item  >
                <Stack spacing={1} component="form" autoComplete="off">
                    <Grid container  >
                        <Grid item xs={5}>
                            <TextField
                                disabled
                                fullWidth
                                value="Id"
                                variant="standard"
                            />
                        </Grid>
                        <Grid item xs={7}>
                            <TextField
                                fullWidth
                                value={data?.id}
                                variant="standard"
                                InputProps={{
                                    readOnly: false,
                                }}
                            />
                        </Grid>
                    </Grid>
                    <Grid container>
                        <Grid item xs={5}>
                            <TextField
                                disabled
                                fullWidth
                                value="Category"
                                variant="standard"
                            />
                        </Grid>
                        <Grid item xs={7}>
                            <TextField
                                fullWidth
                                value={data?.categoryName}
                                variant="standard"
                                InputProps={{
                                    readOnly: true,
                                }}
                            />
                        </Grid>
                    </Grid>
                    <Grid container>
                        <Grid item xs={5}>
                            <TextField
                                disabled
                                fullWidth
                                value="Status"
                                variant="standard"
                            />
                        </Grid>
                        <Grid item xs={7}>
                            <TextField
                                fullWidth
                                value={data?.status}
                                variant="standard"
                                InputProps={{
                                    readOnly: true,
                                }}
                            />
                        </Grid>
                    </Grid>
                    {loanQuery.data !== undefined ?
                        <>
                            <Grid container>
                                <Grid item xs={5}>
                                    <TextField
                                        disabled
                                        fullWidth
                                        value="Loan Start"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={new Date(loanQuery.data.intervalStart!).toLocaleDateString()}
                                        variant="standard"
                                        InputProps={{
                                            readOnly: true,
                                        }}
                                    />
                                </Grid>
                            </Grid>
                            { !loanQuery.data.intervalIsLongterm ? 
                            <Grid container>
                                <Grid item xs={5}>
                                    <TextField
                                        disabled
                                        fullWidth
                                        value="Loan Stop"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={new Date(loanQuery.data.intervalStop!).toLocaleDateString()}
                                        variant="standard"
                                        InputProps={{
                                            readOnly: true,
                                        }}
                                    />
                                </Grid>
                            </Grid>
                            :
                            <Grid container>
                                <Grid item xs={5}>
                                    <TextField
                                        disabled
                                        fullWidth
                                        value="Loan Stop"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={"Longterm loan"}
                                        variant="standard"
                                        InputProps={{
                                            readOnly: true,
                                        }}
                                    />
                                </Grid>
                            </Grid>
                            
                            }
                        </>
                        : <></>}

                </Stack>
            </Grid>
            {data.status !== Status.Unavailable ? <Grid width={250} item marginTop={3}>
                <Button fullWidth size="large" variant="outlined" onClick={() => setOpenUpdateStatus(true)}>Update Status</Button>
            </Grid> : <></>}
            {data.status !== Status.Unavailable ? <Grid width={250} item >
                <Button fullWidth size="large" variant="outlined" onClick={() => setOpenAddLoan(true)}>New loan</Button>
            </Grid> : <></>}
            {data.status === Status.Unavailable ? <Grid width={250} item marginTop={3} >
                <Button fullWidth size="large" variant="outlined" onClick={() => setOpenHandInLoan(true)}>Hand in loan</Button>
            </Grid> : <></>}
            {data.status === Status.Unavailable && !loanQuery.data?.intervalIsLongterm ? <Grid width={250} item flexGrow={1} >
                <Button fullWidth size="large" variant="outlined" onClick={() => setOpenExtendLoan(true)}>Extend loan</Button>
            </Grid> : <></>}

        </Grid>
            : <></>}
        <Dialog open={openAddLoan} onClose={() => setOpenAddLoan(false)}>
            <DialogTitle>New loan</DialogTitle>
            <DialogContent>
                <Stack spacing={3} paddingTop={2} width={250} component="form" autoComplete="off">
                    <MobileDatePicker
                        inputFormat="DD/MM/YYYY"
                        label="Start date"
                        value={loanForm.intervalStart}
                        minDate={new Date().toISOString()}
                        disableFuture
                        onChange={handleStartDateChange}
                        renderInput={(params) => <TextField {...params} />}
                    />
                    {!loanForm.intervalIsLongterm
                        ? <MobileDatePicker
                            inputFormat="DD/MM/YYYY"
                            value={loanForm.intervalStop}
                            label="Stop date"
                            minDate={loanForm.intervalStart}
                            onChange={handleStopDateChange}
                            renderInput={(params) => <TextField {...params} />} />
                        : <></>}
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
                        label="Asset"
                        value={loanForm.assetId}
                    />
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
        <Dialog open={openUpdateStatus} onClose={() => setOpenUpdateStatus(false)}>
            <DialogTitle>Update status</DialogTitle>
            <DialogContent>
                <Stack spacing={3} paddingTop={2} width={250} component="form" autoComplete="off">

                    <TextField
                        label="Status"
                        fullWidth
                        select
                        value={assetForm.status}
                        variant="standard"
                        onChange={(event) => setAssetForm({ status: Status[event.target.value as Status] })}

                    >
                        <MenuItem value={Status.Registered}>{Status.Registered}</MenuItem>
                        <MenuItem value={Status.Available}>{Status.Available}</MenuItem>
                        <MenuItem value={Status.Unavailable}>{Status.Unavailable}</MenuItem>
                        <MenuItem value={Status.Discontinued}>{Status.Discontinued}</MenuItem>
                    </TextField>
                </Stack>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => setOpenUpdateStatus(false)}>Cancel</Button>
                <Button onClick={() => updateAsset.mutate()}>Save</Button>
            </DialogActions>
        </Dialog>
        {loanQuery.data?.intervalStart !== undefined ? <> <Dialog open={openExtendLoan} onClose={() => setOpenExtendLoan(false)}>
            <DialogTitle>Extend loan</DialogTitle>
            <DialogContent>
                <Stack spacing={3} paddingTop={2}>
                    <DesktopDatePicker
                        inputFormat="DD/MM/YYYY"
                        value={stopDate}
                        minDate={new Date(loanQuery.data.intervalStart)}
                        onChange={handleDateChange}
                        renderInput={(params) => <TextField {...params} />}
                    />
                </Stack>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => setOpenExtendLoan(false)}>Cancel</Button>
                <Button onClick={() => handleExtendLoan()}>Save</Button>
            </DialogActions>
        </Dialog>
            <Dialog open={openHandInLoan} onClose={() => setOpenHandInLoan(false)}>
                <DialogTitle>Hand in loan</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Are you sure you want to hand in the loan?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenHandInLoan(false)}>Cancel</Button>
                    <Button onClick={() => handleHandInLoan()}>Confirm</Button>
                </DialogActions>
            </Dialog>
        </>
            : <></>}

        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />


    </>
}