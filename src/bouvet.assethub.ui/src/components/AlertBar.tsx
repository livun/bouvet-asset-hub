import * as React from 'react';
import MuiAlert, { AlertProps } from '@mui/material/Alert';
import { Snackbar } from '@mui/material';

const AlertComp = React.forwardRef<HTMLDivElement, AlertProps>(function Alert(props, ref,) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export default function AlertBar( props: { open: boolean, handleClose: (event?: React.SyntheticEvent | Event, reason?: string) => void, message: string, success: boolean } ) {
    const { open, handleClose, message, success } = props;

    return (
        <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
            { success
            ?<AlertComp onClose={handleClose} severity="success" sx={{ width: '100%' }}>{message}</AlertComp> 
            :<AlertComp onClose={handleClose} severity="error" sx={{ width: '100%' }}>{message}</AlertComp>}
        </Snackbar>
    );
}