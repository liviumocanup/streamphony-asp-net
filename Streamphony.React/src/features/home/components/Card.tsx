import { Avatar, Box, Grid, Typography } from '@mui/material';
import '../Home.css';
import PlayButton from './PlayButton';
import { useEffect, useState } from 'react';
import useAudioPlayerContext from '../../../hooks/context/useAudioPlayerContext';
import { durationToSeconds } from '../../../shared/utils';

interface CardProps {
  index: number;
  image: string;
  title: string;
  description: string;
  resource: any;
  imageAlt: string;
  imageVariant?: 'rounded' | 'circular' | 'square';
}

const Card = ({
  index,
  image,
  title,
  description,
  resource,
  imageAlt,
  imageVariant = 'rounded',
}: CardProps) => {
  const [isHovered, setIsHovered] = useState(false);
  const { isPlaying, togglePlay, currentTrack, setTrack } =
    useAudioPlayerContext();
  const isCurrentTrack = currentTrack.title === title;

  const handlePlayPauseClick = () => {
    const song = resource.resource;

    console.log('song', song);

    if (isCurrentTrack) {
      console.log('isCurrentTrack ', isCurrentTrack);
      togglePlay();
    } else {
      console.log('settingTrack ');
      setTrack({
        coverUrl: song.coverUrl,
        audioUrl: song.audioUrl,
        title: song.title,
        artist: song.artistId,
        duration: durationToSeconds(song.duration),
        stoppedAt: 0,
        autoPlay: true,
      });
    }
  };

  useEffect(() => {
    console.log('isCurrentTrack', isCurrentTrack);

    if (isCurrentTrack) {
      setIsHovered(true);
    }
  }, [isCurrentTrack]);

  return (
    <Grid item key={index}>
      <Box
        className="Card"
        onMouseEnter={() => setIsHovered(true)}
        onMouseLeave={() => {
          if (!isPlaying || !isCurrentTrack) setIsHovered(false);
        }}
        sx={{
          color: 'text.primary',
          position: 'relative',
          width: '225px',
          '&:hover': {
            cursor: 'pointer',
            bgcolor: 'action.hover',
            transition: 'all ease 0.3s',
          },
        }}
      >
        <Box
          sx={{ position: 'relative', width: 'fit-content', margin: 'auto' }}
        >
          <Avatar
            src={image}
            alt={imageAlt}
            variant={imageVariant}
            sx={{
              bgcolor: 'primary.main',
              width: 185,
              height: 185,
              alignSelf: 'center',
            }}
          />

          <PlayButton
            isVisible={isHovered}
            isPlaying={isPlaying && isCurrentTrack}
            onClick={handlePlayPauseClick}
          />
        </Box>

        <Typography variant="subtitle1" align="left" noWrap sx={{ mt: 1 }}>
          {title}
        </Typography>
        <Typography variant="body2" color="text.secondary" align="left" noWrap>
          {description}
        </Typography>
      </Box>
    </Grid>
  );
};

export default Card;
