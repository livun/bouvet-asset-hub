import { GridActionsCellItem, GridColDef, GridRowId, GridRowParams, GridValueGetterParams } from "@mui/x-data-grid";
import { Status } from "./enums";
import { lookupKeysMapper } from "./mappers";
import { capitalizeAndSplit } from "./regex";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { deleteItem, useViewAsset} from "./actions";
import { Link, redirect, useLocation, useNavigate } from "react-router-dom";


const formatHeaderKeys = (key: string) => {
    const result = lookupKeysMapper[key];
    if (result !== undefined){
        return result
    }
    return capitalizeAndSplit(key);
}

function formatString (key: string, object: any){
    
    const filter = () => typeof(object) === "string";
    const value = () => {
        const col : GridColDef = {
            field: key,
                headerName: formatHeaderKeys(key),
                headerAlign: "left",
                align: "left",
                flex: 1,
                //width: 150,
                type: typeof (object)

        }
        return col
    }
    return [filter, value]
}

function formatBooleans (key: string, object: any) {
    const filter = () => typeof (object) === "boolean"
    const value = () => {
        const col : GridColDef = {
            field: key,
                headerName: formatHeaderKeys(key),
                headerAlign: "center",
                align: "center",
                flex: 0.7,
                type: typeof (object)

        }
        return col
    }
    return [filter, value]

}

function formatId (key: string, object: any){
    const filter = () => key.toLocaleLowerCase().includes("id")
    const value = () => {
        const col : GridColDef = {
            field: key,
            headerName: formatHeaderKeys(key),
            headerAlign: "left",
            align: "left",
            flex: 0.5,
            type: 'string'
          

        }
        return col
    }
    return [filter, value]
}


function formatStatus (key: string, object: any){
    const filter = () => key.toLocaleLowerCase().includes("status")
    const value = () => {
        const col : GridColDef = {
            field: key,
                headerName: formatHeaderKeys(key),
                headerAlign: "left",
                align: "left",
                flex: 1,
                type: typeof (object),
                valueGetter: ({ value }) => Status[value],
        }
        return col
    }
    return [filter, value]
}

function formatDate (key: string, object: any){
    const filter = () =>  key.toLocaleLowerCase().includes("interval")
    const value = () => {
        const col : GridColDef = {
            field: key,
                headerName: formatHeaderKeys(key),
                headerAlign: "left",
                align: "left",
                flex: 1,
                type: 'dateTime',
                valueGetter: ({ value }) => value && new Date(value).toLocaleDateString(),
        }
        return col
    }
    return [filter, value]
}
function formatGeneral (key: string, object: any){
    const filter = () => typeof(key) === "string";
    const value = () => {
        const col : GridColDef = {
            field: key,
                headerName: formatHeaderKeys(key),
                headerAlign: "left",
                align: "left",
                flex: 1,
                //width: 150,
                type: "string"

        }
        return col
    }
    return [filter, value]
}


const ButtonsColumn = () => {
    const navigate = useNavigate();
    const location = useLocation();

    console.log(location.pathname)
   
        const col = {
            field: "actions",
            headerName: "Actions",
            flex: 0.8,
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
                icon={<DeleteIcon />} 
                onClick={() => deleteItem(params.id, params)} 
                label="Delete" 
                />,
                

            ]
      
        }

  
    return col
}

// Array of formatproviders, the less generic in the beginning of array
const formatProviders = [formatId, formatBooleans, formatDate, formatStatus, formatString, formatGeneral]

// Loops through all formatters and renders column definitions based on properties of objects. 
function getColumnsFor(key: string, object: any){
    for (const formatter of formatProviders){
        const [filter, value] = formatter(key, object);
        if (filter())
            return value();
    }
    return false
}


// Maps through object type and adds the column definitions to the array of column definitions.
export function formatGridColumnsDefinition(data: object): GridColDef[] {
    const columns: GridColDef[] = []
    const fieldEntries = Object.entries(data);
    fieldEntries.map(([key, val]) => {
        const column = getColumnsFor(key, val);
        if (typeof(column) === "object")
            columns.push(column);
    })
    columns.push(ButtonsColumn())
    return columns;

}