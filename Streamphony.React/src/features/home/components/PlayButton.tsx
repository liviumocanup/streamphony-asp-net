import { IconButton } from '@mui/material';
import PlayArrowIcon from '@mui/icons-material/PlayArrow';
import PauseIcon from '@mui/icons-material/Pause';

interface PlayButtonProps {
  isVisible: boolean;
  isPlaying: boolean;
  onClick: () => void;
}

const PlayButton = ({ isVisible, isPlaying, onClick }: PlayButtonProps) => {
  return (
    <IconButton
      aria-label={isPlaying ? 'pause' : 'play'}
      onClick={onClick}
      sx={{
        width: 45,
        height: 45,
        position: 'absolute',
        bottom: 5,
        right: 5,
        opacity: isVisible ? 1 : 0,
        transform: isVisible ? 'translateY(0)' : 'translateY(20px)',
        bgcolor: '#1DB954',
        boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.3)',
        transition: 'opacity 0.3s ease, transform 0.3s ease',
        '&:hover': {
          bgcolor: '#1DB954',
        },
      }}
    >
      {isPlaying ? (
        <PauseIcon sx={{ color: 'background.default' }} />
      ) : (
        <PlayArrowIcon sx={{ color: 'background.default' }} />
      )}
    </IconButton>
  );
};

export default PlayButton;
