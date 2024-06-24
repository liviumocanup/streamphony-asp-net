import {
  CreateSongData,
  EditSongData,
} from '../../../shared/interfaces/Interfaces';
import axios from 'axios';
import { API_URL, SONG_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useEditSong = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const editSong = async (songData: EditSongData) => {
    try {
      const res = await axios.put(
        `${API_URL}/${SONG_ENDPOINT}`,
        songData,
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
    mutationFn: editSong,
  });
};

export default useEditSong;
