import { SongDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';
import { Artist } from '../../../shared/interfaces/EntitiesInterfaces';
import { Box, Typography } from '@mui/material';
import CircleRoundedIcon from '@mui/icons-material/CircleRounded';
import { formatDateTime, formatDuration } from '../../../shared/utils';
import ArtistHeaderButton from './ArtistHeaderButton';

interface Props {
  song: SongDetails;
  artist: Artist;
}

const SongHeader = ({ song, artist }: Props) => {
  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'flex-start',
      }}
    >
      <ArtistHeaderButton artist={artist} />

      <CircleRoundedIcon sx={{ width: 5, height: 5, ml: 1, mr: 1 }} />

      <Typography variant={'subtitle2'}>
        {formatDateTime(song.createdAt)}
      </Typography>

      <CircleRoundedIcon sx={{ width: 5, height: 5, ml: 1, mr: 1 }} />

      <Typography variant={'subtitle2'}>
        {song.album?.title ? song.album.title : 'Single'}
      </Typography>
      <CircleRoundedIcon sx={{ width: 5, height: 5, ml: 1, mr: 1 }} />

      <Typography variant={'subtitle2'}>
        {formatDuration({
          timeSpan: song.duration,
          withLabel: true,
        })}
      </Typography>

      {song.genre && (
        <>
          <CircleRoundedIcon sx={{ width: 5, height: 5, ml: 1, mr: 1 }} />
          <Typography variant={'subtitle2'}>{song.genre.name}</Typography>
        </>
      )}
    </Box>
  );
};

export default SongHeader;
