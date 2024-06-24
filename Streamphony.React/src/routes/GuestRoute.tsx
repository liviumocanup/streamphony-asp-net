import { Navigate, useLocation } from 'react-router-dom';
import React from 'react';
import useAuthContext from '../hooks/context/useAuthContext';
import { HOME_ROUTE } from './routes';

interface GuestRouteProps {
  children: React.ReactNode;
}

const GuestRoute = ({ children }: GuestRouteProps) => {
  const { isLoggedIn } = useAuthContext();
  const location = useLocation();

  if (isLoggedIn) {
    return <Navigate to={HOME_ROUTE} state={{ from: location }} replace />;
  }

  return children;
};

export default GuestRoute;
