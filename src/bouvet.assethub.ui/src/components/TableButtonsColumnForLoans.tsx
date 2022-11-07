import { GridActionsCellItem, GridColDef, GridRowParams } from "@mui/x-data-grid";
import { useLocation, useNavigate } from "react-router-dom";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { useMutation } from "@tanstack/react-query";
import {  deleteAssetFn } from "../api/assetsApi";
import queryClient from "../config/queryClient";
import { useState } from "react";
import { idText } from "typescript";
import { ILoanActions, IReadOnly } from "../utils/types";
import { Tooltip } from "@mui/material";
import MoreTimeIcon from '@mui/icons-material/MoreTime';
import TaskAltIcon from '@mui/icons-material/TaskAlt';

export function TableButtonsColumnForLoans () {
    const navigate = useNavigate();
    const location = useLocation();
    const [status, setStatus] = useState(0)
    const loanActionsFromShow : ILoanActions = {extendLoan: false, handInLoan: false }
    const loanActionsFromExtend : ILoanActions = {extendLoan: true, handInLoan: false}
    const loanActionsFromHandIn : ILoanActions = {extendLoan: false, handInLoan: true}
    
    const readOnlyFromEdit : IReadOnly = {isReadOnly: false}
    const readOnlyFromView : IReadOnly = {isReadOnly: true}
    
    const deleteAsset = useMutation((id: number) => deleteAssetFn(id), {
        onError:() => setStatus(404),
        onSuccess:()=> {
            queryClient.invalidateQueries(["assets"])
            setStatus(200)     
        }
      });

    const col = {
        field: "actions",
        headerName: "Actions",
        flex: 1,
        type: "actions",
        
        getActions: (params: GridRowParams) => [
            <GridActionsCellItem
            icon={<Tooltip title="Show"><VisibilityIcon /></Tooltip>}
            onClick={() => navigate(`${location.pathname}/${params.id}`, {state : loanActionsFromShow})}
            label="Show"
        ></ GridActionsCellItem>,
            <GridActionsCellItem
                icon={<Tooltip title="Extend loan"><MoreTimeIcon sx={{fill:"black"}} /></Tooltip>}
                onClick={() => navigate(`${location.pathname}/${params.id}`, {state : loanActionsFromExtend})}
                label="Show"
            ></ GridActionsCellItem>,
            <GridActionsCellItem
                icon={<Tooltip title="Hand in loan"><TaskAltIcon  sx={{fill:"black"}}/></Tooltip>}
                onClick={() => navigate(`${location.pathname}/${params.id}`, {state : loanActionsFromHandIn})}
                label="Edit"
            />, 
        ]
    }
    return {col, status}
}