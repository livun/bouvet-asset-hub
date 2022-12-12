import { GridActionsCellItem, GridRowParams } from "@mui/x-data-grid";
import { useLocation, useNavigate } from "react-router-dom";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { Tooltip } from "@mui/material";
import { IAssetActions } from "../utils/interfaces";

export function TableButtonsColumnForAssets() {
    const navigate = useNavigate();
    const location = useLocation();
    const assetActionFromEdit: IAssetActions = { isReadOnly: false, isDelete: false }
    const assetActionFromView: IAssetActions = { isReadOnly: true, isDelete: false }
    const assetActionFromDelete: IAssetActions = { isReadOnly: true, isDelete: true }

    const col = {
        field: "actions",
        headerName: "Actions",
        flex: 1,
        type: "actions",

        getActions: (params: GridRowParams) => [
            <GridActionsCellItem
                icon={<Tooltip title="Show"><VisibilityIcon /></Tooltip>}
                onClick={() => navigate(`${location.pathname}/${params.id}`, { state: assetActionFromView })}
                label="Show"
            ></ GridActionsCellItem>,
            <GridActionsCellItem
                icon={<Tooltip title="Edit"><EditIcon /></Tooltip>}
                onClick={() => navigate(`${location.pathname}/${params.id}`, { state: assetActionFromEdit })}
                label="Edit"
            />,
            <GridActionsCellItem
                icon={<Tooltip title="Delete"><DeleteIcon /></Tooltip>}
                onClick={() => navigate(`${location.pathname}/${params.id}`, { state: assetActionFromDelete })}
                label="Delete"
            />
        ]
    }
    return col
}