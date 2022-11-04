import { useQuery } from "@tanstack/react-query";
import { useParams } from "react-router-dom"
import { getAssetByIdFn } from "../api/assetsApi";
import { AssetResponseDto } from "../__generated__/api-types";

export default function Asset () {
   
    const params = useParams()
    const id : number = +params.id! ?? 0
  
    // const {status, statusText, data, error, loading} = useGetAssetById(id);

    const { isLoading, isSuccess, isError, error, data} = useQuery<AssetResponseDto, Error>(["asset"], () => getAssetByIdFn(id))

    
return <>
     <div> hello from asset, id is {params.id} </div>
</>   
}