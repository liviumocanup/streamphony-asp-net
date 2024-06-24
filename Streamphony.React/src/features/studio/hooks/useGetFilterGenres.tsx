import { useMutation } from '@tanstack/react-query';
import axios from 'axios';
import { API_URL, GENRE_FILTERED_ENDPOINT } from '../../../shared/constants';

const pageNumber = 1;
const pageSize = 10;

const useGetFilteredGenres = () => {
  const getGenres = async (search: string) => {
    const pagedRequest = {
      PageNumber: pageNumber,
      PageSize: pageSize,
      ColumnNameForSorting: 'Name',
      SortDirection: 'ASC',
      RequestFilters: {
        LogicalOperator: 0,
        Filters: [
          {
            Path: 'Name',
            Value: search,
          },
        ],
      },
    };

    try {
      const res = await axios.post(
        `${API_URL}/${GENRE_FILTERED_ENDPOINT}`,
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
    mutationFn: getGenres,
  });
};

export default useGetFilteredGenres;
