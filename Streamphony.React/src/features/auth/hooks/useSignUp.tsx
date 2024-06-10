import { SignUpData } from '../../../shared/Interfaces';
import axios from 'axios';
import { backendUrl, signUpEndpoint } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import useTokenStorage from '../../../hooks/localStorage/useTokenStorage';

const useSignUp = () => {
  const { setToken } = useTokenStorage();

  const addUser = async (newUser: SignUpData) => {
    try {
      const res = await axios.post(`${backendUrl}/${signUpEndpoint}`, newUser);
      setToken(res.data.accessToken);
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
