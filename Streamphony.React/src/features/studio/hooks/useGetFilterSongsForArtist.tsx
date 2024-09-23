import { useMutation } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, SONG_FILTERED_ENDPOINT } from '../../../shared/constants';
import useTokenStorage from '../../../hooks/localStorage/useTokenStorage';

const pageNumber = 1;
const pageSize = 10;

const useGetFilteredSongsForArtist = () => {
  const { getTokenClaims } = useTokenStorage();
  const tokenClaims = getTokenClaims();
  const artistId = tokenClaims?.artistId || '';

  const getSongs = async (search: string) => {
    const pagedRequest = {
      PageNumber: pageNumber,
      PageSize: pageSize,
      ColumnNameForSorting: 'Title',
      SortDirection: 'ASC',
      RequestFilters: {
        LogicalOperator: 0,
        Filters: [
          {
            Path: 'Title',
            Value: search,
          },
          {
            Path: 'OwnerId',
            Value: artistId[1],
            ValueType: 1,
          },
          {
            Path: 'AlbumId',
            Value: '',
            ValueType: 1,
          },
        ],
      },
    };

    try {
      console.log('Request: ', pagedRequest);

      const res = await axios.post(
        `${API_URL}/${SONG_FILTERED_ENDPOINT}`,
        pagedRequest,
      );

      return res.data.items;
    } catch (err: any) {
      console.log(err.response.data);
      if (axios.isAxiosError(err)) {
        throw new Error(err.message);
      } else {
        throw new Error(err.response.data.errors);
      }
    }
  };

  return useMutation({
    mutationFn: getSongs,
  });
};

export default useGetFilteredSongsForArtist;
