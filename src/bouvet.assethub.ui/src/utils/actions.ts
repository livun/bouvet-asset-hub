import { GridRowId, GridRowParams } from "@mui/x-data-grid"
import { useCallback } from "react"
import { redirect, useNavigate } from "react-router-dom"

export function deleteItem(id : GridRowId, params: GridRowParams){
    console.log(params)
    console.log(id)
}

export function useViewAsset(id : GridRowId){
    const navigate = useNavigate()
    navigate(`/assets/${id}`,  {replace: true})
    //navigate(`/assets/${id}`,  {replace: true})
    
}

// export const useViewItem = useCallback( (id: GridRowId) => {
//         redirect(`/assets/${id}`)
        
//     }, [])

