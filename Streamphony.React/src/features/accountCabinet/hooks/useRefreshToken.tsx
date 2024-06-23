import axios from 'axios';
import { API_URL, REFRESH_TOKEN_ENDPOINT } from '../../../shared/constants';
import { useMutation, useQuery } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useRefreshToken = () => {
  const { getToken, tokenRefresh } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const refreshToken = async () => {
    try {
      const res = await axios.get(
        `${API_URL}/${REFRESH_TOKEN_ENDPOINT}`,
        config,
      );

      const response = res.data;
      const token = response.accessToken;
      tokenRefresh(token);

      return;
    } catch (err: any) {
      console.log(err);
      if (axios.isAxiosError(err)) {
        throw new Error(err.message);
      } else {
        throw new Error(err.response.data.errors);
      }
    }
  };

  return useMutation({
    mutationFn: refreshToken,
  });
};

export default useRefreshToken;
