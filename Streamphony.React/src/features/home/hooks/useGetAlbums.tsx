import { useSuspenseQuery } from '@tanstack/react-query';
import axios from 'axios';
import { backendUrl } from '../../../shared/constants';
import { useMemo } from 'react';
import useTokenStorage from '../../../hooks/localStorage/useTokenStorage';

const endpoint = 'albums';

const useGetAlbums = () => {
  const { getToken } = useTokenStorage();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getAlbums = async () => {
    try {
      // await new Promise(resolve => setTimeout(resolve, 5000));
      const res = await axios.get(`${backendUrl}/${endpoint}`, config);
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
