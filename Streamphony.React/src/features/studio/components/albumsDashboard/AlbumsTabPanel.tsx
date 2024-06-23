import { TabPanel } from '../TabPanel';
import { AccessTime as TimeIcon } from '@mui/icons-material';
import DashboardTable from '../DashboardTable';
import { formatDateTime, formatDuration } from '../../../../shared/utils';
import { useMemo } from 'react';
import useGetCurrentArtistAlbums from '../../hooks/useGetCurrentArtistAlbums';
import TitleCell from '../songsDashboard/TitleCell';
import { AlbumDetails } from '../../../../shared/interfaces/EntityDetailsInterfaces';
import MoreOptionsButton from '../MoreOptionsButton';
import DurationCell from '../DurationCell';
import { ItemType } from '../../../../shared/interfaces/Interfaces';

interface AlbumsTabPanelProps {
  value: number;
  index: number;
}

const headers = [
  {
    label: '#',
    propertyName: 'id',
    centered: true,
    width: '4%',
    renderCell: (_item: AlbumDetails, index: number) => index + 1,
  },
  {
    label: 'Title',
    propertyName: 'title',
    width: '40%',
    renderCell: (item: AlbumDetails) => (
      <TitleCell
        songId={item.id}
        title={item.title}
        coverUrl={item.coverUrl}
        artist={item.owner.stageName}
        artistId={item.owner.id}
      />
    ),
  },
  { label: 'Songs', propertyName: 'songNumber', width: '25%' },
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
        itemType={ItemType.ALBUM}
      />
    ),
  },
];

const prepareAlbumData = (albums: AlbumDetails[]) =>
  albums.map((album: AlbumDetails) => ({
    ...album,
    duration: formatDuration({ timeSpan: album.totalDuration }),
    dateAdded: formatDateTime(album.createdAt),
    songNumber: album.songs.length,
    coverUrl: album.coverUrl || '',
  }));

const AlbumsTabPanel = ({ value, index }: AlbumsTabPanelProps) => {
  const { data: albums, isPending, isError } = useGetCurrentArtistAlbums();

  const items = useMemo(
    () => (albums ? prepareAlbumData(albums) : []),
    [albums],
  );

  if (isPending) return <div>Loading...</div>;

  if (isError) return <div>Error fetching albums</div>;

  return (
    <TabPanel value={value} index={index}>
      <DashboardTable headers={headers} items={items} />
    </TabPanel>
  );
};

export default AlbumsTabPanel;
