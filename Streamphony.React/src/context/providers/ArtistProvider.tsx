import { ReactNode, useMemo } from 'react';
import useManageArtist from '../../hooks/context/useManageArtist';
import ArtistContext from '../ArtistContext';

const ArtistProvider = ({ children }: { children: ReactNode }) => {
  const artist = useManageArtist();

  const contextValue = useMemo(() => artist, [artist]);

  return (
    <ArtistContext.Provider value={contextValue}>
      {children}
    </ArtistContext.Provider>
  );
};

export default ArtistProvider;
