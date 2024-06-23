import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import {
  API_URL,
  CURRENT_USER_ALBUMS_ENDPOINT,
} from '../../../shared/constants';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useGetCurrentArtistAlbums = () => {
  const { isArtist, getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getAlbums = async () => {
    try {
      const res = await axios.get(
        `${API_URL}/${CURRENT_USER_ALBUMS_ENDPOINT}`,
        config,
      );

      const items = res.data;

      console.log(res.data);

      return items;
    } catch (err: any) {
      console.error('Failed to fetch albums:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching albums');
    }
  };

  return useQuery({
    queryKey: ['albumsDashboard', token],
    queryFn: getAlbums,
    enabled: !!token && isArtist,
    retry: false,
  });
};

export default useGetCurrentArtistAlbums;
