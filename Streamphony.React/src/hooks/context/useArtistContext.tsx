import { useContext } from 'react';
import ArtistContext from '../../context/ArtistContext';

const useArtistContext = () => useContext(ArtistContext);

export default useArtistContext;
