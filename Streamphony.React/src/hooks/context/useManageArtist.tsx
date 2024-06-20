import { useMemo, useState } from 'react';

const useManageArtist = () => {
  const [artistId, setArtistId] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [pfpUrl, setPfpUrl] = useState('');

  const isArtistLinked = useMemo(() => artistId !== '', [artistId]);

  return {
    isArtistLinked,
    artistId,
    setArtistId,
    pfpUrl,
    setPfpUrl,
    firstName,
    setFirstName,
    lastName,
    setLastName,
  };
};

export default useManageArtist;
