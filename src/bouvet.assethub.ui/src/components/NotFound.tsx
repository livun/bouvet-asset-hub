import WarningAmberIcon from '@mui/icons-material/WarningAmber';
import { Box, Typography } from '@mui/material';
export default function NotFound(props: {message : String}) {

    return (
            <Box sx={{ display: 'flex', height:"100%"}}>
                <Box sx={{margin: "auto", textAlign: "center"}}>
                    <WarningAmberIcon sx={{fontSize: "100px"}} />
                    <Typography variant="h4">404 - Not Found</Typography>
                    <Typography variant="subtitle1">{props.message}</Typography>
                </Box>
            </Box>   
    );
}