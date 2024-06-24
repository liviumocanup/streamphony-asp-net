import { Box, useTheme } from '@mui/material';
import { useEffect, useRef, useState } from 'react';
import PlayerControls from './components/PlayerControls';
import TrackDetails from './components/TrackDetails';
import ProgressBar from './components/ProgressBar';
import VolumeControl from './components/VolumeControl';
import useAudioPlayerContext from '../../hooks/context/useAudioPlayerContext';

interface AudioPlayerProps {
  isDrawerOpen?: boolean;
  drawerWidth?: number;
}

const AudioPlayer = ({
  isDrawerOpen = false,
  drawerWidth = 0,
}: AudioPlayerProps) => {
  const theme = useTheme();
  const { currentTrack, isPlaying, togglePlay, updateStoppedAt, playNext } =
    useAudioPlayerContext();
  const audioRef = useRef<HTMLAudioElement>(null);
  const [position, setPosition] = useState(currentTrack.stoppedAt);
  const [volume, setVolume] = useState(0.5);

  const handlePositionChange = (newPosition: number) => {
    const audio = audioRef.current;
    if (audio) {
      audio.currentTime = newPosition;
      setPosition(newPosition);
    }
  };

  const handleVolumeChange = (newVolume: number) => {
    const audio = audioRef.current;
    if (audio) {
      audio.volume = newVolume;
      setVolume(newVolume);
    }
  };

  useEffect(() => {
    const audio = audioRef.current;
    if (!audio) return;

    const updateTime = () => {
      setPosition(audio.currentTime);
      updateStoppedAt(audio.currentTime);
    };

    const handleEnded = () => {
      playNext();
    };

    audio.addEventListener('timeupdate', updateTime);
    audio.addEventListener('ended', handleEnded);

    return () => {
      audio.removeEventListener('timeupdate', updateTime);
      audio.removeEventListener('ended', handleEnded);
    };
  }, [playNext, updateStoppedAt]);

  useEffect(() => {
    const audio = audioRef.current;
    if (!audio) return;

    if (audio.src !== currentTrack.audioUrl) {
      audio.src = currentTrack.audioUrl;
      audio.currentTime = currentTrack.stoppedAt;

      if (!isPlaying) togglePlay();
    }

    if (isPlaying) {
      audio.play().catch((error) => {
        console.error('Error attempting to play audio:', error);
      });
    } else {
      audio.pause();
    }
  }, [currentTrack, isPlaying, togglePlay]);

  return (
    <Box
      className="WidthCentered"
      sx={{
        position: 'fixed',
        bottom: 0,
        left: isDrawerOpen ? `${drawerWidth}px` : 0,
        right: 0,
        zIndex: 1000,
        width: isDrawerOpen ? `calc(100% - ${drawerWidth}px)` : '100%',
        boxShadow: '0 -2px 10px rgba(0,0,0,0.3)',
        bgcolor: 'background.paper',
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        transition: theme.transitions.create(['left', 'width'], {
          easing: theme.transitions.easing.easeOut,
          duration: theme.transitions.duration.enteringScreen,
        }),
      }}
    >
      <audio ref={audioRef} preload="auto" />
      <TrackDetails
        title={currentTrack.title}
        artist={currentTrack.artist}
        coverUrl={currentTrack.coverUrl}
      />

      <Box
        display="flex"
        flexDirection="column"
        alignItems="center"
        sx={{ flex: '1', textAlign: 'center', ml: 4, minWidth: '200px' }}
      >
        <PlayerControls />

        <ProgressBar
          position={position}
          setPosition={handlePositionChange}
          duration={currentTrack.duration}
        />
      </Box>

      <VolumeControl volume={volume} setVolume={handleVolumeChange} />
    </Box>
  );
};

export default AudioPlayer;
