import { Avatar, Box, Typography } from '@mui/material';
import useAudioPlayerContext from '../../hooks/context/useAudioPlayerContext';
import { APP_GREEN } from '../constants';

interface Props {
  label: string;
}

const PlaylistSection = ({ label }: Props) => {
  return (
    <Typography fontWeight={'bold'} sx={{ ml: 2.5, mb: 1, mt: 5 }}>
      {label}
    </Typography>
  );
};

const PlaylistSidebarItems = () => {
  const { playlist, currentTrack } = useAudioPlayerContext();
  const currentTrackIndex = playlist.findIndex(
    (track) => track.id === currentTrack.id,
  );

  const startPreviouslyPlayedIndex = Math.max(0, currentTrackIndex - 3);
  console.log('startPreviouslyPlayedIndex', startPreviouslyPlayedIndex);
  console.log('currentTrackIndex', currentTrackIndex);

  return (
    <Box sx={{ width: '400px' }}>
      <Typography fontWeight={'bold'} sx={{ ml: 2.5, mt: 3 }}>
        Queue
      </Typography>

      {playlist.map((track, index) => (
        <Box
          key={index}
          sx={{
            display: 'flex',
            flexDirection: 'column',
          }}
        >
          {index === currentTrackIndex && (
            <PlaylistSection label={'Now Playing'} />
          )}
          {index === 0 && index < currentTrackIndex && (
            <PlaylistSection label={'Previously Played'} />
          )}
          {index === currentTrackIndex + 1 && (
            <PlaylistSection label={'Next Up'} />
          )}

          {index >= startPreviouslyPlayedIndex && (
            <Box sx={{ display: 'flex', flexDirection: 'row', mb: 2 }}>
              <Avatar
                src={track.coverUrl}
                variant={'rounded'}
                sx={{ width: 55, height: 55, ml: 2.5, mr: 1.5 }}
              />
              <Box sx={{ display: 'flex', flexDirection: 'column' }}>
                <Typography
                  fontWeight={500}
                  sx={{
                    // if the track is the current track, make the text bold
                    color:
                      index === currentTrackIndex ? APP_GREEN : 'text.primary',
                  }}
                >
                  {track.title}
                </Typography>
                <Typography
                  variant={'subtitle2'}
                  sx={{ color: 'text.disabled' }}
                >
                  {track.artist}
                </Typography>
              </Box>
            </Box>
          )}
        </Box>
      ))}
    </Box>
  );
};

export default PlaylistSidebarItems;
