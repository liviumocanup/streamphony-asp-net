import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { useMemo } from 'react';
import { API_URL } from '../shared/constants';
import useAuthContext from './context/useAuthContext';

const endpoint = 'users';

const useGetUserDetails = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getUser = async () => {
    try {
      const res = await axios.get(`${API_URL}/${endpoint}`, config);

      return {
        id: res.data.id,
        email: res.data.email,
        username: res.data.username,
        artistId: res.data.artistId,
        isArtistLinked: res.data.artistId !== null,
      };
    } catch (err: any) {
      console.error('Failed to fetch current User:', err.response?.data);
      throw new Error(
        err.response?.data?.errors || 'Error fetching current User',
      );
    }
  };

  return useQuery({
    queryKey: ['user', token],
    queryFn: getUser,
    enabled: !!token,
  });
};

export default useGetUserDetails;
