import useLocalStorage from './useLocalStorage';
import { lsThemeKey } from '../../shared/constants';

const useThemeStorage = () => {
  const { getItem, setItem } = useLocalStorage(lsThemeKey);

  const getTheme = getItem;
  const setTheme = (theme: string) => setItem(theme);

  return {
    getTheme,
    setTheme,
  };
};

export default useThemeStorage;
