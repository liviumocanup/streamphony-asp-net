import { createContext } from 'react';

type AuthContextType = {
  isLoggedIn: boolean;
  isArtist: boolean;
  getToken: () => string | null;
  tokenRefresh: (token: string) => void;
  logOut: () => void;
};

const defaultContextValue: AuthContextType = {
  isLoggedIn: false,
  isArtist: false,
  getToken: () => null,
  tokenRefresh: () => {},
  logOut: () => {},
};

const AuthContext = createContext<AuthContextType>(defaultContextValue);

export default AuthContext;
