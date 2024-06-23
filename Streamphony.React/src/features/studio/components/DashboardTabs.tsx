import { SyntheticEvent, useState } from 'react';
import { Box, Tab, Tabs } from '@mui/material';
import { a11yProps, TabPanel } from './TabPanel';
import '../ContentStudio.css';
import SongsTabPanel from './songsDashboard/SongsTabPanel';
import AlbumsTabPanel from './albumsDashboard/AlbumsTabPanel';

const DashboardTabs = () => {
  const [value, setValue] = useState(0);

  const handleChange = (_event: SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  return (
    <Box sx={{ width: '100%' }}>
      <Box sx={{ borderBottom: 1, borderColor: 'divider', ml: 3, mr: 6 }}>
        <Tabs value={value} onChange={handleChange} aria-label="content tabs">
          <Tab
            label="Songs"
            {...a11yProps(0)}
            sx={{ textTransform: 'none', fontWeight: 'bold', mr: 2 }}
          />
          <Tab
            label="Albums"
            {...a11yProps(1)}
            sx={{ textTransform: 'none', fontWeight: 'bold', mr: 2 }}
          />
          <Tab
            label="Playlists"
            {...a11yProps(2)}
            sx={{ textTransform: 'none', fontWeight: 'bold', mr: 2 }}
          />
        </Tabs>
      </Box>

      <SongsTabPanel value={value} index={0} />

      <AlbumsTabPanel value={value} index={1} />

      <TabPanel value={value} index={2}>
        Playlists
      </TabPanel>
    </Box>
  );
};

export default DashboardTabs;
