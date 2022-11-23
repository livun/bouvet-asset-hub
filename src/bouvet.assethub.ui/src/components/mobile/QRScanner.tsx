import { Box, Button, Grid } from "@mui/material";
import jsQR from "jsqr";
import { useEffect, useRef, useState } from "react";
import Webcam from "react-webcam";
import { drawLine } from "../../utils/drawLine";
import { IQrScannerProp } from "../../utils/interfaces";
import CircularLoader from "../CircularLoader";




export default function QRScanner(prop:  IQrScannerProp ) {
    const {handleQrGuid} = prop;

    const videoRef = useRef<Webcam>(null);
    const canvasRef = useRef<HTMLCanvasElement>(null);
    const [isVideoLoading, setIsVideoLoading] = useState(true);

    function scan() {
        const video = videoRef.current?.video
        if (video && video.readyState === video.HAVE_ENOUGH_DATA) {
            setIsVideoLoading(false);
            const canvasElement = canvasRef.current;
            const canvas = canvasElement?.getContext("2d");
            if (canvas && canvasElement) {
                canvasElement.height = video.videoHeight;
                canvasElement.width = video.videoWidth;
                canvas.drawImage(video, 0, 0, canvasElement.width, canvasElement.height);
                const imageData = canvas.getImageData(0, 0, canvasElement.width, canvasElement.height);

                var code = jsQR(imageData?.data, imageData?.width, imageData?.height, {
                    inversionAttempts: "dontInvert",
                });

                if (code && canvas) {
                    drawLine(code.location.topLeftCorner, code.location.topRightCorner, "#FF3B58", canvas);
                    drawLine(code.location.topRightCorner, code.location.bottomRightCorner, "#FF3B58", canvas);
                    drawLine(code.location.bottomRightCorner, code.location.bottomLeftCorner, "#FF3B58", canvas);
                    drawLine(code.location.bottomLeftCorner, code.location.topLeftCorner, "#FF3B58", canvas);
                    handleQrGuid(code.data);
                }
            }
        }
        requestAnimationFrame(scan);

    }

    useEffect(() => {
        scan()
    }, []);



    return (<>
        <Box sx={{ m: 1, }} >
            <Webcam
                audio={false}
                ref={videoRef}
                videoConstraints={{ width: 320, facingMode: "environment" }}
                style={{ display: "none" }}
            />
            <Grid container direction="column" alignItems="center"  >
                <Grid item height={250} >
                    {isVideoLoading
                        ? <CircularLoader />
                        : <canvas ref={canvasRef} />
                    }
                </Grid>
            </Grid>
        </Box>

    </>

    );
}