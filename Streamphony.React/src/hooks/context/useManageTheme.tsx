import { useCallback, useState } from 'react';
import DarkTheme from '../../themes/DarkTheme';
import LightTheme from '../../themes/LightTheme';
import useThemeStorage from '../localStorage/useThemeStorage';

const useManageTheme = () => {
  const { getTheme, setTheme: setStorageTheme } = useThemeStorage();

  const [activeTheme, setActiveTheme] = useState(() => {
    const storedTheme = getTheme();
    return storedTheme === 'light' ? LightTheme : DarkTheme;
  });

  const toggleTheme = useCallback(() => {
    const isLight = activeTheme === LightTheme;
    const newTheme = isLight ? DarkTheme : LightTheme;

    setActiveTheme(newTheme);
    setStorageTheme(isLight ? 'dark' : 'light');
  }, [activeTheme, setStorageTheme]);

  return { activeTheme, toggleTheme };
};

export default useManageTheme;
