import { Avatar, Box, Typography } from '@mui/material';
import { useMemo } from 'react';

interface TitleCellProps {
  itemId: string;
  title: string;
  coverUrl: string;
}

const TitleCell = ({ itemId, title, coverUrl }: TitleCellProps) => {
  const handleClick = () => {
    console.log('Clicked item ID: ', itemId);
  };

  return useMemo(
    () => (
      <Box sx={{ display: 'flex', alignItems: 'center' }}>
        <Avatar
          onClick={handleClick}
          src={coverUrl || ''}
          sx={{
            width: 48,
            height: 48,
            marginRight: 2,
            bgcolor: 'background.paper',
            mr: 1.5,
            cursor: 'pointer',
          }}
          variant="rounded"
          alt={`${title} cover`}
        />
        <Typography
          onClick={handleClick}
          sx={{
            cursor: 'pointer',
            '&:hover': {
              textDecoration: 'underline',
              color: 'primary.main',
            },
          }}
        >
          {title}
        </Typography>
      </Box>
    ),
    [coverUrl, title],
  );
};

export default TitleCell;
