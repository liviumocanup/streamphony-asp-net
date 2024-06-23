import { Avatar, Box, Grid, Typography } from '@mui/material';
import '../Home.css';
import PlayButton from '../../../shared/audioPlayer/components/PlayButton';
import { useEffect, useState } from 'react';
import useAudioPlayerContext from '../../../hooks/context/useAudioPlayerContext';
import { durationToSeconds } from '../../../shared/utils';
import { useNavigate } from 'react-router-dom';

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
  const navigate = useNavigate();
  const [isHovered, setIsHovered] = useState(false);
  const { isPlaying, togglePlay, currentTrack, replacePlaylist } =
    useAudioPlayerContext();

  const isCurrentTrack = currentTrack.title === title;

  const handleClick = () => {
    const id = resource.resource.id;
    const type = resource.resourceType;

    navigate(`/${type}/${id}`);
  };

  const handlePlayPauseClick = () => {
    const song = resource.resource;

    if (isCurrentTrack) {
      togglePlay();
    } else {
      replacePlaylist([
        {
          id: song.id,
          coverUrl: song.coverUrl,
          audioUrl: song.audioUrl,
          title: song.title,
          artist: song.artistName,
          album: song.albumName,
          duration: durationToSeconds(song.duration),
          stoppedAt: 0,
          autoPlay: true,
          resource: song,
        },
      ]);
    }
  };

  useEffect(() => {
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
          width: '100%',
          minWidth: 210,
          zIndex: 0,
          '&:hover': {
            cursor: 'pointer',
            bgcolor: 'action.hover',
            transition: 'all ease 0.3s',
            zIndex: 0,
          },
        }}
      >
        <Box sx={{ position: 'relative', width: 'fit-content' }}>
          <Avatar
            src={image}
            alt={imageAlt}
            variant={imageVariant}
            onClick={handleClick}
            sx={{
              bgcolor: 'primary.main',
              display: 'block',
              // maxWidth: '230px',
              // maxHeight: '230px',
              // width: 'auto',
              // height: 'auto',
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
