import { useCallback, useEffect, useState } from 'react';
import useTokenStorage from './localStorage/useTokenStorage';

const useAuthStatus = () => {
  const { getToken, removeToken } = useTokenStorage();
  const isTokenStored = useCallback(() => {
    return getToken() !== null;
  }, [getToken]);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(() => isTokenStored());

  const handleLogOut = () => {
    removeToken();
    setIsLoggedIn(false);
  };

  useEffect(() => {
    const handleStorageChange = () => {
      setIsLoggedIn(isTokenStored());
    };

    window.addEventListener('storage', handleStorageChange);

    handleStorageChange();

    return () => window.removeEventListener('storage', handleStorageChange);
  }, [isTokenStored]);

  return { isLoggedIn, handleLogOut };
};

export default useAuthStatus;
