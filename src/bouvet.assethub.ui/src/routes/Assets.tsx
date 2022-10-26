import { useGetAssets } from "../api/useAssets";
import DataGridTable from "../components/Table";

export default function Assets () {
    const {status, statusText, data, error, loading} = useGetAssets();
    
    console.log(status)
    console.log("loading is;", loading)
    if(!loading)
    {
        console.log(data)
    }

   
   

   
    return <>
    {data !== undefined ? <DataGridTable rows={data} /> : <></> }
    
    </>
}