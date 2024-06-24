import { Navigate, useLocation } from 'react-router-dom';
import React from 'react';
import { REGISTER_ARTIST_ROUTE } from './routes';
import ProtectedRoute from './ProtectedRoute';
import useAuthContext from '../hooks/context/useAuthContext';

interface ArtistRouteProps {
  children: React.ReactNode;
}

const ArtistRoute = ({ children }: ArtistRouteProps) => {
  const { isArtist } = useAuthContext();
  const location = useLocation();

  if (!isArtist) {
    return (
      <Navigate to={REGISTER_ARTIST_ROUTE} state={{ from: location }} replace />
    );
  }

  return <ProtectedRoute>{children}</ProtectedRoute>;
};

export default ArtistRoute;
