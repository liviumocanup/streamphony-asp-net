import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import { IconButton } from '@mui/material';
import { Dispatch, SetStateAction } from 'react';

interface BackPaginationButtonProps {
  pageNumber: number;
  setPageNumber: Dispatch<SetStateAction<number>>;
}

const BackPaginationButton = ({
  pageNumber,
  setPageNumber,
}: BackPaginationButtonProps) => {
  return (
    <IconButton
      aria-label="previous-page"
      onClick={() => setPageNumber((page) => page - 1)}
      sx={{
        position: 'absolute',
        left: 0,
        top: 88,
        zIndex: 1,
        bgcolor: 'background.paper',
        display: pageNumber === 1 ? 'none' : 'inherit',
      }}
    >
      <NavigateBeforeIcon />
    </IconButton>
  );
};

export default BackPaginationButton;
