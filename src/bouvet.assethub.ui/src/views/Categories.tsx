import { Box, Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, Stack, TextField } from "@mui/material"
import { DataGrid, GridActionsCellItem, GridRowParams, GridColDef, GridSelectionModel } from "@mui/x-data-grid"
import { useMutation, useQuery } from "@tanstack/react-query"
import axios from "axios"
import { deleteCategoryFn, getCategoriesFn, putCategoryFn } from "../api/categoriesApi"
import AlertBar from "../components/AlertBar"
import CircularLoader from "../components/CircularLoader"
import DataGridTable from "../components/DataGridTable"
import NotFound from "../components/NotFound"
import { CategoryResponseDto } from "../__generated__/api-types"
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import queryClient from "../config/queryClient";
import { useEffect, useState } from "react";
import { Tooltip } from "@mui/material";
import { AssetResponseDto, UpdateCategoryDto } from "../__generated__/api-types2";
import TableToolbar from "../components/TableToolbar"
import { getAssetsByCategoryFn } from "../api/assetsApi"
import SpeedDialAddItemsMenu from "../components/SpeedDialAddItemsMenu"

export default function Categories() {
    const [pageSize, setPageSize] = useState<number>(30);
    const [categoryForm, setCategoryForm] = useState<UpdateCategoryDto>({})
    const [id, setId] = useState<number>(0)
    const [openUpdateCategory, setOpenUpdateCategory] = useState(false)
    const [selectionModel, setSelectionModel] = useState<GridSelectionModel>([1]);
    const [selectedCategoryId, setSelectedCategoryId] = useState<number>(0)
    const [openConfirmDelete, setOpenConfirmDelete] = useState(false)

    useEffect(() => {
        if(selectionModel) {
            setSelectedCategoryId(Number(selectionModel[0]))
        }
    }, [selectionModel])

    // Queries
    const { isLoading, isSuccess, isError, error, data } = useQuery<CategoryResponseDto[], Error>(["categories"], getCategoriesFn)
    const assetsByCategoryQuery = useQuery<AssetResponseDto[], Error>(["assets", selectedCategoryId], () => getAssetsByCategoryFn(selectedCategoryId), {
        onError: (() => openAlertBar("No assets on that category", false)),
        enabled: !!selectedCategoryId
    })
    
    //Mutations
    const deleteCategory = useMutation((id: number) => deleteCategoryFn(id), {
        onError: () => openAlertBar("Could not delete category, because it is used", false),
        onSuccess: () => {
            queryClient.invalidateQueries(["categories"])
            openAlertBar("You have sucessfully deleted categories", true)
        }
    });
    const updateCategory = useMutation(() => putCategoryFn(id, categoryForm), {
        onError: () => {
            setOpenUpdateCategory(false)
            openAlertBar("Cannot update category, because it is used.", false)
        },
        onSuccess: () => {
            setOpenUpdateCategory(false)
            queryClient.invalidateQueries(["categories"])
            openAlertBar("Category is successfully updated.", true)
        }
    });

    // Defining table
    const columns: GridColDef[] = [
        { field: "id", type: "string", flex: 0.3, headerName: "Id" },
        { field: "name", type: "string", flex: 1, headerName: "Name" }
    ]
    const getCategoryName = (id: number) => {
        const cat = data?.filter(cat => cat.id === id)
        if (cat) {
            return cat[0].name
        }
        return "error"
    }
    const tableButtonsColumn = {
        field: "actions",
        flex: 0.4,
        type: "actions",
        _getActions: (params: GridRowParams) => [
            <GridActionsCellItem
                icon={<Tooltip title="Edit"><EditIcon /></Tooltip>}
                onClick={() => {
                    setId(Number(params.id))
                    setCategoryForm({ name: getCategoryName(Number(params.id)) })
                    setOpenUpdateCategory(true)
                }}
                label="Edit" />,
            <GridActionsCellItem
                icon={<Tooltip title="Delete"><DeleteIcon /></Tooltip>}
                onClick={() => {
                    setId(Number(params.id))
                    setOpenConfirmDelete(true)
                }}
                label="Delete" />
        ],
        get getActions() {
            return this._getActions
        },
        set getActions(value) {
            this._getActions = value
        },
    }
    columns.push(tableButtonsColumn);

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
        {isLoading
            ? <CircularLoader />
            : isError && axios.isAxiosError(error)
                ? <NotFound message={error?.response?.data} />
                : isSuccess
                    ? <Grid container spacing={1}>
                        <Grid item>
                            <Box height={700} width={600}>
                                <div style={{ display: "flex", height: "100%" }}>
                                    <div style={{ flexGrow: 1 }}>
                                        <DataGrid
                                            rows={data}
                                            rowHeight={40}
                                            columns={columns}
                                            pageSize={pageSize}
                                            onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
                                            rowsPerPageOptions={[10, 30, 50, 70, 100]}
                                            components={{ Toolbar: TableToolbar }}
                                            componentsProps={{
                                                toolbar: {
                                                    headerName: "Categories"
                                                }
                                            }}
                                            checkboxSelection
                                            onSelectionModelChange={(selection) => {
                                                if (selection.length > 1) {
                                                    const selectionSet = new Set(selectionModel)
                                                    const result = selection.filter((s) => !selectionSet.has(s))
                                                    setSelectionModel(result);
                                                } else {
                                                    setSelectionModel(selection)
                                                }
                                            }}
                                            selectionModel={selectionModel}
                                        />
                                    </div>
                                </ div>
                            </Box>
                        </Grid>
                        <Grid item flexGrow="1" >
                            {assetsByCategoryQuery.isSuccess ?
                                <DataGridTable<AssetResponseDto> rows={assetsByCategoryQuery.data} headerName="Assets by Category" /> : <></>
                            }
                        </Grid>
                    </Grid>
                    : <></>
        }
        <Dialog open={openUpdateCategory} onClose={() => setOpenUpdateCategory(false)}>
            <DialogTitle>Update category</DialogTitle>
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
                <Button onClick={() => setOpenUpdateCategory(false)}>Cancel</Button>
                <Button onClick={() => updateCategory.mutate()}>Save</Button>
            </DialogActions>
        </Dialog>
        <Dialog open={openConfirmDelete} onClose={() => setOpenConfirmDelete(false)}
        >  
            <DialogContent>
                <DialogContentText id="alert-dialog-description">
                {`Are you sure you want to delete category with id ${id}?` }           
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => setOpenConfirmDelete(false)}>No</Button>
                <Button onClick={() => {
                    deleteCategory.mutate(id) 
                    setOpenConfirmDelete(false) }}
                    autoFocus>
                    yes
                </Button>
            </DialogActions>
        </Dialog>
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />
        <SpeedDialAddItemsMenu />
    </>
}