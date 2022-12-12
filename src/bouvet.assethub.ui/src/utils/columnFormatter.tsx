import { GridColDef } from "@mui/x-data-grid";
import { TableButtonsColumnForAssets } from "../components/TableButtonsColumnForAssets";
import { TableButtonsColumnForLoans } from "../components/TableButtonsColumnForLoans";
import { columnFormatProviders } from "./columnFormatProviders";
import { IFormatInput } from "./interfaces";

// Loops through all formatters and renders column definitions based on properties of objects. 
function getColumnsFor({key, object}: IFormatInput) {
    for (const formatter of columnFormatProviders) {
        const {filter, value} = formatter({key, object});
        if (filter())
            return value();
    }
    return undefined
}

// Maps through object type and adds the column definitions to the array of column definitions.
export function formatGridColumnsDefinition(data: object, pathname: string): GridColDef[] {
    const columns: GridColDef[] = []
    const fieldEntries = Object.entries(data);
    fieldEntries.forEach(([key, object]) => {
        const column = getColumnsFor({key, object});
        if (typeof (column) === "object")
            columns.push(column);
    })

    switch (pathname){
        case "/assets":
            const assetsCol = TableButtonsColumnForAssets()
            columns.push(assetsCol)
            break;
        case "/loans" :
            const loansCol = TableButtonsColumnForLoans()
            columns.push(loansCol)
            break;
    }
    return  columns;
}