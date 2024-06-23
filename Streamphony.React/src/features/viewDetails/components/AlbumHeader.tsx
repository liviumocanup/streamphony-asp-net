import { Box, Typography } from '@mui/material';
import CircleRoundedIcon from '@mui/icons-material/CircleRounded';
import { formatDateTime, formatDuration } from '../../../shared/utils';
import { AlbumDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';
import { Artist } from '../../../shared/interfaces/EntitiesInterfaces';
import ArtistHeaderButton from './ArtistHeaderButton';

interface Props {
  album: AlbumDetails;
  artist: Artist;
}

const AlbumHeader = ({ album, artist }: Props) => {
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
        {formatDateTime(album.releaseDate)}
      </Typography>

      <CircleRoundedIcon sx={{ width: 5, height: 5, ml: 1, mr: 1 }} />

      <Typography variant={'subtitle2'} sx={{ mr: 1 }}>
        {album.songs.length} songs,
      </Typography>

      <Typography variant={'subtitle2'}>
        {formatDuration({
          timeSpan: album.totalDuration,
          withLabel: true,
        })}
      </Typography>
    </Box>
  );
};

export default AlbumHeader;
