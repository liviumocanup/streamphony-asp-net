import { createTheme } from '@mui/material/styles';

const LightTheme = createTheme({
  palette: {
    mode: 'light',
    text: {
      primary: '#213547',
    },
    background: {
      default: '#ffffff',
      paper: '#f9f9f9',
    },
    action: {
      hover: '#ececec',
    },
  },
});

export default LightTheme;
