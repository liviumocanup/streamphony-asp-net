import { TabPanel } from '../TabPanel';
import { AccessTime as TimeIcon } from '@mui/icons-material';
import DashboardTable from '../DashboardTable';
import useGetCurrentArtistSongs from '../../hooks/useGetCurrentArtistSongs';
import { formatDateTime, formatDuration } from '../../../../shared/utils';
import { useMemo } from 'react';
import { Song } from '../../../../shared/Interfaces';
import PlayButtonCell from './PlayButtonCell';
import TitleCell from './TitleCell';

interface SongsTabPanelProps {
  value: number;
  index: number;
}

const headers = [
  {
    label: '#',
    propertyName: 'id',
    centered: true,
    width: '4%',
    renderCell: (_item: Song, index: number, _isHovered: boolean) => index + 1,
  },
  {
    label: 'Title',
    propertyName: 'title',
    width: '40%',
    renderCell: (item: Song) => (
      <TitleCell itemId={item.id} title={item.title} coverUrl={item.coverUrl} />
    ),
  },
  { label: 'Album', propertyName: 'albumId', width: '25%' },
  { label: 'Date Added', propertyName: 'dateAdded' },
  {
    label: 'Duration',
    propertyName: 'duration',
    centered: true,
    width: '10%',
    icon: <TimeIcon />,
  },
];

const prepareSongData = (songs: Song[]) =>
  songs.map((song: Song) => ({
    ...song,
    duration: formatDuration(song.duration),
    dateAdded: formatDateTime(song.createdAt),
    albumId: song.albumId || '-',
    coverUrl: song.coverUrl || '',
  }));

const SongsTabPanel = ({ value, index }: SongsTabPanelProps) => {
  const { data: songs, isPending, isError } = useGetCurrentArtistSongs();

  const items = useMemo(() => (songs ? prepareSongData(songs) : []), [songs]);

  if (isPending) return <div>Loading...</div>;

  if (isError) return <div>Error fetching songs</div>;

  return (
    <TabPanel value={value} index={index}>
      <DashboardTable headers={headers} items={items} />
    </TabPanel>
  );
};

export default SongsTabPanel;
