import { ReactNode, useCallback, useMemo, useState } from 'react';
import DarkTheme from './DarkTheme';
import { ThemeProvider as MUIThemeProvider } from '@mui/material/styles';
import LightTheme from './LightTheme';
import ThemeContext from './ThemeContext';
import useThemeStorage from '../hooks/localStorage/useThemeStorage';
import { CssBaseline } from '@mui/material';

const ThemeProvider = ({ children }: { children: ReactNode }) => {
  const { getTheme: getStorageTheme, setTheme: setStorageTheme } =
    useThemeStorage();

  const [activeTheme, setActiveTheme] = useState(() => {
    const savedTheme = getStorageTheme();
    return savedTheme === 'light' ? LightTheme : DarkTheme;
  });

  const toggleTheme = useCallback(() => {
    const isLight = activeTheme === LightTheme;
    const newTheme = isLight ? DarkTheme : LightTheme;

    setActiveTheme(newTheme);
    setStorageTheme(isLight ? 'dark' : 'light');
  }, [activeTheme, setStorageTheme]);

  const contextValue = useMemo(
    () => ({
      currentTheme: activeTheme,
      toggleTheme,
    }),
    [activeTheme, toggleTheme],
  );

  return (
    <ThemeContext.Provider value={contextValue}>
      <MUIThemeProvider theme={contextValue.currentTheme}>
        <CssBaseline />
        {children}
      </MUIThemeProvider>
    </ThemeContext.Provider>
  );
};

export default ThemeProvider;
