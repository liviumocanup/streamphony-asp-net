import { Box } from '@mui/material';
import ProfileHeaderWrapper from './ProfileHeaderWrapper';
import ProfileBody from './ProfileBody';
import { SongDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';
import SongHeader from './SongHeader';

interface Props {
  song: SongDetails;
}

const SongViewDetails = ({ song }: Props) => {
  return (
    <Box sx={{ ml: 8, mt: 6 }}>
      <ProfileHeaderWrapper
        type={'Song'}
        name={song.title}
        coverUrl={song.coverUrl}
        children={<SongHeader song={song} artist={song.artist} />}
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

export default SongViewDetails;
