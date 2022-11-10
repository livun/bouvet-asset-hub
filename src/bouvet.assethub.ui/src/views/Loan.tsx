import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, IconButton, InputAdornment, Stack, TextField, Tooltip, Typography } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom"
import { deleteLoanFn, getLoanByIdFn, putLoanFn } from "../api/loansApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import queryClient from "../config/queryClient";
import { ILoanActions,  } from "../utils/interfaces";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import AlertBar from "../components/AlertBar";
import CheckIcon from '@mui/icons-material/Check';
import ClearIcon from '@mui/icons-material/Clear';
import MoreTimeIcon from '@mui/icons-material/MoreTime';
import TaskAltIcon from '@mui/icons-material/TaskAlt';
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import SpeedDialAddItemsMenu from "../components/SpeedDialAddItemsMenu";
import { LoanResponseDto, UpdateLoanDto } from "../_generated/api-types";


export default function Loan() {
    const location = useLocation()
    const navigate = useNavigate()

    const [id] = useState(Number(useParams().id))
    const [loanActions] = useState<ILoanActions>(location.state as ILoanActions);
    const [stopDate, setStopDate] = useState<Date | null>(new Date())

    // Queries
    const { isLoading, isSuccess, isError, error, data } = useQuery<LoanResponseDto, Error>(["loan", id], () => getLoanByIdFn(id))

    //Mutations
    const extendLoan = useMutation((dto: UpdateLoanDto) => putLoanFn(id, dto), {
        onError: () => openAlertBar("Loan cannot be extended", false),
        onSuccess: () => {
            queryClient.invalidateQueries(["loan", id])
            openAlertBar("Loan is sucessfully extended.", true)
        }
    });
    const deleteLoan = useMutation(() => deleteLoanFn(id), {
        onError: () => openAlertBar("Cannot delete loan, an error occurred.", false),
        onSuccess: () => {
            queryClient.invalidateQueries(["loans"])
            openAlertBar("Loan is successfully handed in.", true)
            setTimeout(() => {
                navigate(-1)
            }, 1500)
        }
    });

    const isLongTerm = (): React.ReactNode => {
        if (data?.intervalIsLongterm === true) {
            return (
                <InputAdornment position="start">
                    <CheckIcon />
                </InputAdornment>
            )
        } return (
            <InputAdornment position="start">
                <ClearIcon />
            </InputAdornment>
        )
    }

    useEffect(() => {
        if (data && data.intervalStop) {
            setStopDate(new Date(data.intervalStop))           
        }
        
    }, [location, data])

    useEffect(() => {
        if (loanActions.extendLoan) {
            setOpenExtendLoan(true)
        }
        if (loanActions.handInLoan) {
            setOpenHandInLoan(true)
        }
    }, [])

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
    const [openExtendLoan, setOpenExtendLoan] = useState(false);
    const handleDateChange = (newValue: Date | null) => {
        if(newValue) {
            setStopDate(newValue)
        }
    };
    const handleExtendLoan = () => {
        if(stopDate) {
            extendLoan.mutate({intervalStop: stopDate.toISOString()})
        }
        setOpenExtendLoan(false)
    }

    const [openHandInLoan, setOpenHandInLoan] = useState(false);
    const handleHandInLoan = () => {
        deleteLoan.mutate()
        setOpenHandInLoan(false)
    }
    return <>
        {isLoading 
            ? <CircularLoader />
            : isError && axios.isAxiosError(error) 
                ? <NotFound message={error?.response?.data} />
                : isSuccess
                    && data !== undefined
                    && data.intervalStart !== undefined
                    && data.intervalStop !== undefined
                    && stopDate !== null
                    ?
                    <>
                    <Grid container width={400} height={50} alignItems="center"
                            sx={{
                                borderRadius: "4px 4px 0 0", 
                                borderLeft:"0.5px solid rgba(0, 0, 0, 0.12)", 
                                borderRight:"0.5px solid rgba(0, 0, 0, 0.12)", 
                                borderTop:"0.5px solid rgba(0, 0, 0, 0.12)", 
                                marginLeft:2, px: 2}}>
                                    <Grid item>
                                    <Typography variant="h5">
                                        Loan
                                    </Typography>
                                    </Grid>
                                    
                        </Grid>
                        <Grid container width={400} height={50} marginBottom={4} 
                            sx={{
                                borderRadius: "0 0 4px 4px", 
                                border:"0.5px solid rgba(0, 0, 0, 0.12)", 
                                marginLeft:2}}>                            <Grid item flexGrow={1}>
                                <IconButton size="large" onClick={() => navigate(-1)} aria-label="go back">
                                    <ArrowBackIcon />
                                </IconButton>
                            </Grid>
                            <Grid item>
                                <Tooltip title="Extend loan"><IconButton size="large" onClick={() => setOpenExtendLoan(true)} aria-label="edit"> <MoreTimeIcon /></IconButton></Tooltip>
                            </Grid>
                            <Grid item>
                                <Tooltip title="Hand in loan"><IconButton size="large" onClick={() => setOpenHandInLoan(true)} aria-label="edit"> <TaskAltIcon /></IconButton></Tooltip>
                            </Grid>
                        </Grid>
                        <Stack marginLeft={2} spacing={3} width={400} height={400} component="form" autoComplete="off">
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
                                        value={data.id}
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
                                        value="Start Date"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={new Date(data.intervalStart).toLocaleDateString()}
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
                                        value="Stop Date"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={new Date(data.intervalStop).toLocaleDateString()}
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
                                        value="Long Term"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        //value={data.intervalIsLongterm}
                                        variant="standard"
                                        InputProps={{
                                            readOnly: true,
                                            startAdornment: isLongTerm(),
                                        }}
                                    />
                                </Grid>
                            </Grid>
                            <Grid container>
                                <Grid item xs={5}>
                                    <TextField
                                        disabled
                                        fullWidth
                                        value="Assigned To"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={data.assignedToValue}
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
                                        value="Asset Id"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={data.assetId}
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
                                        value="Asset Category"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={data.assetCategoryName}
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
                                        value="BSD"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={data.bsdReference}
                                        variant="standard"
                                        InputProps={{
                                            readOnly: true,
                                        }}
                                    />
                                </Grid>
                            </Grid>
                        </Stack>
                        
                        <Dialog  open={openExtendLoan} onClose={() => setOpenExtendLoan(false)}>
                            <DialogTitle>Extend loan</DialogTitle>
                            <DialogContent>
                                <Stack spacing={3} paddingTop={2}>
                                    <DesktopDatePicker
                                        inputFormat="DD/MM/YYYY"
                                        value={stopDate}
                                        minDate={new Date(data.intervalStart)}
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
                        <Dialog  open={openHandInLoan} onClose={() => setOpenHandInLoan(false)}>
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
                    : <CircularLoader />
        }
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />
        <SpeedDialAddItemsMenu />
    </>
}