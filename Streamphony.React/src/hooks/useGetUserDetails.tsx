import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { useMemo } from 'react';
import { API_URL, USER_ENDPOINT } from '../shared/constants';
import useAuthContext from './context/useAuthContext';

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
      const res = await axios.get(`${API_URL}/${USER_ENDPOINT}`, config);

      const response = res.data;
      return {
        id: response.id,
        email: response.email,
        username: response.username,
        artistId: response.artistId,
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
