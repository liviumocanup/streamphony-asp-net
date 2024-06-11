import { Navigate, useLocation } from 'react-router-dom';
import React from 'react';
import useAuthStatus from '../hooks/useAuthStatus';

interface GuestRouteProps {
  children: React.ReactNode;
}

const GuestRoute = ({ children }: GuestRouteProps) => {
  const { isLoggedIn } = useAuthStatus();
  const location = useLocation();

  if (isLoggedIn) {
    return <Navigate to="/" state={{ from: location }} replace />;
  }

  return children;
};

export default GuestRoute;
