import { CreateAlbumData } from '../../../shared/interfaces/Interfaces';
import axios from 'axios';
import { ALBUM_ENDPOINT, API_URL } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useCreateAlbum = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const addAlbum = async (newAlbum: CreateAlbumData) => {
    console.log('newAlbum: ', newAlbum);

    try {
      const res = await axios.post(
        `${API_URL}/${ALBUM_ENDPOINT}`,
        newAlbum,
        config,
      );

      console.log('Response: ', res.data);

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
    mutationFn: addAlbum,
  });
};

export default useCreateAlbum;
