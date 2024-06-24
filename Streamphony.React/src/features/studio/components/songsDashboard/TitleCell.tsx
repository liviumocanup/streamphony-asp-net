import { Avatar, Box, Typography } from '@mui/material';
import { useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { ItemType } from '../../../../shared/interfaces/Interfaces';

interface TitleCellProps {
  songId: string;
  artistId: string;
  title: string;
  coverUrl: string;
  artist: string;
}

const TitleCell = ({
  songId,
  artistId,
  title,
  artist,
  coverUrl,
}: TitleCellProps) => {
  const navigate = useNavigate();

  const navigateToSong = useCallback(() => {
    navigate(`/${ItemType.SONG}/${songId}`);
  }, [navigate, songId]);

  const navigateToArtist = useCallback(() => {
    navigate(`/${ItemType.ARTIST}/${artistId}`);
  }, [artistId, navigate]);

  return (
    <Box sx={{ display: 'flex', alignItems: 'center' }}>
      <Avatar
        onClick={navigateToSong}
        src={coverUrl || ''}
        sx={{
          width: 48,
          height: 48,
          marginRight: 2,
          bgcolor: 'background.paper',
          mr: 1.5,
          cursor: 'pointer',
        }}
        variant="rounded"
        alt={`${title} cover`}
      />
      <Box sx={{ display: 'flex', flexDirection: 'column' }}>
        <Typography
          onClick={navigateToSong}
          sx={{
            cursor: 'pointer',
            '&:hover': {
              textDecoration: 'underline',
              color: 'primary.main',
            },
          }}
        >
          {title}
        </Typography>
        <Typography
          onClick={navigateToArtist}
          variant={'subtitle2'}
          sx={{
            color: 'text.disabled',
            cursor: 'pointer',
            '&:hover': {
              textDecoration: 'underline',
              color: 'primary.main',
            },
          }}
        >
          {artist}
        </Typography>
      </Box>
    </Box>
  );
};

export default TitleCell;
