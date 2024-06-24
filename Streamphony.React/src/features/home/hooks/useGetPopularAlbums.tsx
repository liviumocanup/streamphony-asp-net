import { useSuspenseQuery } from '@tanstack/react-query';
import axios from 'axios';
import { ALBUM_ENDPOINT, API_URL } from '../../../shared/constants';

const useGetPopularAlbums = (pageNumber = 1, pageSize = 10) => {
  const getAlbums = async () => {
    try {
      const res = await axios.get(`${API_URL}/${ALBUM_ENDPOINT}`, {
        params: { pageNumber, pageSize },
      });
      const response = res.data;

      return {
        items: response.items,
        totalRecords: response.totalRecords,
      };
    } catch (err: any) {
      console.error('Failed to fetch albums:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching albums');
    }
  };

  return useSuspenseQuery({
    queryKey: ['albums', pageNumber, pageSize],
    queryFn: getAlbums,
  });
};

export default useGetPopularAlbums;
