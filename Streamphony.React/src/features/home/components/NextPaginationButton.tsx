import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import { IconButton } from '@mui/material';
import { Dispatch, SetStateAction } from 'react';

interface NextPaginationButtonProps {
  totalRecords: number;
  pageSize: number;
  pageNumber: number;
  setPageNumber: Dispatch<SetStateAction<number>>;
}

const NextPaginationButton = ({
  totalRecords,
  pageSize,
  pageNumber,
  setPageNumber,
}: NextPaginationButtonProps) => {
  return (
    <IconButton
      aria-label="next-page"
      onClick={() => setPageNumber((page) => page + 1)}
      sx={{
        position: 'absolute',
        right: 0,
        top: 88,
        zIndex: 1,
        mr: 2,
        bgcolor: 'background.paper',
        display: pageNumber * pageSize >= totalRecords ? 'none' : 'inherit',
      }}
    >
      <NavigateNextIcon />
    </IconButton>
  );
};

export default NextPaginationButton;
