import { Navigate, useLocation } from 'react-router-dom';
import React from 'react';
import useAuthStatus from '../hooks/useAuthStatus';

interface ProtectedRouteProps {
  children: React.ReactNode;
}

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
  const isLoggedIn = useAuthStatus();
  const location = useLocation();

  if (!isLoggedIn) {
    return <Navigate to="/logIn" state={{ from: location }} replace />;
  }

  return children;
};

export default ProtectedRoute;
