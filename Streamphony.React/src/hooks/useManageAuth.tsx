import { useCallback, useState } from 'react';
import useTokenStorage from './localStorage/useTokenStorage';

const useManageAuth = () => {
  const { getToken, setToken, removeToken } = useTokenStorage();

  const isTokenStored = useCallback(() => {
    return getToken() !== null;
  }, [getToken]);

  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(() => isTokenStored());

  const tokenRefresh = useCallback(
    (token: string) => {
      setToken(token);
      setIsLoggedIn(isTokenStored());
    },
    [setToken, isTokenStored],
  );

  const logOut = () => {
    removeToken();
    setIsLoggedIn(false);
  };

  return { isLoggedIn, getToken, tokenRefresh, logOut };
};

export default useManageAuth;
