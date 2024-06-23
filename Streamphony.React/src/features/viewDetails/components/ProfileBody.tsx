import PlayButton from '../../../shared/audioPlayer/components/PlayButton';
import useAudioPlayerContext from '../../../hooks/context/useAudioPlayerContext';
import {
  durationToSeconds,
  formatDateTime,
  formatDuration,
} from '../../../shared/utils';
import { Box } from '@mui/material';
import DashboardTable from '../../studio/components/DashboardTable';
import TitleCell from '../../studio/components/songsDashboard/TitleCell';
import { AccessTime as TimeIcon } from '@mui/icons-material';
import { useMemo } from 'react';
import PlayButtonCell from '../../studio/components/songsDashboard/PlayButtonCell';
import { SongDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';

interface ProfileBodyProps {
  songs: SongDetails[];
}

const headers = [
  {
    label: '#',
    propertyName: 'id',
    centered: true,
    width: '4%',
    renderCell: (item: SongDetails, index: number, isHovered: boolean) => (
      <PlayButtonCell song={item} index={index} isHovered={isHovered} />
    ),
  },
  {
    label: 'Title',
    propertyName: 'title',
    renderCell: (item: SongDetails) => (
      <TitleCell
        songId={item.id}
        artistId={item.artist.id}
        title={item.title}
        artist={item.artist.stageName}
        coverUrl={item.coverUrl}
      />
    ),
  },
  {
    label: 'Duration',
    propertyName: 'duration',
    centered: true,
    width: '5%',
    icon: <TimeIcon />,
  },
];

const prepareTableData = (songs: SongDetails[]) =>
  songs.map((song: SongDetails) => ({
    ...song,
    duration: formatDuration({ timeSpan: song.duration }),
    dateAdded: formatDateTime(song.createdAt),
    artistName: song.artist.stageName || '-',
    albumName: song.album?.title || '-',
    coverUrl: song.coverUrl || '',
  }));

const ProfileBody = ({ songs }: ProfileBodyProps) => {
  const { isPlaying, togglePlay, replacePlaylist } = useAudioPlayerContext();
  const items = useMemo(() => (songs ? prepareTableData(songs) : []), [songs]);

  console.log(items);

  const handleClick = () => {
    if (isPlaying) {
      togglePlay();
      return;
    }

    const tracks = songs.map((song) => ({
      ...song,
      artist: song.artist.stageName,
      album: song.album?.title || '-',
      duration: durationToSeconds(song.duration),
      stoppedAt: 0,
      autoPlay: false,
      resource: songs,
    }));
    replacePlaylist(tracks);
  };

  return (
    <Box sx={{ mr: 6 }}>
      <Box
        sx={{
          mt: 2,
          position: 'relative',
          width: 70,
          height: 100,
          zIndex: 100,
          mb: 2,
        }}
      >
        <PlayButton
          isVisible={true}
          isPlaying={isPlaying}
          onClick={handleClick}
          size={60}
        />
      </Box>
      <DashboardTable headers={headers} items={items} />
    </Box>
  );
};

export default ProfileBody;
