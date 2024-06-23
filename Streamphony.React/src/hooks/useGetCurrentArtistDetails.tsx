import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { useMemo } from 'react';
import { API_URL, CURRENT_USER_ARTIST_ENDPOINT } from '../shared/constants';
import useAuthContext from './context/useAuthContext';

const useGetCurrentArtistDetails = () => {
  const { isArtist, getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getArtist = async () => {
    try {
      const res = await axios.get(
        `${API_URL}/${CURRENT_USER_ARTIST_ENDPOINT}`,
        config,
      );

      const response = res.data;

      return {
        id: response.id,
        stageName: response.stageName,
        dateOfBirth: response.dateOfBirth,
        pfpUrl: response.profilePictureUrl,
        createdAt: response.createdAt,
        updatedAt: response.updatedAt,
      };
    } catch (err: any) {
      console.error('Failed to fetch current User:', err.response?.data);
      throw new Error(
        err.response?.data?.errors || 'Error fetching current User',
      );
    }
  };

  return useQuery({
    queryKey: ['artist', token],
    queryFn: getArtist,
    enabled: !!token && isArtist,
  });
};

export default useGetCurrentArtistDetails;
