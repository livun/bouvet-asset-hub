import { Box, Button, Grid, IconButton, MenuItem, Stack, TextField, Tooltip } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import { useEffect, useState } from "react";
import { getAssetByGuidFn, getAssetByIdFn } from "../api/assetsApi";
import QRScanner from "../components/mobile/QRScanner";
import { AssetResponseDto, Status } from "../_generated/api-types";
import GridViewSharpIcon from '@mui/icons-material/GridViewSharp';
import AlertBar from "../components/AlertBar";
import MobileActions from "../components/mobile/MobileActions";
import Html5TsWrapper from "../Html5QrCodePlugin/Html5TsWrapper";
import { useNavigate } from "react-router-dom";
import QrCodeScannerIcon from '@mui/icons-material/QrCodeScanner';
import QrCode2Icon from '@mui/icons-material/QrCode2';
import AddBoxIcon from '@mui/icons-material/AddBox';
import AddIcon from '@mui/icons-material/Add';
import MobileMain from "../components/mobile/MobileMain";
import NewAssetMobile from "../components/mobile/NewAssetMobile";


export default function Mobile() {
    const [scan, setScan] = useState(false)
    const [register, setRegister] = useState(false)
    const [buttons, setButtons] = useState(true)
    const navigate = useNavigate();


    const [qrGuid, setQrGuid] = useState<string>()
    const handleQrGuid = (qrGuid: string) => {
        setQrGuid(qrGuid);
    }


    const { data, isLoading, isError } = useQuery<AssetResponseDto, Error>(["assets", qrGuid], () => getAssetByGuidFn(qrGuid!), {
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

        <MobileMain open={false} />
        {buttons ? <Grid container width="100%" height="90vh" flexGrow={1} direction="column" justifyContent="center" spacing={1} alignItems="center">
            <Grid item  >
                <Tooltip title="scan">
                <IconButton color="primary"  onClick={() => { setScan(true); setRegister(false); setButtons(false) }}> <QrCode2Icon sx={{fontSize:"150px"}} /></IconButton>

                </Tooltip>
            </Grid>
            <Grid item>
                <Tooltip title="nev asset">
                    <IconButton color="primary"  onClick={() => { setScan(false); setRegister(true); setButtons(false) }}> <AddIcon sx={{fontSize:"150px"}} /></IconButton>

                </Tooltip>
            </Grid>

        </Grid> : <></>}

        {!buttons ? <Grid container justifyContent="center" width="100%" marginTop="60px" spacing={1} sx={{ mx: 1 }}>
            
            <Grid item xs={12}>
                {scan ? <Box sx={{ marginTop: "60px", width: "100%", height: "70vh" }}>
                    {qrGuid === undefined ? <QRScanner handleQrGuid={handleQrGuid} /> : <></>}
                    {data?.id !== undefined ? <MobileActions assetId={data.id} /> : <></>}
                </Box> : <></>}
            </Grid>
            <Grid item xs={12}>
                {register 
                ? <Box sx={{ marginTop: "60px", width: "100%", height: "70vh" }}>
                    <NewAssetMobile />
                </Box> 
                : <></>}
                </Grid>
            
                <Grid width={245}  item  >
                <Button fullWidth variant="contained" onClick={() => { navigate(0) }}>Back to mobile menu</Button>
            </Grid>

        </Grid> : <></>}

        <AlertBar open={open} handleClose={handleClose} message={alertBarMsg} success={success} />

    </>
}