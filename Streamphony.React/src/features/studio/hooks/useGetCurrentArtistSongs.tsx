import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import {
  API_URL,
  CURRENT_USER_SONGS_ENDPOINT,
} from '../../../shared/constants';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useGetCurrentArtistSongs = () => {
  const { isArtist, getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getSongs = async () => {
    try {
      const res = await axios.get(
        `${API_URL}/${CURRENT_USER_SONGS_ENDPOINT}`,
        config,
      );

      return res.data;
    } catch (err: any) {
      console.error('Failed to fetch songs:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching songs');
    }
  };

  return useQuery({
    queryKey: ['songsDashboard', token],
    queryFn: getSongs,
    enabled: !!token && isArtist,
    retry: false,
  });
};

export default useGetCurrentArtistSongs;
