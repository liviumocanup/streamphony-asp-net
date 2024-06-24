import { createTheme } from '@mui/material/styles';

const DarkTheme = createTheme({
  palette: {
    mode: 'dark',
    text: {
      primary: '#ffffff',
    },
    background: {
      default: '#121212',
      paper: '#171717',
    },
    action: {
      hover: '#232323',
    },
  },
});

export default DarkTheme;
