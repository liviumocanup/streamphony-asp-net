import { createContext } from 'react';

type ArtistContextType = {
  isArtistLinked: boolean;
  artistId: string;
  setArtistId: (id: string) => void;
  firstName: string;
  setFirstName: (name: string) => void;
  lastName: string;
  setLastName: (name: string) => void;
  pfpUrl: string;
  setPfpUrl: (url: string) => void;
};

const defaultContextValue: ArtistContextType = {
  isArtistLinked: false,
  artistId: '',
  setArtistId: () => {},
  firstName: '',
  setFirstName: () => {},
  lastName: '',
  setLastName: () => {},
  pfpUrl: '',
  setPfpUrl: () => {},
};

const ArtistContext = createContext<ArtistContextType>(defaultContextValue);

export default ArtistContext;
