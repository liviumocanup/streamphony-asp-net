import { createContext } from 'react';
import { Theme } from '@mui/material/styles';
import DarkTheme from './DarkTheme';

type ThemeContextType = {
  currentTheme: Theme;
  toggleTheme: () => void;
};

const defaultContextValue: ThemeContextType = {
  currentTheme: DarkTheme,
  toggleTheme: () => {},
};

const ThemeContext = createContext<ThemeContextType>(defaultContextValue);

export default ThemeContext;
