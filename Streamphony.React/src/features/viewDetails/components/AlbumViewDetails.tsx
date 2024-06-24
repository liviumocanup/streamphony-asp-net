import { Box } from '@mui/material';
import ProfileHeaderWrapper from './ProfileHeaderWrapper';
import ProfileBody from './ProfileBody';
import { AlbumDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';
import { Song } from '../../../shared/interfaces/EntitiesInterfaces';
import AlbumHeader from './AlbumHeader';

interface Props {
  album: AlbumDetails;
}

const AlbumViewDetails = ({ album }: Props) => {
  const albumSongs = album.songs;
  const songs = albumSongs.map((song: Song) => ({
    ...song,
    artist: album.owner,
  }));

  return (
    <Box sx={{ ml: 8, mt: 6 }}>
      <ProfileHeaderWrapper
        type={'Album'}
        name={album.title}
        coverUrl={album.coverUrl}
        children={<AlbumHeader album={album} artist={album.owner} />}
      />

      <Box
        sx={{
          position: 'absolute',
          left: 0,
          top: 0,
          height: '375px',
          width: '100%',
          backgroundImage: 'linear-gradient(to bottom, #1DB954, #121212)',
          zIndex: 1,
        }}
      />

      <ProfileBody songs={songs} />
    </Box>
  );
};

export default AlbumViewDetails;
