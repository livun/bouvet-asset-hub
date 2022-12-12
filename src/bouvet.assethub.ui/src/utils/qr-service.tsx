import { v4 as uuidv4 } from 'uuid';
import QRCode from 'qrcode'
import { useState } from 'react';
import { Box, Button, TextField } from '@mui/material';
import { Stack } from '@mui/system';
export default function QRService() {


    const uid = uuidv4();
    const [guid, setGuid] = useState(uid)
    const [qr, setQr] = useState("")
    const generatedQR = () => {


        QRCode.toDataURL(guid, {
            type: "image/png",
            width: 200,
            margin: 0

        }, (err, image) => {
            if (err) return console.error(err)

            console.log(image)
            setQr(image)
        })
    }

    return <>
        <Stack width={350} spacing={2}>
            <Button variant='outlined' onClick={generatedQR}>Generate</Button>
            <TextField
                fullWidth
                size='small'
                value={guid}
                onChange={(event) => setGuid(event.target.value)}
            />
            <Box alignItems="center" >
                {qr && <>
                <img src={qr} />
                <p>Guid: {guid}</p>
                {/* <a href={qr} download="qrcode.png">Download</a> */}
            </>}
            </Box>
        </Stack></>
}