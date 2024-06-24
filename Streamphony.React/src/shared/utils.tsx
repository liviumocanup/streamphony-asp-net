import { Alert, SxProps } from '@mui/material';
import { Theme } from '@mui/material/styles';
import { format, parseISO } from 'date-fns';

export const fallbackTrack = {
  id: '',
  coverUrl: '',
  audioUrl: '',
  title: '',
  artist: '',
  album: '',
  duration: 0,
  stoppedAt: 0,
  autoPlay: false,
  resource: [],
};

export const formatDateTime = (dateTime: string) => {
  const date = parseISO(dateTime);

  return format(date, 'MMMM dd, yyyy');
};

interface formatDurationProps {
  timeSpan: string;
  withLabel?: boolean;
}

export const formatDuration = ({
  timeSpan,
  withLabel = false,
}: formatDurationProps) => {
  const parts = timeSpan.split(':');
  if (parts.length === 3) {
    const hours = parseInt(parts[0], 10);
    const minutes = parseInt(parts[1], 10);
    const seconds = parseInt(parts[2], 10);
    if (withLabel) {
      return `${hours > 0 ? `${hours} h ` : ''}${minutes} min ${seconds} sec`;
    }
    return `${hours > 0 ? `${hours}:` : ''}${minutes}:${seconds.toString().padStart(2, '0')}`;
  }
  return timeSpan;
};

export const durationToSeconds = (duration: string): number => {
  const parts = duration.split(':').map((part) => parseInt(part, 10));

  let seconds = 0;
  if (parts.length === 3) {
    // Format "HH:MM:SS"
    seconds = parts[0] * 3600 + parts[1] * 60 + parts[2];
  } else if (parts.length === 2) {
    // Format "MM:SS"
    seconds = parts[0] * 60 + parts[1];
  } else {
    throw new Error('Invalid time format');
  }

  return seconds;
};

interface displayAlertProps {
  isPending: boolean;
  error: Error | null;
  isBlobNull: boolean;
  sx?: SxProps<Theme>;
}

export const displayAlert = ({
  isPending,
  error,
  isBlobNull,
  sx = { bgcolor: 'background.paper' },
}: displayAlertProps) => {
  if (error) {
    return (
      <Alert severity={'error'} sx={{ ...sx, p: 0, borderRadius: '5px' }}>
        {error.message}
      </Alert>
    );
  }

  if (isPending) {
    return (
      <Alert severity={'info'} sx={{ ...sx, p: 0 }}>
        Uploading...
      </Alert>
    );
  }

  if (isBlobNull) {
    return (
      <Alert severity={'info'} sx={{ ...sx, p: 0 }}>
        Upload a file
      </Alert>
    );
  }

  return <Alert severity={'success'} sx={{ ...sx, p: 0 }} />;
};
