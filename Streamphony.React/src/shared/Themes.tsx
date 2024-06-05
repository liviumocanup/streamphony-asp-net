import { createTheme } from "@mui/material/styles";

export const lightTheme = createTheme({
    palette: {
        mode: 'light',
        text: {
            primary: '#213547',
        },
        background: {
            default: '#ffffff',
            paper: '#e1e1e1',
        },
        action: {
            hover: '#d3d3d3',
        },
    },
});

export const darkTheme = createTheme({
    palette: {
        mode: 'dark',
        text: {
            primary: '#ffffff',
        },
        background: {
            default: '#121212',
            paper: '#222222',
        },
        action: {
            hover: '#333333',
        },
    },
});