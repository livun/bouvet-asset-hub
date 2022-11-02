import { DataGrid } from '@mui/x-data-grid';
import { useLocation, useParams } from 'react-router-dom';
import { TableProps } from '../utils/props';
import { formatGridColumnsDefinition } from '../utils/tableFormatter';

export default function DataGridTable<T extends Object>(props: TableProps<T>) {
    const { rows } = props
    const gridColDef = formatGridColumnsDefinition(rows[0])

    return <>
        <div style={{ display: "flex", height: "100%" }}>
            <div style={{ flexGrow: 1 }}>
                <DataGrid
                    rows={rows}
                    rowHeight={45}
                    columns={gridColDef}
                    pageSize={10}
                    rowsPerPageOptions={[10]}
                    checkboxSelection
                    //disableSelectionOnClick
                />
            </div>
        </div>
    </>
};