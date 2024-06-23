import {
  VolumeUpRounded as VolumeUpIcon,
  VolumeOffRounded as VolumeOffIcon,
  QueueMusicRounded as QueueIcon,
} from '@mui/icons-material';
import AudioSlider from './AudioSlider';
import { Box, IconButton } from '@mui/material';
import { useState } from 'react';
import TemporaryDrawer from '../../drawer/TemporaryDrawer';

interface VolumeControlProps {
  volume: number;
  setVolume: (position: number) => void;
}

const VolumeControl = ({ volume, setVolume }: VolumeControlProps) => {
  const [open, setOpen] = useState(false);

  const handleDrawerOpen = () => {
    setOpen(true);
  };

  const handleDrawerClose = () => {
    setOpen(false);
  };

  return (
    <Box
      sx={{
        display: 'flex',
        mr: 3,
        alignItems: 'center',
        justifyContent: 'flex-end',
        width: '20%',
      }}
    >
      <IconButton onClick={handleDrawerOpen}>
        <QueueIcon />
      </IconButton>

      <IconButton sx={{ mr: 0.5 }}>
        <VolumeUpIcon />
      </IconButton>

      <AudioSlider
        position={volume}
        setPosition={setVolume}
        duration={1}
        min={0}
        max={1}
        step={0.01}
        width="25%"
      />

      <TemporaryDrawer open={open} handleDrawerClose={handleDrawerClose} />
    </Box>
  );
};

export default VolumeControl;
