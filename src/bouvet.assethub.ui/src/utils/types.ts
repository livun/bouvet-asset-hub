import { GridColDef } from "@mui/x-data-grid"

export type formatResult = {
    filter : () => boolean,
    value :  () => GridColDef

}