import { useSuspenseQuery } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL } from '../../../shared/constants';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const endpoint = 'albums';

const useGetAlbums = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getAlbums = async () => {
    try {
      const res = await axios.get(`${API_URL}/${endpoint}`, config);
      console.log(res.data);
      return res.data.items;
    } catch (err: any) {
      console.error('Failed to fetch albums:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching albums');
    }
  };

  return useSuspenseQuery({
    queryKey: ['albums', token],
    queryFn: getAlbums,
  });
};

export default useGetAlbums;
