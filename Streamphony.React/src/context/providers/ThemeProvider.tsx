import { ReactNode, useMemo } from 'react';
import { ThemeProvider as MUIThemeProvider } from '@mui/material/styles';
import ThemeContext from '../ThemeContext';
import { CssBaseline } from '@mui/material';
import useManageTheme from '../../hooks/useManageTheme';

const ThemeProvider = ({ children }: { children: ReactNode }) => {
  const { activeTheme, toggleTheme } = useManageTheme();

  const contextValue = useMemo(
    () => ({
      activeTheme,
      toggleTheme,
    }),
    [activeTheme, toggleTheme],
  );

  return (
    <ThemeContext.Provider value={contextValue}>
      <MUIThemeProvider theme={contextValue.activeTheme}>
        <CssBaseline />
        {children}
      </MUIThemeProvider>
    </ThemeContext.Provider>
  );
};

export default ThemeProvider;
