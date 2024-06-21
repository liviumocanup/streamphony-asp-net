import { useAuth0 } from '@auth0/auth0-react';
import { useCallback } from 'react';

const useAuth = () => {
  const { loginWithRedirect, logout, isAuthenticated, isLoading, user } =
    useAuth0();

  const logIn = useCallback(() => loginWithRedirect(), [loginWithRedirect]);
  const logOut = useCallback(
    () => logout({ logoutParams: { returnTo: window.location.origin } }),
    [logout],
  );

  return { logIn, logOut, isAuthenticated, isLoading, user };
};

export default useAuth;
