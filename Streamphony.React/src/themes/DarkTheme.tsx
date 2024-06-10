import { createTheme } from '@mui/material/styles';

const DarkTheme = createTheme({
  palette: {
    mode: 'dark',
    text: {
      primary: '#ffffff',
    },
    background: {
      default: '#212121',
      paper: '#171717',
    },
    action: {
      hover: '#2f2f2f',
    },
  },
});

export default DarkTheme;
