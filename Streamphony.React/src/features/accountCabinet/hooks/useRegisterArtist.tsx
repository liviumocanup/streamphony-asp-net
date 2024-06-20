import { RegisterArtistData } from '../../../shared/Interfaces';
import axios from 'axios';
import { API_URL, REGISTER_ARTIST_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';
import useArtistContext from '../../../hooks/context/useArtistContext';

const useRegisterArtist = () => {
  const { getToken } = useAuthContext();
  const token = getToken();
  const { setArtistId, setFirstName, setLastName, setPfpUrl } =
    useArtistContext();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const addArtist = async (newArtist: RegisterArtistData) => {
    try {
      const res = await axios.post(
        `${API_URL}/${REGISTER_ARTIST_ENDPOINT}`,
        newArtist,
        config,
      );

      console.log('Response: ', res.data);

      const response = res.data;

      const artist = {
        id: response.id,
        firstName: response.firstName,
        lastName: response.lastName,
        dateOfBirth: response.dateOfBirth,
        pfpUrl: response.profilePictureUrl,
      };

      setArtistId(artist.id);
      setFirstName(artist.firstName);
      setLastName(artist.lastName);
      setPfpUrl(artist.pfpUrl);

      return;
    } catch (err: any) {
      console.log(err);
      if (axios.isAxiosError(err)) {
        throw new Error(err.message);
      } else {
        throw new Error(err.response.data.errors);
      }
    }
  };

  return useMutation({
    mutationFn: addArtist,
  });
};

export default useRegisterArtist;
