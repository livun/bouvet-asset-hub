import { useEffect, useState } from "react";
import useAssets from "../api/useAssets"
import { CreateAssetCommand } from "../__generated__/api-client";

export default function Assets () {
    const {assets, getAssets, asset, getAssetById, postAsset} = useAssets();

    const [test, setTest] = useState<any>();
    const newAsset  = new CreateAssetCommand({serialNumberValue: 45678, categoryId: 1});
    

    useEffect( () => {
        // const temp = getAssetsTest()
        // setTest(temp);
       
        const response = postAsset(newAsset).then(data => console.log(data.status)).catch(error => console.error(error))
        
        getAssets()

    }, [])
    console.log(assets)

   
    return <></>
}