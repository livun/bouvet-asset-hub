import queryClient from '../config/queryClient';
import { SpeedDial, SpeedDialAction, SpeedDialIcon, Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControlLabel, MenuItem, Stack, Switch, TextField} from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { getAssetsFn, postAssetsFn } from "../api/assetsApi";
import AlertBar from "./AlertBar";
import { getCategoriesFn, postCategoriesFn } from "../api/categoriesApi";
import CircularLoader from "./CircularLoader";
import { DesktopDatePicker } from "@mui/x-date-pickers";
import { postLoansFn } from '../api/loansApi';
import CategoryIcon from '@mui/icons-material/Category';
import CalendarMonthSharpIcon from '@mui/icons-material/CalendarMonthSharp';
import DashboardCustomizeSharpIcon from '@mui/icons-material/DashboardCustomizeSharp';
import { useNavigate } from 'react-router-dom';
import { IAssetActions, ILoanActions } from '../utils/interfaces';
import { v4 as uuidv4 } from 'uuid';
import { AssetResponseDto, CategoryResponseDto, CreateAssetDto, CreateCategoryDto, CreateLoanDto, Status } from '../_generated/api-types';


export default function AddItemsFAB() {
    const navigate = useNavigate()
    const today = new Date().toISOString()
    const [assetForm, setAssetForm] = useState<CreateAssetDto>({})
    const [loanForm, setLoanForm] = useState<CreateLoanDto>({ intervalStart: today })
    const [categoryForm, setCategoryForm] = useState<CreateCategoryDto>({})
    const [openAddAsset, setOpenAddAsset] = useState(false);
    const [openAddLoan, setOpenAddLoan] = useState(false);
    const [openAddCategory, setOpenAddCategory] = useState(false)
    const assetActionFromView: IAssetActions = { isReadOnly: true, isDelete: false }
    const loanActionsFromView: ILoanActions = { extendLoan: false, handInLoan: false }

    const actions = [
        { icon: <CalendarMonthSharpIcon />, name: 'Add loan', event: () => setOpenAddLoan(true) },
        { icon: <CategoryIcon />, name: 'Add category', event: () => setOpenAddCategory(true) },
        { icon: <DashboardCustomizeSharpIcon />, name: 'Add asset', event: () => setOpenAddAsset(true) },
    ];

    // Queries
    const assetsQuery = useQuery<AssetResponseDto[], Error>(["assets"], getAssetsFn, {
        select: (assets) => assets.filter((asset) => asset.status !== Status.Unavailable  && asset.status !== Status.Discontinued),
    })
    const categoriesQuery = useQuery<CategoryResponseDto[], Error>(["categories"], getCategoriesFn)

    //Mutations
    const addAsset = useMutation(() => postAssetsFn(assetForm), {
        onError: () => {
            openAlertBar("Cannot add asset.", false)
        },
        onSuccess: (data) => {
            queryClient.invalidateQueries(["assets"])
            setAssetForm({})
            setOpenAddAsset(false)
            openAlertBar("Asset is added.", true)
            setTimeout(() => {
                navigate(`/assets/${data.id}`, { state: assetActionFromView })
            }, 1500)
        }
    });
    const addLoan = useMutation(() => postLoansFn(loanForm), {
        onError: () => {
            openAlertBar("Cannot add loan.", false)
        },
        onSuccess: (data) => {
            queryClient.invalidateQueries(["loans"])
            setLoanForm({ ...loanForm, intervalStart: today })
            setOpenAddLoan(false)
            openAlertBar("Loan is added.", true)
            setTimeout(() => {
                navigate(`/loans/${data.id}`, { state: loanActionsFromView })
            }, 1500)
        }
    });
    const addCategory = useMutation(() => postCategoriesFn(categoryForm), {
        onError: () => {
            openAlertBar("Cannot add category.", false)
        },
        onSuccess: () => {
            queryClient.invalidateQueries(["categories"])
            setCategoryForm({})
            setOpenAddCategory(false)
            openAlertBar("Category is added.", true)
            setTimeout(() => {
                navigate(`/categories`)
            }, 1500)
        }
    });

    const handleAddAsset = () => {
        setAssetForm({ ...assetForm, qrIdentifier: uuidv4() })
        addAsset.mutate()
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

    return <>
        <SpeedDial
            ariaLabel="SpeedDial basic example"
            sx={{ position: 'absolute', bottom: 20, right: 20, }}
            icon={<SpeedDialIcon />}
        >
            {actions.map((action) => (
                <SpeedDialAction
                    key={action.name}
                    icon={action.icon}
                    tooltipTitle={action.name}
                    tooltipOpen
                    onClick={action.event}
                />
            ))}
        </SpeedDial>
        {categoriesQuery !== undefined
            ? <Dialog open={openAddAsset} onClose={() => setOpenAddAsset(false)}>
                <DialogTitle>Add asset</DialogTitle>
                <DialogContent>
                    <Stack spacing={3} paddingTop={2} component="form" autoComplete="off" width={400} >
                        <TextField
                            fullWidth
                            type="string"
                            label="Serial Number"
                            value={assetForm.serialNumber}
                            onChange={(event) => setAssetForm({ ...assetForm, serialNumber: event?.target.value })}
                        />
                        <TextField
                            fullWidth
                            select
                            label="Category"
                            value={assetForm.categoryId}
                            onChange={(event) => setAssetForm({ ...assetForm, categoryId: Number(event?.target.value), qrIdentifier: uuidv4() })}
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
                    <Button onClick={() => handleAddAsset()}>Save</Button>
                </DialogActions>
            </Dialog>
            : <CircularLoader />}
        {assetsQuery !== undefined
            ? <Dialog open={openAddLoan} onClose={() => setOpenAddLoan(false)}>
                <DialogTitle>Add loan</DialogTitle>
                <DialogContent>
                    <Stack spacing={3} paddingTop={2} width={400} component="form" autoComplete="off">
                        <DesktopDatePicker
                            inputFormat="DD/MM/YYYY"
                            label="Start date"
                            value={loanForm.intervalStart}
                            minDate={new Date().toISOString()}
                            disableFuture
                            onChange={handleStartDateChange}
                            renderInput={(params) => <TextField {...params} />}
                        />
                        {!loanForm.intervalIsLongterm
                            ? <DesktopDatePicker
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
                            select
                            label="Asset"
                            value={loanForm.assetId}
                            onChange={(event) => setLoanForm({ ...loanForm, assetId: Number(event?.target.value) })}
                        >
                            {assetsQuery.data?.map((asset) => (
                                <MenuItem key={asset.id} value={asset.id}>
                                    {asset.id}, {asset.categoryName}, {asset.status}
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
        {assetsQuery !== undefined
            ? <Dialog open={openAddCategory} onClose={() => setOpenAddCategory(false)}>
                <DialogTitle>Add category</DialogTitle>
                <DialogContent>
                    <Stack spacing={3} paddingTop={2} width={400} component="form" autoComplete="off">
                        <TextField
                            fullWidth
                            label="Name"
                            value={categoryForm.name}
                            onChange={(event) => setCategoryForm({ name: event.target.value })}
                        >
                        </TextField>
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenAddCategory(false)}>Cancel</Button>
                    <Button onClick={() => addCategory.mutate()}>Save</Button>
                </DialogActions>
            </Dialog>
            : <CircularLoader />}
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />
    </>
}
