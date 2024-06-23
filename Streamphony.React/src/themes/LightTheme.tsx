import { createTheme } from '@mui/material/styles';

const LightTheme = createTheme({
  palette: {
    mode: 'light',
    text: {
      primary: '#000',
    },
    background: {
      default: '#edede9',
      paper: '#e3d5ca',
    },
    action: {
      hover: '#d5bdaf',
    },
  },
});

export default LightTheme;
