import axios from 'axios';
import { API_URL, UPLOAD_BLOB_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';
import { BlobType } from '../../../shared/interfaces/EntitiesInterfaces';

const useUploadBlob = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }),
    [token],
  );

  const uploadBlob = async ({
    file,
    blobType,
  }: {
    file: File;
    blobType: BlobType;
  }) => {
    const cancelTokenSource = axios.CancelToken.source();

    try {
      await new Promise((resolve) => setTimeout(resolve, 3000));

      const formData = new FormData();
      formData.append('file', file);
      formData.append('blobType', blobType);

      const res = await axios.post(
        `${API_URL}/${UPLOAD_BLOB_ENDPOINT}`,
        formData,
        {
          ...config,
          cancelToken: cancelTokenSource.token,
        },
      );

      return {
        id: res.data.id,
        url: res.data.url,
        duration: res.data.duration,
      };
    } catch (err: any) {
      console.log(err);
      if (axios.isAxiosError(err)) {
        throw new Error(
          err.response?.data.message || 'An error occurred during upload',
        );
      } else {
        throw new Error('An unexpected error occurred');
      }
    }
  };

  return useMutation({
    mutationFn: uploadBlob,
  });
};

export default useUploadBlob;
