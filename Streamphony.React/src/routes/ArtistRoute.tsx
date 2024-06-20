import { Navigate, useLocation } from 'react-router-dom';
import React from 'react';
import { REGISTER_ARTIST_ROUTE } from './routes';
import useArtistContext from '../hooks/context/useArtistContext';
import ProtectedRoute from './ProtectedRoute';

interface ArtistRouteProps {
  children: React.ReactNode;
}

const ArtistRoute = ({ children }: ArtistRouteProps) => {
  const { isArtistLinked } = useArtistContext();
  const location = useLocation();

  if (!isArtistLinked) {
    return (
      <Navigate to={REGISTER_ARTIST_ROUTE} state={{ from: location }} replace />
    );
  }

  return <ProtectedRoute>{children}</ProtectedRoute>;
};

export default ArtistRoute;
