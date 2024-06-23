import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { ALBUM_ENDPOINT, API_URL } from '../../../shared/constants';

const useGetAlbum = (albumId: string | undefined) => {
  const getAlbum = async () => {
    try {
      const res = await axios.get(`${API_URL}/${ALBUM_ENDPOINT}/${albumId}`);

      const album = res.data;

      console.log(res.data);

      return album;
    } catch (err: any) {
      console.error('Failed to fetch album:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching album');
    }
  };

  return useQuery({
    queryKey: ['album', albumId],
    queryFn: getAlbum,
    enabled: !!albumId,
  });
};

export default useGetAlbum;
