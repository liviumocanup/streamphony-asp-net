import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, CREATE_SONG_ENDPOINT } from '../../../shared/constants';
import { useMemo } from 'react';
import useAuthContext from '../../../hooks/context/useAuthContext';
import { formatDuration } from '../../../shared/utils';

const endpoint = `${CREATE_SONG_ENDPOINT}/current`;

interface Song {
  id: string;
  title: string;
  duration: string;
  ownerId: string;
  albumId: string;
  genreId: string;
  coverUrl: string;
}

const useGetCurrentUserSongs = () => {
  const { getToken } = useAuthContext();
  const token = getToken();

  const config = useMemo(
    () => ({
      headers: { Authorization: `Bearer ${token}` },
    }),
    [token],
  );

  const getSongs = async () => {
    try {
      const res = await axios.get(`${API_URL}/${endpoint}`, config);

      const items = res.data;

      console.log(res.data);

      return items.map((item: Song) => ({
        id: item.id,
        title: item.title,
        duration: formatDuration(item.duration),
        coverUrl: item.coverUrl,
        audioUrl: item.audioUrl,
        dateAdded: '-',
        albumId: item.albumId === null ? '-' : item.albumId,
        genreId: item.genreId === null ? '-' : item.genreId,
        ownerId: item.ownerId,
      }));
    } catch (err: any) {
      console.error('Failed to fetch albums:', err.response?.data);
      throw new Error(err.response?.data?.errors || 'Error fetching albums');
    }
  };

  return useQuery({
    queryKey: ['songs', token],
    queryFn: getSongs,
    enabled: !!token,
  });
};

export default useGetCurrentUserSongs;
