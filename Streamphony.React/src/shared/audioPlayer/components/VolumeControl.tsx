import { VolumeUpRounded as VolumeUpIcon } from '@mui/icons-material';
import AudioSlider from './AudioSlider';
import { Box } from '@mui/material';

interface VolumeControlProps {
  volume: number;
  setVolume: (position: number) => void;
}

const VolumeControl = ({ volume, setVolume }: VolumeControlProps) => {
  return (
    <Box
      sx={{
        display: 'flex',
        mr: 1,
        width: '8vw',
        alignItems: 'center',
        justifyContent: 'space-between',
      }}
    >
      <VolumeUpIcon sx={{ mr: 1 }} />
      <AudioSlider
        position={volume}
        setPosition={setVolume}
        duration={1}
        min={0}
        max={1}
        step={0.01}
      />
    </Box>
  );
};

export default VolumeControl;
