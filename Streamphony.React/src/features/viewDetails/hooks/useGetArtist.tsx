import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, ARTIST_ENDPOINT } from '../../../shared/constants';

const useGetArtist = (artistId: string | undefined) => {
  const getArtist = async () => {
    try {
      const res = await axios.get(`${API_URL}/${ARTIST_ENDPOINT}/${artistId}`);

      const album = res.data;

      console.log(res.data);

      return album;
    } catch (err: any) {
      console.error('Failed to fetch artist:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching artist');
    }
  };

  return useQuery({
    queryKey: ['artist', artistId],
    queryFn: getArtist,
    enabled: !!artistId,
  });
};

export default useGetArtist;
