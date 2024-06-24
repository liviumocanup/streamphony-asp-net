import { Box } from '@mui/material';
import ProfileHeaderWrapper from './ProfileHeaderWrapper';
// import ProfileBody from './ProfileBody';
import { ArtistDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';
import { Song } from '../../../shared/interfaces/EntitiesInterfaces';

interface Props {
  artist: ArtistDetails;
}

const ArtistViewDetails = ({ artist }: Props) => {
  const artistSongs = artist.uploadedSongs;
  const artistAlbums = artist.ownedAlbums;

  const songs = artistSongs.map((song: Song) => ({
    ...song,
  }));

  return (
    <Box sx={{ ml: 8, mt: 6 }}>
      <ProfileHeaderWrapper
        type={'Artist'}
        name={artist.stageName}
        coverUrl={artist.profilePictureUrl}
        aritstCoverUrl={''}
        artistName={''}
        artistId={artist.id}
        releaseDate={''}
        totalDuration={''}
        albumName={''}
        numberOfSongs={0}
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

      {/*<ProfileBody songs={songs} />*/}
    </Box>
  );
};

export default ArtistViewDetails;
