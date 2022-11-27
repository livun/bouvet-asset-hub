import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, MenuItem, Stack, TextField } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import { useEffect, useState } from "react";


import { v4 as uuidv4 } from 'uuid';
import { useNavigate } from "react-router-dom";
import { getCategoriesFn } from "../../api/categoriesApi";
import { CategoryResponseDto, CreateAssetDto } from "../../_generated/api-types";
import { postAssetsFn } from "../../api/assetsApi";
import queryClient from "../../config/queryClient";
import Html5TsWrapper from "../../Html5QrCodePlugin/Html5TsWrapper";
import AlertBar from "../AlertBar";


export default function NewAssetMobile() {
    const [decode, setDecode] = useState(true)
    const [decodedText, setDecodedText] = useState("")
    const [openDialog, setOpenDialog] = useState(false)
    const [assetForm, setAssetForm] = useState<CreateAssetDto>({})
    const [openAddAsset, setOpenAddAsset] = useState(false);
    const navigate = useNavigate();


    //queries
    const categoriesQuery = useQuery<CategoryResponseDto[], Error>(["categories"], getCategoriesFn)


    //Mutations
    const addAsset = useMutation(() => postAssetsFn(assetForm), {
        onError: () => {
            openAlertBar("Cannot add asset.", false)
            setAssetForm({})
            setDecodedText("")
            setOpenAddAsset(false)
            setDecode(true);
        },
        onSuccess: () => {
            queryClient.invalidateQueries(["assets"])
            setAssetForm({})
            setDecodedText("")
            setOpenAddAsset(false)
            openAlertBar("Asset is added.", true)
            setTimeout(() => {
                navigate(0)
            }, 1500)
            
        }
    });
    
    const handleDecodedText = (text: string) => {
        setDecodedText(text)
        setAssetForm({ ...assetForm, serialNumber: text })



    }

    const handleAddAsset = () => {
        setAssetForm({ ...assetForm, qrIdentifier: uuidv4() })
        addAsset.mutate()
    }

    useEffect(() =>{
        if (decodedText !== ""){
            setOpenDialog(true)

        }

    }, [decodedText])


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
        {decode ? <Html5TsWrapper handleDecodedText={handleDecodedText} /> : <></> } 
        <Dialog  open={openDialog} onClose={() => setOpenDialog(false)}>
                <DialogContent>
                    <DialogContentText>
                        Is "{decodedText}" the correct serial number?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => {setOpenDialog(false); setDecode(true); }}>No</Button>
                    <Button onClick={() => { setOpenDialog(false); setOpenAddAsset(true); setDecode(false)}}>Yes</Button>
                </DialogActions>
            </Dialog>    
        <Dialog open={openAddAsset} onClose={() => setOpenAddAsset(false)}>
                <DialogTitle>Add asset</DialogTitle>
                <DialogContent>
                    <Stack spacing={3} paddingTop={2} component="form" autoComplete="off" width={250} >
                        <TextField
                            fullWidth
                            type="string"
                            label="Serial Number"
                            value={assetForm.serialNumber}
                        />
                        <TextField
                            fullWidth
                            select
                            label="Category"
                            value={assetForm.categoryId}
                            onChange={(event) => setAssetForm({ ...assetForm, categoryId: Number(event?.target.value), qrIdentifier: uuidv4() })}
                        >
                            {categoriesQuery.data?.map((cat : CategoryResponseDto) => (
                                <MenuItem key={cat.id} value={cat.id}>
                                    {cat.name}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => {setDecode(true); setOpenAddAsset(false)}}>Cancel</Button>
                    <Button onClick={() => handleAddAsset()}>Save</Button>
                </DialogActions>
            </Dialog>
            <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />

    </>
}

