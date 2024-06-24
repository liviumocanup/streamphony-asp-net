import { useSuspenseQuery } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, ARTIST_ENDPOINT } from '../../../shared/constants';

const useGetPopularArtists = (pageNumber = 1, pageSize = 10) => {
  const getArtists = async () => {
    try {
      //wait 5 seconds
      // await new Promise((resolve) => setTimeout(resolve, 5000));
      const res = await axios.get(`${API_URL}/${ARTIST_ENDPOINT}`, {
        params: { pageNumber, pageSize },
      });
      const response = res.data;

      return {
        items: response.items,
        totalRecords: response.totalRecords,
      };
    } catch (err: any) {
      console.error('Failed to fetch artists:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching artists');
    }
  };

  return useSuspenseQuery({
    queryKey: ['artists', pageNumber, pageSize],
    queryFn: getArtists,
  });
};

export default useGetPopularArtists;
