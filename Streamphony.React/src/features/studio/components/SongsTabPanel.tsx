import { TabPanel } from './TabPanel';
import { AccessTime as TimeIcon, PlayArrow } from '@mui/icons-material';
import ContentTable from './ContentTable';
import useGetCurrentUserSongs from '../hooks/useGetSongs';
import { Avatar, Box } from '@mui/material';

interface SongsTabPanelProps {
  value: number;
  index: number;
}

// Example song data
const headers = [
  {
    label: '#',
    propertyName: 'id',
    centered: true,
    width: '4%',
    function: (item, index, isHovered) =>
      isHovered ? (
        <PlayArrow sx={{ color: 'text.primary', fontSize: 20 }} />
      ) : (
        index + 1
      ),
  },
  {
    label: 'Title',
    propertyName: 'title',
    width: '40%',
    function: (item, _index, _isHovered) => (
      <Box sx={{ display: 'flex', alignItems: 'center' }}>
        <Avatar
          src={item.coverUrl || ''}
          sx={{ width: 48, height: 48, marginRight: 2 }}
          variant="rounded"
          alt={item.title}
        />
        {item.title}
      </Box>
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

const SongsTabPanel = ({ value, index }: SongsTabPanelProps) => {
  const { data: songs, isPending, isError } = useGetCurrentUserSongs();
  const items = songs;
  console.log('Items: ', items);
  console.log('Songs: ', songs);

  if (isPending) return <div>Loading...</div>;

  if (isError) return <div>Error fetching songs</div>;

  return (
    <TabPanel value={value} index={index}>
      <ContentTable headers={headers} items={items} />
    </TabPanel>
  );
};

export default SongsTabPanel;
