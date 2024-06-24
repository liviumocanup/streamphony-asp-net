import { useCallback, useState, useEffect } from 'react';
import useTokenStorage from '../localStorage/useTokenStorage';

const useManageAuth = () => {
  const { getToken, setToken, removeToken, getTokenClaims } = useTokenStorage();

  const isTokenStored = useCallback(() => {
    return getToken() !== null;
  }, [getToken]);

  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(() => isTokenStored());
  const [isArtist, setIsArtist] = useState<boolean>(false);

  useEffect(() => {
    const claims = getTokenClaims();
    if (claims) {
      const artistId = claims.artistId;
      setIsArtist(
        artistId !== undefined && artistId !== null && artistId !== '',
      );
    }
  }, [getTokenClaims]);

  const tokenRefresh = useCallback(
    (token: string) => {
      setToken(token);
      const claims = getTokenClaims();
      if (claims) {
        const artistId = claims.artistId;
        setIsArtist(
          artistId !== undefined && artistId !== null && artistId !== '',
        );
      }
      setIsLoggedIn(isTokenStored());
    },
    [setToken, getTokenClaims, isTokenStored],
  );

  const logOut = () => {
    removeToken();
    setIsLoggedIn(false);
    setIsArtist(false);
  };

  return { isLoggedIn, isArtist, getToken, tokenRefresh, logOut };
};

export default useManageAuth;
