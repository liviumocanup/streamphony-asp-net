import { useMemo } from 'react';
import { Box, Typography } from '@mui/material';
import MoreOptionsButton from './MoreOptionsButton';
import { ItemType } from '../../../shared/interfaces/Interfaces';

interface DurationCellProps {
  duration: number;
  isHovered: boolean;
  itemId: string;
  itemType: ItemType;
}

const DurationCell = ({
  duration,
  isHovered,
  itemId,
  itemType,
}: DurationCellProps) => {
  const handleClick = () => {
    console.log('More options clicked', itemId);
  };

  return useMemo(
    () => (
      <Box
        sx={{
          position: 'relative',
        }}
      >
        <Typography variant="body2" sx={{ mr: 1 }}>
          {duration}
        </Typography>
        {isHovered && <MoreOptionsButton itemId={itemId} itemType={itemType} />}
      </Box>
    ),
    [duration, handleClick, isHovered],
  );
};

export default DurationCell;
