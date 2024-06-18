import { RegisterArtistData } from '../../../shared/Interfaces';
import axios from 'axios';
import { API_URL, REGISTER_ARTIST_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useRegisterArtist = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

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

      const artist = res.data.artist;

      console.log(res.data);

      return artist;
    } catch (err: any) {
      if (axios.isAxiosError(err)) {
        throw new Error(err.message);
      } else {
        console.log(err);
        throw new Error(err.response.data.errors);
      }
    }
  };

  return useMutation({
    mutationFn: addArtist,
  });
};

export default useRegisterArtist;
