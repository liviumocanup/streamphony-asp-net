import { useCallback, useState } from 'react';
import DarkTheme from '../themes/DarkTheme';
import LightTheme from '../themes/LightTheme';
import useThemeStorage from './localStorage/useThemeStorage';

const useManageTheme = () => {
  const { getTheme, setTheme: setStorageTheme } = useThemeStorage();

  const [activeTheme, setActiveTheme] = useState(() => {
    const storedTheme = getTheme();
    return storedTheme === 'light' ? LightTheme : DarkTheme;
  });

  const toggleTheme = useCallback(() => {
    setActiveTheme((currentTheme) => {
      const isLight = currentTheme === LightTheme;
      const newTheme = isLight ? DarkTheme : LightTheme;

      setStorageTheme(isLight ? 'dark' : 'light');
      return newTheme;
    });
  }, [setStorageTheme]);

  return { activeTheme, toggleTheme };
};

export default useManageTheme;
