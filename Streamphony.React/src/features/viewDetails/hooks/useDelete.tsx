import axios from 'axios';
import { API_URL, SONG_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';

interface Props {
  endpoint: string;
  entityId: string;
}

const useDelete = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const deleteEntity = async ({ endpoint, entityId }: Props) => {
    try {
      const res = await axios.delete(
        `${API_URL}/${endpoint}/${entityId}`,
        config,
      );

      console.log('Response: ', res.data);

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
    mutationFn: deleteEntity,
  });
};

export default useDelete;
