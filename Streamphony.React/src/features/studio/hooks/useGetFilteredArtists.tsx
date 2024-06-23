import { useMutation } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, ARTIST_FILTERED_ENDPOINT } from '../../../shared/constants';

const pageNumber = 1;
const pageSize = 10;

const useGetFilteredArtists = () => {
  const getArtists = async (search: string) => {
    const pagedRequest = {
      PageNumber: pageNumber,
      PageSize: pageSize,
      ColumnNameForSorting: 'StageName',
      SortDirection: 'ASC',
      RequestFilters: {
        LogicalOperator: 0,
        Filters: [
          {
            Path: 'StageName',
            Value: search,
          },
        ],
      },
    };

    try {
      const res = await axios.post(
        `${API_URL}/${ARTIST_FILTERED_ENDPOINT}`,
        pagedRequest,
      );

      return res.data.items;
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
    mutationFn: getArtists,
  });
};

export default useGetFilteredArtists;
