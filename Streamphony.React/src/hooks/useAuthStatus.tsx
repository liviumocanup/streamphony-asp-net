import { useEffect, useState } from 'react';
import useTokenStorage from './localStorage/useTokenStorage';

const useAuthStatus = () => {
  const { getToken } = useTokenStorage();
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(
    () => getToken() !== null,
  );

  useEffect(() => {
    const token = getToken();
    setIsLoggedIn(token !== null);
  }, [getToken]);

  return isLoggedIn;
};

export default useAuthStatus;
