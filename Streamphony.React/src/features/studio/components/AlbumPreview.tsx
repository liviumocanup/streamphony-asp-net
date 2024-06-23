import {
  Avatar,
  Box,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Typography,
} from '@mui/material';
import { Song } from '../../../shared/interfaces/Interfaces';
import { formatDuration } from '../../../shared/utils';

interface AlbumPreviewProps {
  coverUrl: string | undefined;
  artistName: string;
  title: string;
  songs: Song[];
}

const AlbumPreview = ({
  coverUrl,
  artistName,
  title,
  songs,
}: AlbumPreviewProps) => {
  return (
    <Box
      sx={{
        width: '100%',
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        // bgcolor: 'red',
        borderRadius: 2,
      }}
    >
      <Avatar
        src={coverUrl}
        alt="Album Cover"
        style={{
          width: '185px',
          height: '185px',
          borderRadius: '4px',
          boxShadow: '0 0 10px 5px rgba(0, 0, 0, 0.1)',
        }}
      />
      <Typography variant="h5" fontWeight={'bold'} sx={{ mt: 3 }}>
        {title}
      </Typography>
      <Typography
        variant="subtitle2"
        sx={{ mt: 1, color: 'text.disabled', fontSize: 13, mb: 2 }}
      >
        ALBUM BY {artistName.toUpperCase()}
      </Typography>

      <List
        sx={{ width: '100%', display: 'flex', flexDirection: 'column', mr: 2 }}
      >
        {songs.map((song, index) => (
          <ListItem key={index} sx={{ py: 0.5 }}>
            <ListItemText
              primary={`${index + 1}.`}
              sx={{
                flex: '0 0 auto',
                color: 'text.disabled',
                fontSize: '0.75rem',
                mr: 2,
              }}
            />
            <ListItemAvatar>
              <Avatar
                src={song.coverUrl}
                alt={song.title}
                sx={{ width: 45, height: 45 }}
                variant={'rounded'}
              />
            </ListItemAvatar>
            <ListItemText
              primary={song.title}
              primaryTypographyProps={{
                noWrap: true,
                sx: {
                  fontSize: '0.875rem',
                  fontWeight: 'medium',
                  textAlign: 'left',
                },
              }}
              sx={{ flexGrow: 1, maxWidth: '60%', mr: 1 }}
            />
            <ListItemText
              primary={formatDuration({ timeSpan: song.duration })}
              sx={{
                color: 'text.disabled',
                fontSize: '0.75rem',
                textAlign: 'right',
              }}
            />
          </ListItem>
        ))}
      </List>
    </Box>
  );
};

export default AlbumPreview;
