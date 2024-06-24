import { useSuspenseQuery } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, SONG_ENDPOINT } from '../../../shared/constants';

const useGetPopularSongs = (pageNumber = 1, pageSize = 10) => {
  const getSongs = async () => {
    try {
      const res = await axios.get(`${API_URL}/${SONG_ENDPOINT}`, {
        params: { pageNumber, pageSize },
      });
      const response = res.data;

      return {
        items: response.items,
        totalRecords: response.totalRecords,
      };
    } catch (err: any) {
      console.error('Failed to fetch songs:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching songs');
    }
  };

  return useSuspenseQuery({
    queryKey: ['songs', pageNumber, pageSize],
    queryFn: getSongs,
  });
};

export default useGetPopularSongs;
