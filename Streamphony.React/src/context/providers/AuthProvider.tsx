import { ReactNode, useMemo } from 'react';
import useManageAuth from '../../hooks/useManageAuth';
import AuthContext from '../AuthContext';

const AuthProvider = ({ children }: { children: ReactNode }) => {
  const auth = useManageAuth();

  const contextValue = useMemo(() => auth, [auth]);

  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
};

export default AuthProvider;
