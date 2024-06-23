import { TabPanel } from '../TabPanel';
import { AccessTime as TimeIcon } from '@mui/icons-material';
import DashboardTable from '../DashboardTable';
import useGetCurrentArtistSongs from '../../hooks/useGetCurrentArtistSongs';
import { formatDateTime, formatDuration } from '../../../../shared/utils';
import { useMemo } from 'react';
import TitleCell from './TitleCell';
import { SongDetails } from '../../../../shared/interfaces/EntityDetailsInterfaces';
import DurationCell from '../DurationCell';
import { ItemType } from '../../../../shared/interfaces/Interfaces';

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
    renderCell: (_item: SongDetails, index: number) => index + 1,
  },
  {
    label: 'Title',
    propertyName: 'title',
    width: '40%',
    renderCell: (item: SongDetails) => (
      <TitleCell
        songId={item.id}
        title={item.title}
        artist={item.artist.stageName}
        artistId={item.artist.id}
        coverUrl={item.coverUrl}
      />
    ),
  },
  { label: 'Album', propertyName: 'albumName', width: '25%' },
  { label: 'Date Added', propertyName: 'dateAdded' },
  {
    label: 'Duration',
    propertyName: 'duration',
    centered: true,
    width: '10%',
    icon: <TimeIcon />,
    renderCell: (item: any, _index: number, isHovered: boolean) => (
      <DurationCell
        duration={item.duration}
        itemId={item.id}
        isHovered={isHovered}
        itemType={ItemType.SONG}
      />
    ),
  },
];

const prepareSongData = (songs: SongDetails[]) =>
  songs.map((song: SongDetails) => ({
    ...song,
    duration: formatDuration({ timeSpan: song.duration }),
    dateAdded: formatDateTime(song.createdAt),
    albumName: song.album?.title || '-',
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
