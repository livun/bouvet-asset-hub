import { GridColDef } from "@mui/x-data-grid";
import { StatusEnum } from "./enums";
import { IFormatInput, IFormatOutput } from "./interfaces";
import { lookupKeysMapper } from "./mappers";
import { capitalizeAndSplit } from "./regex";

const formatHeaderKeys = (key: string) => {
    const result = lookupKeysMapper[key];
    if (result !== undefined) {
        return result
    }
    return capitalizeAndSplit(key);
}
function formatString({key, object} : IFormatInput) : IFormatOutput {
    
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
    return {filter, value}
}

function formatSerialNumber({key} : IFormatInput) : IFormatOutput {
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
    return {filter, value}
}
function formatBooleans({key, object} : IFormatInput) : IFormatOutput {
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
    return {filter, value}
}

function formatId({key} : IFormatInput) : IFormatOutput {
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
    return {filter, value}
}
function formatCatId({key} : IFormatInput) : IFormatOutput {
    const filter = () => key.toLocaleLowerCase().includes("categoryid")
    const value = () => {
        return undefined
    }
    return {filter, value}
}

function formatStatus({key} : IFormatInput) : IFormatOutput {
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
    return {filter, value}
}

function formatDate({key} : IFormatInput) : IFormatOutput {
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
    return {filter, value}
}
function formatGeneral({key} : IFormatInput) : IFormatOutput {
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
    return {filter, value}
}

export const columnFormatProviders = [formatCatId, formatSerialNumber, formatId, formatBooleans, formatDate, formatStatus, formatString, formatGeneral]
 