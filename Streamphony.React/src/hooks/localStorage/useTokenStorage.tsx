import { LS_TOKEN_KEY } from '../../shared/constants';
import useLocalStorage from './useLocalStorage';
import { jwtDecode } from 'jwt-decode';

const useTokenStorage = () => {
  const { getItem, setItem, removeItem } = useLocalStorage(LS_TOKEN_KEY);

  const getUserClaims = () => {
    const token = getItem();
    if (token) {
      try {
        const decoded = jwtDecode(token);

        return {
          firstName: decoded.FirstName,
          lastName: decoded.LastName,
        };
      } catch (error) {
        console.error('Failed to decode token:', error);
      }
    }
    return null;
  };

  const getToken = () => getItem();
  const setToken = (token: string) => setItem(token);
  const removeToken = removeItem;

  return {
    getToken,
    getUserClaims,
    setToken,
    removeToken,
  };
};

export default useTokenStorage;
