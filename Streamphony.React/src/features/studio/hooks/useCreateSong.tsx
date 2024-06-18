import { CreateSongData } from '../../../shared/Interfaces';
import axios from 'axios';
import { API_URL, CREATE_SONG_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useCreateSong = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const addSong = async (newSong: CreateSongData) => {
    try {
      const res = await axios.post(
        `${API_URL}/${CREATE_SONG_ENDPOINT}`,
        newSong,
        config,
      );

      const song = res.data.song;

      console.log(res.data);

      return song;
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
    mutationFn: addSong,
  });
};

export default useCreateSong;
