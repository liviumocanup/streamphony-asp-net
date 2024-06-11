import { LogInData } from '../../../shared/Interfaces';
import axios from 'axios';
import { backendUrl, logInEndpoint } from '../../../shared/constants';
import { useMutation } from '@tanstack/react-query';
import useTokenStorage from '../../../hooks/localStorage/useTokenStorage';
import useAuthContext from '../../../hooks/context/useAuthContext';

const useLogIn = () => {
  const { setToken } = useTokenStorage();
  const { tokenRefresh } = useAuthContext();

  const verifyCredentials = async (user: LogInData) => {
    try {
      const res = await axios.post(`${backendUrl}/${logInEndpoint}`, user);
      setToken(res.data.accessToken);
      tokenRefresh();
      return res.data;
    } catch (err: any) {
      if (axios.isAxiosError(err)) {
        throw new Error(err.response?.data.errors[0]);
      } else {
        console.log(err);
        throw new Error(err.response.data.errors);
      }
    }
  };

  return useMutation({
    mutationFn: verifyCredentials,
  });
};

export default useLogIn;
