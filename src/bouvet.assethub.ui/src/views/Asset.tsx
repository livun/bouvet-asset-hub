import { Grid, IconButton, MenuItem, Stack, TextField, Tooltip } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom"
import { deleteAssetFn, getAssetByIdFn, putAssetByIdFn } from "../api/assetsApi";
import { getCategoriesFn } from "../api/categoriesApi";
import CircularLoader from "../components/CircularLoader";
import NotFound from "../components/NotFound";
import { statusChecker } from "../utils/mappers";
import { IReadOnly } from "../utils/types";
import { AssetResponseDto, CategoryResponseDto, UpdateAssetDto } from "../__generated__/api-types";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import AlertBar from "../components/AlertBar";
import queryClient from "../config/queryClient";

export default function Asset() {
    const location = useLocation()
    const navigate = useNavigate()

    const [id] = useState(Number(useParams().id))
    const [readOnly, setReadOnly] = useState<IReadOnly>(location.state as IReadOnly);
    const [form, setForm] = useState<UpdateAssetDto>()

    // Queries
    const { isLoading, isSuccess, isError, error, data } = useQuery<AssetResponseDto, Error>(["asset", id], () => getAssetByIdFn(id), {})
    const categoriesQuery = useQuery<CategoryResponseDto[], Error>(["categories"], getCategoriesFn)

    //Mutations
    const deleteAsset = useMutation(() => deleteAssetFn(id), {
        onError: () => openAlertBar("Asset cannot be deleted, used in loan", false),
        onSuccess: () => {
            queryClient.invalidateQueries(["assets"])
            openAlertBar("Succesfull delete of asset", true)
            setTimeout(() => {
                navigate(-1)
            }, 1500);
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

    useEffect(() => {
        if (data) {
            setForm({ status: data.status, categoryId: data.categoryId })
            if (!readOnly.isReadOnly && data.status === 2) {
                openAlertBar("Asset is Unavailable and cannot be edited.", false)
                setReadOnly({ isReadOnly: true })
            }
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

    return <>
        {isLoading && categoriesQuery.isLoading
            ? <CircularLoader />
            : isError && axios.isAxiosError(error) && categoriesQuery.isError
                ? <NotFound message={error?.response?.data} />
                : isSuccess
                    && data !== undefined
                    && categoriesQuery.isSuccess
                    && categoriesQuery.data !== undefined
                    && form !== undefined
                    ?
                    <>
                        <Grid container width={430} height={50} marginBottom={4}>
                            <Grid item flexGrow={1}>
                                <Tooltip title="Go back">
                                    <IconButton size="large" onClick={() => navigate(-1)} aria-label="go back">
                                        <ArrowBackIcon />
                                    </IconButton>
                                </Tooltip>
                            </Grid>
                            <Grid item> {readOnly.isReadOnly
                                ? <Tooltip title="Edit"><IconButton disabled={data.status === 2} size="large" onClick={() => setReadOnly({ isReadOnly: false })} aria-label="edit"> <EditIcon /></IconButton></Tooltip>
                                : <Tooltip title="Save"><IconButton size="large" onClick={() => updateAsset.mutate(form)} aria-label="save"> <SaveIcon /></IconButton></Tooltip>}
                            </ Grid>
                            <Grid item>
                                <Tooltip title="Delete"><IconButton disabled={data.status === 2} size="large" onClick={() => deleteAsset.mutate()} aria-label="edit"> <DeleteIcon /></IconButton></Tooltip>
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
                                        value="Serial Number"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        value={data.serialNumberValue}
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
                                        value="Category"
                                        variant="standard"
                                    />
                                </Grid>
                                <Grid item xs={7}>
                                    <TextField
                                        fullWidth
                                        select
                                        value={form?.categoryId}
                                        variant="standard"
                                        onChange={(event) => setForm({ ...form, categoryId: Number(event?.target.value) })}
                                        InputProps={{
                                            readOnly: readOnly.isReadOnly
                                        }}
                                    >
                                        {categoriesQuery.data.map((cat) => (
                                            <MenuItem key={cat.id} value={cat.id}>
                                                {cat.name}
                                            </MenuItem>
                                        ))}
                                    </TextField>
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
                                        select
                                        value={Number(form.status)}
                                        variant="standard"
                                        onChange={(event) => setForm({ ...form, status: statusChecker(event.target.value) })}
                                        InputProps={{
                                            readOnly: readOnly.isReadOnly
                                        }}
                                    >
                                        <MenuItem value={0}>Registered</MenuItem>
                                        <MenuItem value={1}>Available</MenuItem>
                                        <MenuItem disabled value={2}>Unavailable</MenuItem>
                                        <MenuItem value={3}>Discontinued</MenuItem>
                                    </TextField>
                                </Grid>
                            </Grid>
                        </Stack>
                    </>
                    : <CircularLoader />
        }
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />
    </>
}