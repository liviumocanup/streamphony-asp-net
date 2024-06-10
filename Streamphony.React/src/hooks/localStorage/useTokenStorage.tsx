import { lsTokenKey } from '../../shared/constants';
import useLocalStorage from './useLocalStorage';

const useTokenStorage = () => {
  const { getItem, setItem, removeItem } = useLocalStorage(lsTokenKey);

  const getToken = getItem;
  const setToken = (token: string) => setItem(token);
  const removeToken = removeItem;

  return {
    getToken,
    setToken,
    removeToken,
  };
};

export default useTokenStorage;
