import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, CREATE_SONG_ENDPOINT } from '../../../shared/constants';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const endpoint = `${CREATE_SONG_ENDPOINT}/current`;

const useGetCurrentUserSongs = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getSongs = async () => {
    try {
      const res = await axios.get(`${API_URL}/${endpoint}`, config);

      const items = res.data;

      console.log(res.data);

      return items;
    } catch (err: any) {
      console.error('Failed to fetch albums:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching albums');
    }
  };

  return useQuery({
    queryKey: ['songsDashboard', token],
    queryFn: getSongs,
    enabled: !!token,
  });
};

export default useGetCurrentUserSongs;
