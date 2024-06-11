import { Navigate, useLocation } from 'react-router-dom';
import React from 'react';
import useAuthContext from '../hooks/context/useAuthContext';

interface GuestRouteProps {
  children: React.ReactNode;
}

const GuestRoute = ({ children }: GuestRouteProps) => {
  const { isLoggedIn } = useAuthContext();
  const location = useLocation();

  if (isLoggedIn) {
    return <Navigate to="/" state={{ from: location }} replace />;
  }

  return children;
};

export default GuestRoute;
