import { Box } from '@mui/material';
import { useEffect, useRef, useState } from 'react';
import PlayerControls from './components/PlayerControls';
import TrackDetails from './components/TrackDetails';
import ProgressBar from './components/ProgressBar';
import VolumeControl from './components/VolumeControl';

interface AudioPlayerProps {
  url: string;
  title: string;
  artist: string;
}

const AudioPlayer = ({ url, title, artist }: AudioPlayerProps) => {
  const [isPlaying, setIsPlaying] = useState(false);
  const audioRef = useRef<HTMLAudioElement>(null);
  const [position, setPosition] = useState(0);
  const [volume, setVolume] = useState(0.5);
  const songDuration = 200;

  const togglePlayPause = () => {
    const audio = audioRef.current;
    if (!audio) return;

    if (isPlaying) {
      audio.pause();
    } else {
      audio
        .play()
        .then(() => {
          console.log('Audio playing!');
        })
        .catch((error) => {
          console.error('Error attempting to play audio:', error);
          setIsPlaying(false);
        });
    }
    setIsPlaying(!isPlaying);
  };

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
      setPosition(Math.floor(audio.currentTime));
    };

    audio.addEventListener('timeupdate', updateTime);

    return () => {
      audio.removeEventListener('timeupdate', updateTime);
    };
  }, []);

  return (
    <Box
      className="WidthCentered"
      sx={{
        width: 'inherit',
        bgcolor: 'blue',
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
      }}
    >
      <audio ref={audioRef} src={url} preload="auto" />
      <TrackDetails
        title={title}
        artist={artist}
        coverUrl={
          'https://fastly.picsum.photos/id/446/185/185.jpg?hmac=-LrVBMhrSY98rhvxkY_0BKXggRTux5yvRUcLoBiaQY0'
        }
      />

      <Box display="flex" flexDirection="column" alignItems="center">
        <PlayerControls
          isPlaying={isPlaying}
          togglePlayPause={togglePlayPause}
        />

        <ProgressBar
          position={position}
          setPosition={handlePositionChange}
          duration={songDuration}
        />
      </Box>

      <VolumeControl volume={volume} setVolume={handleVolumeChange} />
    </Box>
  );
};

export default AudioPlayer;
//
// import { useState, useRef, useEffect } from 'react';
// import { IconButton } from '@mui/material';
// import PlayArrowIcon from '@mui/icons-material/PlayArrow';
// import PauseIcon from '@mui/icons-material/Pause';
//
// interface AudioPlayerProps {
//   url: string;
// }

// const AudioPlayer = ({ url }: AudioPlayerProps) => {
//
//   return (
//     <div>
//       <audio ref={audioRef} src={url} preload="auto" />
//       <IconButton onClick={togglePlayPause}>
//         {isPlaying ? <PauseIcon /> : <PlayArrowIcon />}
//       </IconButton>
//     </div>
//   );
// };
//
// export default AudioPlayer;

// <Card sx={{ display: 'flex' }}>
//   <Box sx={{ display: 'flex', flexDirection: 'column' }}>
//     <CardContent sx={{ flex: '1 0 auto' }}>
//       <Typography component="div" variant="h5">
//         {title}
//       </Typography>
//
//       <Typography
//         variant="subtitle1"
//         color="text.secondary"
//         component="div"
//       >
//         {artist}
//       </Typography>
//     </CardContent>
//
//     <Box sx={{ display: 'flex', alignItems: 'center', pl: 1, pb: 1 }}>
//       <IconButton aria-label="previous">
//         <SkipPreviousIcon />
//       </IconButton>
//
//       <audio ref={audioRef} src={url} preload="auto" />
//       <IconButton aria-label="play/pause" onClick={togglePlayPause}>
//         {isPlaying ? (
//           <PauseIcon sx={{ height: 38, width: 38 }} />
//         ) : (
//           <PlayArrowIcon sx={{ height: 38, width: 38 }} />
//         )}
//       </IconButton>
//
//       <IconButton aria-label="next">
//         <SkipNextIcon />
//       </IconButton>
//     </Box>
//   </Box>
//
//   <CardMedia
//     component="img"
//     sx={{ width: 151 }}
//     image="https://fastly.picsum.photos/id/1035/185/185.jpg?hmac=d_bceIJkKGnuQ7InHVppPrscz1MJS0eNHoKBNxnBt3s"
//     alt="Live from space album cover"
//   />
// </Card>
