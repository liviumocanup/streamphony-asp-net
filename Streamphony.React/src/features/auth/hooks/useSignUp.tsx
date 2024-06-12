import { SignUpData } from '../../../shared/Interfaces';
import axios from 'axios';
import { API_URL, SIGN_UP_ENDPOINT } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import useTokenStorage from '../../../hooks/localStorage/useTokenStorage';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useSignUp = () => {
  const { setToken } = useTokenStorage();
  const { tokenRefresh } = useAuthContext();

  const addUser = async (newUser: SignUpData) => {
    try {
      const res = await axios.post(`${API_URL}/${SIGN_UP_ENDPOINT}`, newUser);
      setToken(res.data.accessToken);
      tokenRefresh();
      return res.data;
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
  });
};

export default useSignUp;
