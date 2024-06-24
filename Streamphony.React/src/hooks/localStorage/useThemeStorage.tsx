import useLocalStorage from './useLocalStorage';
import { LS_THEME_KEY } from '../../shared/constants';

const useThemeStorage = () => {
  const { getItem, setItem } = useLocalStorage(LS_THEME_KEY);

  const getTheme = getItem;
  const setTheme = (theme: string) => setItem(theme);

  return {
    getTheme,
    setTheme,
  };
};

export default useThemeStorage;
