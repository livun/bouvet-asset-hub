import { Box } from "@mui/material";
import { useState } from "react";
import MobileMenu from "../components/MobileMain";
import QRScanner from "../components/QRScanner";

export default function Mobile() {
   
    return <>

        <MobileMenu open={false} />

        <Box sx={{ marginTop: "56px", width: "100%", height: "90vh", border: "solid 4px pink" }}>
            <QRScanner />


        </Box>

    </>
}