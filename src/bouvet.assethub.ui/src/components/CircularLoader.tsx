import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';

export default function CircularLoader() {
    return (
        <Box sx={{ display: 'flex', height: "100%" }}>
            <CircularProgress style={{ margin: "auto" }} />
        </Box>
    );
}