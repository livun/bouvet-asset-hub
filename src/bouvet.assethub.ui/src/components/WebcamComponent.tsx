import React, { useRef, useState } from 'react';
import Webcam from 'react-webcam';
import jsQR from 'jsqr';


export default function WebcamComponent() {

    const [isShowVideo, setIsShowVideo] = useState(false);
    const videoElement = useRef<Webcam>(null);
    
    const videoConstraints = {
        width: 640,
        height: 480,
        facingMode: "user"
    }

    const startCam = () => {
        setIsShowVideo(true);
        
    }
    const getImage = () => {
        if (null !== videoElement.current && null !== videoElement.current?.stream){
            let stream = videoElement.current.stream;
            const track = stream.getTracks()[0];
            const image = new ImageCapture(track);
            //jsQR(image, videoConstraints.width, videoConstraints.height);
            // let image = videoElement.current.getScreenshot()
            // console.log(image);
            // if (image !== null) {
            //     const code = jsQR(image, videoConstraints.width, videoConstraints.height)

            // }

           

    }
}

    const stopCam = () => {
        if (null !== videoElement.current && null !== videoElement.current?.stream){
            let stream = videoElement.current.stream;
            const tracks = stream.getTracks();
            tracks.forEach(track => track.stop());
            setIsShowVideo(false);

        }
        
    }

    return (
        <div>
            <div className="camView">
                {isShowVideo &&
                    <Webcam audio={false} ref={videoElement} videoConstraints={videoConstraints} screenshotFormat="image/jpeg"  />
                }
            </div>
            <button onClick={startCam} style={{marginRight:5}}>Open Camera</button> 
            <button onClick={getImage} style={{marginRight:5}}>Take Picture</button> 
            <button onClick={stopCam}>Close Camera</button>
        </div>
    );

};
