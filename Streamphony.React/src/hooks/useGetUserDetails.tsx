import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { useMemo } from 'react';
import { API_URL } from '../shared/constants';
import useAuthContext from './context/useAuthContext';
import useArtistContext from './context/useArtistContext';

const endpoint = 'users';

const useGetUserDetails = () => {
  const { getToken } = useAuthContext();
  const token = getToken();
  const { setArtistId } = useArtistContext();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getUser = async () => {
    try {
      const res = await axios.get(`${API_URL}/${endpoint}`, config);

      const response = res.data;
      const user = {
        id: response.id,
        email: response.email,
        username: response.username,
        artistId: response.artistId,
      };

      if (user.artistId) {
        setArtistId(user.artistId);
      }

      return user;
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
