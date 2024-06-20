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
        mr: 3,
        alignItems: 'center',
        justifyContent: 'flex-end',
        width: '30%',
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
        width="25%"
      />
    </Box>
  );
};

export default VolumeControl;
