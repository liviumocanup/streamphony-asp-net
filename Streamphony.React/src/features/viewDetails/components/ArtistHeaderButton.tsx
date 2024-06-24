import { Avatar, Typography } from '@mui/material';
import { Artist } from '../../../shared/interfaces/EntitiesInterfaces';
import { useNavigate } from 'react-router-dom';
import { useCallback } from 'react';
import { ItemType } from '../../../shared/interfaces/Interfaces';

const ArtistHeaderButton = ({ artist }: { artist: Artist }) => {
  const navigate = useNavigate();
  const artistId = artist.id;

  const navigateToArtist = useCallback(() => {
    navigate(`/${ItemType.ARTIST}/${artistId}`);
  }, [artistId, navigate]);

  return (
    <>
      <Avatar
        onClick={navigateToArtist}
        src={artist.profilePictureUrl}
        variant={'circular'}
        alt={artist.stageName}
        sx={{ width: 30, height: 30, cursor: 'pointer', mr: 1 }}
      />

      <Typography
        onClick={navigateToArtist}
        variant={'subtitle2'}
        fontWeight={'bold'}
        sx={{
          cursor: 'pointer',
          '&:hover': {
            textDecoration: 'underline',
            color: 'primary.main',
          },
        }}
      >
        {artist.stageName}
      </Typography>
    </>
  );
};

export default ArtistHeaderButton;
