import { GridActionsCellItem, GridRowParams } from "@mui/x-data-grid";
import { useLocation, useNavigate } from "react-router-dom";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { deleteItem } from "../utils/actions";

export const TableButtonsColumn = () => {
    const navigate = useNavigate();
    const location = useLocation();

    const col = {
        field: "actions",
        headerName: "Actions",
        flex: 1,
        type: "actions",
        getActions: (params: GridRowParams) => [
            <GridActionsCellItem
                icon={<VisibilityIcon />}
                onClick={() => navigate(`${location.pathname}/${params.id}`)}
                label="Show"
            ></ GridActionsCellItem>,
            <GridActionsCellItem
                icon={<EditIcon />}
                onClick={() => deleteItem(params.id, params)}
                label="Edit"
            />,
            <GridActionsCellItem
                icon={<DeleteIcon/>}
                onClick={() => deleteItem(params.id, params)}
                label="Delete"
            />,

        ]

    }


    return col
}