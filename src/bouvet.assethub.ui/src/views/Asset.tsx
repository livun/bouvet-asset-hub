import { useParams } from "react-router-dom"
import { useGetAssetById } from "../api/useAssets";

export default function Asset () {
   
    const params = useParams()
    const id : number = +params.id! ?? 0
  
    const {status, statusText, data, error, loading} = useGetAssetById(id);

    
    
return <>
     <div> hello from asset, id is {params.id} </div>
</>   
}