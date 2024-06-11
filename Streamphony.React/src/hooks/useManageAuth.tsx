import { useCallback, useState } from 'react';
import useTokenStorage from './localStorage/useTokenStorage';

const useManageAuth = () => {
  const { getToken, removeToken } = useTokenStorage();

  const isTokenStored = useCallback(() => {
    return getToken() !== null;
  }, [getToken]);

  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(() => isTokenStored());

  const tokenRefresh = useCallback(() => {
    setIsLoggedIn(isTokenStored());
  }, [isTokenStored]);

  const handleLogOut = () => {
    removeToken();
    setIsLoggedIn(false);
  };

  return { isLoggedIn, tokenRefresh, handleLogOut };
};

export default useManageAuth;
