import { Navigate, useLocation } from 'react-router-dom';
import React from 'react';
import useAuthContext from '../hooks/context/useAuthContext';
import { LOG_IN_ROUTE } from './routes';

interface ProtectedRouteProps {
  children: React.ReactNode;
}

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
  const { isLoggedIn } = useAuthContext();
  const location = useLocation();

  if (!isLoggedIn) {
    return <Navigate to={LOG_IN_ROUTE} state={{ from: location }} replace />;
  }

  return children;
};

export default ProtectedRoute;
