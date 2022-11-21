import { Box, Button, Grid, MenuItem, Stack, TextField } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { getAssetByGuidFn, getAssetByIdFn } from "../api/assetsApi";
import MobileMenu from "../components/MobileMain";
import QRScanner from "../components/QRScanner";
import { AssetResponseDto, Status } from "../_generated/api-types";
import GridViewSharpIcon from '@mui/icons-material/GridViewSharp';
import AlertBar from "../components/AlertBar";
import MobileActions from "../components/MobileActions";


export default function Mobile() {


    const [qrGuid, setQrGuid] = useState<string>()
    const handleQrGuid = (qrGuid: string) => {
        setQrGuid(qrGuid);
    }


    const {data, isLoading, isError} = useQuery<AssetResponseDto, Error>(["assets", qrGuid], () => getAssetByGuidFn(qrGuid!), {
        onError: (() => openAlertBar("Cant find asset", false)),
        enabled: !!qrGuid
    })




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

        <MobileMenu open={false} />

        <Box sx={{ marginTop: "60px", width: "100%", height: "90vh"}}>
            {qrGuid === undefined ? <QRScanner handleQrGuid={handleQrGuid} /> : <></>}
            {data?.id !== undefined ? <MobileActions assetId={data.id} /> : <></>}
           
            
        </Box>
        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />

    </>
}