import { LS_TOKEN_KEY } from '../../shared/constants';
import useLocalStorage from './useLocalStorage';

const useTokenStorage = () => {
  const { getItem, setItem, removeItem } = useLocalStorage(LS_TOKEN_KEY);

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
