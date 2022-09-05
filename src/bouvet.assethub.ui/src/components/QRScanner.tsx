import jsQR from "jsqr";
import { useEffect, useRef, useState } from "react";

export function drawLine(begin: any, end:any, color: string, canvas:  CanvasRenderingContext2D) {
    canvas.beginPath();
    canvas.moveTo(begin.x, begin.y);
    canvas.lineTo(end.x, end.y);
    canvas.lineWidth = 4;
    canvas.strokeStyle = color;
    canvas.stroke();
  }

export default function QRScanner () {

    const video = document.createElement("video");
    const canvasElement = useRef<HTMLCanvasElement>(null)
    const canvas = canvasElement?.current?.getContext("2d")

    const [loadingMsg, setLoadingMsg] = useState("")
    const [output, setOutput] = useState("")

    function tick() {
        setLoadingMsg("⌛ Loading video...")
        if (video.readyState === video.HAVE_ENOUGH_DATA && canvasElement.current !== null){
            console.log("hello")
            setLoadingMsg("")
            canvasElement.current.hidden = false;

            canvasElement.current.height = video.videoHeight;
            canvasElement.current.width = video.videoWidth;
            canvas?.drawImage(video, 0,0, canvasElement.current.width, canvasElement.current.height);
            const imageData = canvas?.getImageData(0, 0, canvasElement.current.width, canvasElement.current.height);
            if (imageData){
                var code = jsQR(imageData?.data, imageData?.width, imageData?.height, {
                    inversionAttempts: "dontInvert",
                });
                if (code && canvas) {
                    drawLine(code.location.topLeftCorner, code.location.topRightCorner, "#FF3B58", canvas);
                    drawLine(code.location.topRightCorner, code.location.bottomRightCorner, "#FF3B58", canvas);
                    drawLine(code.location.bottomRightCorner, code.location.bottomLeftCorner, "#FF3B58", canvas);
                    drawLine(code.location.bottomLeftCorner, code.location.topLeftCorner, "#FF3B58", canvas);
                    setOutput(code.data);
                    
                } else {
                    console.log("waiting")
                }
            }
            requestAnimationFrame(tick);
               
        }

    }


    navigator.mediaDevices.getUserMedia({video: {facingMode: "environment"}}).then(function(stream){
        video.srcObject = stream;
        video.setAttribute("playsinline", ""); //tell iOS safari we dont want fullscreen
        video.play();
        requestAnimationFrame(tick)
    })
    
   
    

    

    
    return(<>
    <h3>QRScanner</h3>
    <div> {loadingMsg} </div>
    <canvas ref={canvasElement} hidden/>
    <div> {output} </div>

    
    
    

    </>

    );
}