import { Avatar, Box, Typography } from '@mui/material';

interface TrackDetailsProps {
  title: string;
  artist: string;
  coverUrl: string;
}

const TrackDetails = ({ title, artist, coverUrl }: TrackDetailsProps) => {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'row' }}>
      <Avatar
        variant="rounded"
        src={coverUrl}
        sx={{
          width: '60px',
          height: '60px',
          border: '1px solid white',
          ml: 1,
        }}
      />

      <Box
        sx={{
          ml: 1,
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
        }}
      >
        <Typography variant="body1">{title}</Typography>
        <Typography variant="body2" sx={{ color: 'text.disabled' }}>
          {artist}
        </Typography>
      </Box>
    </Box>
  );
};

export default TrackDetails;
