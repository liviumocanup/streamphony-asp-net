import { Box, Typography } from '@mui/material';
import AudioSlider from './AudioSlider';

interface ProgressBar {
  position: number;
  setPosition: (position: number) => void;
  duration: number;
}

const ProgressBar = ({ position, setPosition, duration }: ProgressBar) => {
  function formatDuration(value: number) {
    const minute = Math.floor(value / 60);
    const secondLeft = value - minute * 60;
    return `${minute}:${secondLeft < 10 ? `0${secondLeft}` : secondLeft}`;
  }

  return (
    <Box
      display="flex"
      flexDirection="row"
      alignItems="center"
      width="30vw"
      sx={{ mb: 1 }}
    >
      <Typography variant="subtitle2" sx={{ color: 'text.secondary', mr: 2 }}>
        {formatDuration(position)}
      </Typography>
      <AudioSlider
        position={position}
        setPosition={setPosition}
        duration={duration}
      />
      <Typography variant="subtitle2" sx={{ color: 'text.secondary', ml: 2 }}>
        {formatDuration(duration)}
      </Typography>
    </Box>
  );
};

export default ProgressBar;
