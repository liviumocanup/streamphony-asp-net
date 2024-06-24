import { SignUpData } from '../../../shared/interfaces/Interfaces';
import axios from 'axios';
import { API_URL, SIGN_UP_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useSignUp = () => {
  const { tokenRefresh } = useAuthContext();

  const addUser = async (newUser: SignUpData) => {
    try {
      const res = await axios.post(`${API_URL}/${SIGN_UP_ENDPOINT}`, newUser);

      const token = res.data.accessToken;
      tokenRefresh(token);
    } catch (err: any) {
      if (axios.isAxiosError(err)) {
        throw new Error(err.message);
      } else {
        console.log(err);
        throw new Error(err.response.data.errors);
      }
    }
  };

  return useMutation({
    mutationFn: addUser,
    retry: false,
  });
};

export default useSignUp;
