import { GridActionsCellItem, GridColDef, GridRowParams } from "@mui/x-data-grid";
import { useLocation, useNavigate } from "react-router-dom";
import VisibilityIcon from '@mui/icons-material/Visibility';
import { ILoanActions } from "../utils/interfaces";
import { Tooltip } from "@mui/material";
import MoreTimeIcon from '@mui/icons-material/MoreTime';
import TaskAltIcon from '@mui/icons-material/TaskAlt';

export function TableButtonsColumnForLoans() {
    const navigate = useNavigate();
    const location = useLocation();
    const loanActionsFromView: ILoanActions = { extendLoan: false, handInLoan: false }
    const loanActionsFromExtend: ILoanActions = { extendLoan: true, handInLoan: false }
    const loanActionsFromHandIn: ILoanActions = { extendLoan: false, handInLoan: true }

    const col = {
        field: "actions",
        headerName: "Actions",
        flex: 1,
        type: "actions",

        getActions: (params: GridRowParams) => [
            <GridActionsCellItem
                icon={<Tooltip title="Show"><VisibilityIcon /></Tooltip>}
                onClick={() => navigate(`${location.pathname}/${params.id}`, { state: loanActionsFromView })}
                label="Show"
            ></ GridActionsCellItem>,
            <GridActionsCellItem
                icon={<Tooltip title="Extend loan"><MoreTimeIcon sx={{ fill: "black" }} /></Tooltip>}
                onClick={() => navigate(`${location.pathname}/${params.id}`, { state: loanActionsFromExtend })}
                label="Show"
            ></ GridActionsCellItem>,
            <GridActionsCellItem
                icon={<Tooltip title="Hand in loan"><TaskAltIcon sx={{ fill: "black" }} /></Tooltip>}
                onClick={() => navigate(`${location.pathname}/${params.id}`, { state: loanActionsFromHandIn })}
                label="Edit"
            />,
        ]
    }
    return col
}