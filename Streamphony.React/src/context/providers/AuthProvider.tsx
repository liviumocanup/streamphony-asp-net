import { ReactNode, useMemo } from 'react';
import useManageAuth from '../../hooks/useManageAuth';
import AuthContext from '../AuthContext';

const AuthProvider = ({ children }: { children: ReactNode }) => {
  const { isLoggedIn, tokenRefresh, handleLogOut } = useManageAuth();

  const contextValue = useMemo(
    () => ({
      isLoggedIn,
      tokenRefresh,
      handleLogOut,
    }),
    [isLoggedIn, tokenRefresh, handleLogOut],
  );

  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
};

export default AuthProvider;
