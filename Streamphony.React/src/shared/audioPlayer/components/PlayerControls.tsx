import { Box, IconButton } from '@mui/material';
import {
  PauseCircleFilledRounded as PauseIcon,
  PlayCircleFilledRounded as PlayIcon,
  SkipNextRounded as SkipNextIcon,
  SkipPreviousRounded as SkipPreviousIcon,
  LoopRounded as LoopIcon,
} from '@mui/icons-material';
import useAudioPlayerContext from '../../../hooks/context/useAudioPlayerContext';

const PlayerControls = () => {
  const { isPlaying, togglePlay, playNext, playPrevious } =
    useAudioPlayerContext();

  return (
    <Box
      display="flex"
      flexDirection="row"
      alignItems="center"
      sx={{ mt: 1.5 }}
    >
      <IconButton
        aria-label="previous"
        onClick={playPrevious}
        sx={{ m: 0, p: 0, mr: 1 }}
      >
        <SkipPreviousIcon sx={{ height: 30, width: 30 }} />
      </IconButton>

      <IconButton
        aria-label="play/pause"
        onClick={togglePlay}
        sx={{ m: 0, p: 0 }}
      >
        {isPlaying ? (
          <PauseIcon sx={{ height: 38, width: 38 }} />
        ) : (
          <PlayIcon sx={{ height: 38, width: 38 }} />
        )}
      </IconButton>

      <IconButton
        aria-label="next"
        onClick={playNext}
        sx={{ m: 0, p: 0, ml: 1 }}
      >
        <SkipNextIcon sx={{ height: 34, width: 34 }} />
      </IconButton>

      <IconButton>
        <LoopIcon />
      </IconButton>
    </Box>
  );
};

export default PlayerControls;
