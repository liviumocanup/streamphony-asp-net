import { ThemeProvider, createTheme } from '@mui/material/styles'
import Home from './features/home/Home'
import { useEffect, useState } from 'react';

const lightTheme = createTheme({
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

const darkTheme = createTheme({
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

const App = () => {
  const [currentTheme, setCurrentTheme] = useState(lightTheme);

  const toggleTheme = () => {
    setCurrentTheme(currentTheme === lightTheme ? darkTheme : lightTheme);
  };

  useEffect(() => {
    document.body.style.backgroundColor = currentTheme.palette.background.default;
  }, [currentTheme]);

  return (
    <ThemeProvider theme={currentTheme}>
      <Home toggleTheme={toggleTheme} />
    </ThemeProvider>
  );
};

export default App
