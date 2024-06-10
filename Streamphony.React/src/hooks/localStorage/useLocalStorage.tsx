import { useCallback } from 'react';

const useLocalStorage = (key: string) => {
  const getItem = useCallback(() => {
    return localStorage.getItem(key);
  }, [key]);

  const setItem = useCallback(
    (value: string) => {
      localStorage.setItem(key, value);
    },
    [key],
  );

  const removeItem = useCallback(() => {
    localStorage.removeItem(key);
  }, [key]);

  return {
    getItem,
    setItem,
    removeItem,
  };
};

export default useLocalStorage;
