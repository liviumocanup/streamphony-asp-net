import { IconButton } from '@mui/material';
import { PlayArrow } from '@mui/icons-material';
import { useMemo } from 'react';

interface PlayButtonCellProps {
  index: number;
  isHovered: boolean;
}

const PlayButtonCell = ({ index, isHovered }: PlayButtonCellProps) => {
  return useMemo(
    () =>
      isHovered ? (
        <IconButton sx={{ m: 0, p: 0 }}>
          <PlayArrow sx={{ color: 'text.primary', fontSize: 20 }} />
        </IconButton>
      ) : (
        index + 1
      ),
    [index, isHovered],
  );
};

export default PlayButtonCell;
