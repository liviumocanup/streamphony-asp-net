import { IconButton } from '@mui/material';
import PlayArrowIcon from '@mui/icons-material/PlayArrow';
import PauseIcon from '@mui/icons-material/Pause';
import { APP_GREEN } from '../../constants';

interface PlayButtonProps {
  isVisible: boolean;
  isPlaying: boolean;
  onClick: () => void;
  size?: number;
}

const PlayButton = ({
  isVisible,
  isPlaying,
  onClick,
  size = 45,
}: PlayButtonProps) => {
  return (
    <IconButton
      aria-label={isPlaying ? 'pause' : 'play'}
      onClick={onClick}
      sx={{
        width: size,
        height: size,
        position: 'absolute',
        bottom: 5,
        right: 5,
        opacity: isVisible ? 1 : 0,
        transform: isVisible ? 'translateY(0)' : 'translateY(20px)',
        bgcolor: APP_GREEN,
        boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.3)',
        transition: 'opacity 0.3s ease, transform 0.3s ease',
        '&:hover': {
          bgcolor: APP_GREEN,
          zIndex: 1500,
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
