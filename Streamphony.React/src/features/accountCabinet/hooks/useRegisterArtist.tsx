import { RegisterArtistData } from '../../../shared/interfaces/Interfaces';
import axios from 'axios';
import { API_URL, ARTIST_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useRegisterArtist = () => {
  const { getToken, tokenRefresh } = useAuthContext();
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
        `${API_URL}/${ARTIST_ENDPOINT}`,
        newArtist,
        config,
      );

      const token = res.data.accessToken;
      tokenRefresh(token);
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
