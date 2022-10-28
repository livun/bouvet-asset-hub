import * as React from 'react';
import Box from '@mui/material/Box';
import { DataGrid, GridColDef, GridValueGetterParams } from '@mui/x-data-grid';
import { AssetResponseDto, LoanResponseDto } from '../__generated__/api-types';
import { Status } from '../types/enums';
import { TableProps } from '../types/props';


const lookupKeysMapper: any = {
    'serialNumberValue': 'Serial Number',
    'categoryName': 'Category',
    'intervalStart': 'Start Date',
    'intervalStop': 'Stop Date',
    'intervalIsLongterm': 'Longterm',
    'assignedToValue': 'Borrower',
    'bsdReference': 'BSD',
    'borrowerEmployeeNumberValue' : 'Borrower',
    'assetCategoryName': 'Asset Category'
};
const formatHeaderKeys = (key: string) => {
    const result = lookupKeysMapper[key];
    if (result !== undefined){
        return result
    }
    // Capitalize first letter and split the string on capitalized letters
    return key.split(/(?=[A-Z])/).map((p) => { return p[0].toUpperCase() + p.slice(1); }).join(' ');
}




function formatString (key: string, object: any){
    const filter = () => typeof(object) === "string";
    const value = () => {
        const col : GridColDef = {
            field: key,
                headerName: formatHeaderKeys(key),
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
        console.log("hi i am a bool")
        const col : GridColDef = {
            field: key,
                headerName: formatHeaderKeys(key),
                flex: 0.5,
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
                flex: 0.5,
                //width: 150,
                type: typeof (object)

        }
        return col
    }
    return [filter, value]
}

// Value getter for Status - maps number to "Status enum"
const formatStatusValue = (params: GridValueGetterParams)  => {
    return `${Status[params.row.status]}`
    }
function formatStatus (key: string, object: any){
    const filter = () => key === "status"
    const value = () => {
        const col : GridColDef = {
            field: key,
                headerName: formatHeaderKeys(key),
                flex: 1,
                type: typeof (object),
                valueGetter: formatStatusValue,
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
                flex: 1,
                //width: 150,
                type: typeof (object)

        }
        return col
    }
    return [filter, value]
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
function formatGridColumnsDefinition(data: object): GridColDef[] {
    const columns: GridColDef[] = []
    const fieldEntries = Object.entries(data);
    fieldEntries.map(([key, val]) => {
        const column = getColumnsFor(key, val);
        if (typeof(column) === "object")
            columns.push(column);
      
    })
    return columns;

}

export default function DataGridTable<T extends Object>(props: TableProps<T>)  {
    const { rows } = props
    const gridColDef = formatGridColumnsDefinition(rows[0])


    return (<>
        <div style={{ display: "flex", height: "100%" }}>
            <div style={{ flexGrow: 1 }}>
                <DataGrid
                    rows={rows}
                    rowHeight={45}
                    //getRowHeight={() => 'auto'}
                    columns={gridColDef}
                    pageSize={10}
                    rowsPerPageOptions={[10]}
                    checkboxSelection
                    disableSelectionOnClick
                    experimentalFeatures={{ newEditingApi: true }}
                />

            </div>

        </div>
    </>


    );
};