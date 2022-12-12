import { createTheme } from "@mui/material";

export const theme = createTheme({
	components: {
		MuiSpeedDialAction: {
			styleOverrides: {
				staticTooltipLabel: {
					width: 130
				}
			}
		}
	}
})