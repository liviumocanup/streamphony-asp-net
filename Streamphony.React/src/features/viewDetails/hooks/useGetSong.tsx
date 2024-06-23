import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, SONG_ENDPOINT } from '../../../shared/constants';

const useGetSong = (songId: string | undefined) => {
  const getSong = async () => {
    try {
      const res = await axios.get(`${API_URL}/${SONG_ENDPOINT}/${songId}`);

      const album = res.data;

      console.log(res.data);

      return album;
    } catch (err: any) {
      console.error('Failed to fetch song:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching song');
    }
  };

  return useQuery({
    queryKey: ['song', songId],
    queryFn: getSong,
    enabled: !!songId,
  });
};

export default useGetSong;
