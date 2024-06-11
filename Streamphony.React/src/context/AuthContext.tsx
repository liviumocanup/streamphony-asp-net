import { createContext } from 'react';

type AuthContextType = {
  isLoggedIn: boolean;
  tokenRefresh: () => void;
  handleLogOut: () => void;
};

const defaultContextValue: AuthContextType = {
  isLoggedIn: false,
  tokenRefresh: () => {},
  handleLogOut: () => {},
};

const AuthContext = createContext<AuthContextType>(defaultContextValue);

export default AuthContext;
