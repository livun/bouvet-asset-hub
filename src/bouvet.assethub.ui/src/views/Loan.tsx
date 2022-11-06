import { Box, Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, IconButton, InputAdornment, MenuItem, Stack, TextField, Tooltip } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom"
import { deleteAssetFn, getAssetByIdFn, putAssetByIdFn } from "../api/assetsApi";
import { getCategoriesFn } from "../api/categoriesApi";
import { getLoanByIdFn, putLoanFn } from "../api/loansApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import queryClient from "../config/queryClient";
import { statusChecker } from "../utils/mappers";
import { IReadOnly } from "../utils/types";
import { AssetResponseDto, CategoryResponseDto, LoanResponseDto, UpdateAssetDto, UpdateLoanDto } from "../__generated__/api-types";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import AlertBar from "../components/AlertBar";
import DataGridTable from "../components/DataGridTable";
import CheckIcon from '@mui/icons-material/Check';
import ClearIcon from '@mui/icons-material/Clear';
import MoreTimeIcon from '@mui/icons-material/MoreTime';
import TaskAltIcon from '@mui/icons-material/TaskAlt';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { MobileDatePicker } from '@mui/x-date-pickers/MobileDatePicker';
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';


export default function Loan() {
    const location = useLocation()
    const navigate = useNavigate()

    const [id] = useState(Number(useParams().id))
    const [readOnly, setReadOnly] = useState<IReadOnly>(location.state as IReadOnly);
    const [stopDate, setStopDate] = useState<Date | null>(new Date())
    //const [form, setForm] = useState<UpdateLoanDto>()

    // Queries
    const { isLoading, isSuccess, isError, error, data } = useQuery<LoanResponseDto, Error>(["loan", id], () => getLoanByIdFn(id))
    const categoriesQuery = useQuery<CategoryResponseDto[], Error>(["categories"], getCategoriesFn)

    //Mutations
    const extendLoan = useMutation((dto: UpdateLoanDto) => putLoanFn(id, dto), {
        onError: () => openAlertBar("Loan can not be extended.", false),
        onSuccess: () => {
            queryClient.invalidateQueries(["loan", id])
            openAlertBar("Loan is sucessfully extended.", true)
        }
    });
    const updateAsset = useMutation((dto: UpdateAssetDto) => putAssetByIdFn(id, dto), {
        onError: () => {
            openAlertBar("Cannot update asset, asset is Unavailable.", false)
        },
        onSuccess: () => {
            queryClient.invalidateQueries(["assets"])
            openAlertBar("Asset successfully updated.", true)
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
        console.log(data)
        if (data && data.intervalStop) {
            //setForm({ intervalStop: data.intervalStop })
            setStopDate(new Date(data.intervalStop))
           
        }
    }, [categoriesQuery.data, location, data, readOnly])

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
            //setForm({intervalStop: newValue.toISOString()});

        }
    };
    const handleExtendLoan = () => {
        if(stopDate) {
            extendLoan.mutate({intervalStop: stopDate.toISOString()})
        }
        setOpenExtendLoan(false)
    }

    return <>
        {isLoading && categoriesQuery.isLoading
            ? <CircularLoader />
            : isError && axios.isAxiosError(error) && categoriesQuery.isError
                ? <NotFound message={error?.response?.data} />
                : isSuccess
                    && data !== undefined
                    && data.intervalStart !== undefined
                    && data.intervalStop !== undefined
                    && stopDate !== null
                    //&& categoriesQuery.isSuccess
                    //&& categoriesQuery.data !== undefined
                    //&& form !== undefined
                    ?
                    <>
                        <Grid container width={430} height={50} marginBottom={4}>
                            <Grid item flexGrow={1}>
                                <IconButton size="large" onClick={() => navigate(-1)} aria-label="go back">
                                    <ArrowBackIcon />
                                </IconButton>
                            </Grid>
                            <Grid item>
                                <Tooltip title="Extend loan"><IconButton size="large" onClick={() => setOpenExtendLoan(true)} aria-label="edit"> <MoreTimeIcon /></IconButton></Tooltip>
                            </Grid>
                            <Grid item>
                                <Tooltip title="Hand in loan"><IconButton size="large" onClick={() => setOpenExtendLoan(true)} aria-label="edit"> <TaskAltIcon /></IconButton></Tooltip>
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
                    </>
                    : <><p>An error has occured, manually refresh page!</p></>
        }
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />
    </>
}