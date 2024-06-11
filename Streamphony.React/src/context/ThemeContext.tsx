import { createContext } from 'react';
import { Theme } from '@mui/material/styles';
import DarkTheme from '../themes/DarkTheme';

type ThemeContextType = {
  activeTheme: Theme;
  toggleTheme: () => void;
};

const defaultContextValue: ThemeContextType = {
  activeTheme: DarkTheme,
  toggleTheme: () => {},
};

const ThemeContext = createContext<ThemeContextType>(defaultContextValue);

export default ThemeContext;
