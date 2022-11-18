import { Box, Button, Grid } from "@mui/material";
import jsQR from "jsqr";
import { useEffect, useRef, useState } from "react";
import Webcam from "react-webcam";
import { drawLine } from "../utils/drawLine";
import CircularLoader from "./CircularLoader";




export default function QRScanner() {

    const videoRef = useRef<any>(null);
    const canvasRef = useRef<HTMLCanvasElement>(null);
    const [isVideoLoading, setIsVideoLoading] = useState(true);
    const [output, setOutput] = useState(""); 
    const [shouldScan, setShouldScan] = useState(true)

    function scan ()  {
        console.log("scannning  ")
        const video = videoRef.current.video;
        if (video && video.readyState === video.HAVE_ENOUGH_DATA) {

            setIsVideoLoading(false);

            const canvasElement = canvasRef.current;
            const canvas = canvasElement?.getContext("2d");
            if (canvas && canvasElement) {
                canvasElement.height = video.videoHeight;
                canvasElement.width = video.videoWidth;
                canvas.drawImage(video, 0, 0, canvasElement.width, canvasElement.height);
                console.log("heighy", canvasElement.height )
                const imageData = canvas.getImageData(0, 0, canvasElement.width, canvasElement.height);

                var code = jsQR(imageData?.data, imageData?.width, imageData?.height, {
                    inversionAttempts: "dontInvert",
                });

                if (code && canvas) {
                    drawLine(code.location.topLeftCorner, code.location.topRightCorner, "#FF3B58", canvas);
                    drawLine(code.location.topRightCorner, code.location.bottomRightCorner, "#FF3B58", canvas);
                    drawLine(code.location.bottomRightCorner, code.location.bottomLeftCorner, "#FF3B58", canvas);
                    drawLine(code.location.bottomLeftCorner, code.location.topLeftCorner, "#FF3B58", canvas);
                    setOutput(code.data);
                }
                requestAnimationFrame(scan);
            }
        }
    }

    useEffect(()=>{
        console.log("hello from useeffects");
        scan()
    }, [canvasRef]);



    return (<>
    <Box sx={{backgroundColor: "pink", m:1, }} >
    <Webcam 
        audio={false} 
        ref={videoRef} 
        videoConstraints= {{width: 320, facingMode: "user"}}
        style={{display:"none"}}
        />

        <Grid container direction="column" alignItems="center"  >
            <Grid item >         
                <canvas ref={canvasRef} />
            </Grid>
      
            {/* <Grid item>
                <Button variant="outlined" onClick={scan} >Open Scanner</Button>

            </Grid> */}
            <Grid>Ouput: {output}</Grid>

        </Grid>


    </Box>
      
    </>

    );
}