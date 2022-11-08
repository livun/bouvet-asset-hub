import { GridColDef } from "@mui/x-data-grid";
import { TableButtonsColumnForAssets } from "../components/TableButtonsColumnForAssets";
import { TableButtonsColumnForLoans } from "../components/TableButtonsColumnForLoans";
import { StatusEnum } from "./enums";
import { lookupKeysMapper, statusChecker, statusMapper } from "./mappers";
import { capitalizeAndSplit } from "./regex";



export const formatHeaderKeys = (key: string) => {
    const result = lookupKeysMapper[key];
    if (result !== undefined) {
        return result
    }
    return capitalizeAndSplit(key);
}

function formatString(key: string, object: any) {
    const filter = () => typeof (object) === "string";
    const value = () => {
        const col: GridColDef = {
            field: key,
            headerName: formatHeaderKeys(key),
            headerAlign: "left",
            align: "left",
            flex: 1,
            type: typeof (object)
        }
        return col
    }
    return [filter, value]
}

function formatSerialNumber(key: string, object: any) {
    const filter = () => key.toLocaleLowerCase().includes("serial")
    const value = () => {
        const col: GridColDef = {
            field: key,
            headerName: formatHeaderKeys(key),
            headerAlign: "left",
            align: "left",
            flex: 1,
            type: 'dateTime',
            valueFormatter(params) {
                if (params.value === 0) {
                    return ""
                }
                return params.value
            },
        }
        return col
    }
    return [filter, value]
}

function formatBooleans(key: string, object: any) {
    const filter = () => typeof (object) === "boolean"
    const value = () => {
        const col: GridColDef = {
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

function formatId(key: string, object: any) {
    const filter = () => key.toLocaleLowerCase().includes("id")
    const value = () => {
        const col: GridColDef = {
            field: key,
            headerName: formatHeaderKeys(key),
            headerAlign: "left",
            align: "left",
            flex: 0.3,
            type: 'string'
        }
        return col
    }
    return [filter, value]
}
function formatCatId(key: string, object: any) {
    const filter = () => key.toLocaleLowerCase().includes("categoryid")
    const value = () => {

        return false
    }
    return [filter, value]
}


function formatStatus(key: string, object: any) {
    const filter = () => key.toLocaleLowerCase().includes("status")
    const value = () => {
        const col: GridColDef = {
            field: key,
            headerName: formatHeaderKeys(key),
            headerAlign: "left",
            align: "left",
            flex: 1,
            valueGetter: ({ value }) => StatusEnum[value],
        }
        return col
    }
    return [filter, value]
}

function formatDate(key: string, object: any) {
    const filter = () => ["intervalstart", "intervalstop", "returndate"].includes(key.toLocaleLowerCase())
    const value = () => {
        const col: GridColDef = {
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
function formatGeneral(key: string, object: any) {
    const filter = () => typeof (key) === "string";
    const value = () => {
        const col: GridColDef = {
            field: key,
            headerName: formatHeaderKeys(key),
            headerAlign: "left",
            align: "left",
            flex: 1,
            type: "string"
        }
        return col
    }
    return [filter, value]
}

// Array of formatproviders, the less generic in the beginning of array
const formatProviders = [formatCatId, formatSerialNumber, formatId, formatBooleans, formatDate, formatStatus, formatString, formatGeneral]

// Loops through all formatters and renders column definitions based on properties of objects. 
function getColumnsFor(key: string, object: any) {
    for (const formatter of formatProviders) {
        const [filter, value] = formatter(key, object);
        if (filter())
            return value();
    }
    return false
}


// Maps through object type and adds the column definitions to the array of column definitions.
export function formatGridColumnsDefinition(data: object, pathname: string): GridColDef[] {
    const columns: GridColDef[] = []
    const fieldEntries = Object.entries(data);
    fieldEntries.map(([key, val]) => {
        const column = getColumnsFor(key, val);
        if (typeof (column) === "object")
            columns.push(column);
    })

    if (pathname === "/assets") {
        const col = TableButtonsColumnForAssets()
        columns.push(col)
    }
    if (pathname === "/loans") {
        const { col } = TableButtonsColumnForLoans()
        columns.push(col)
    }
   
    return  columns;

}