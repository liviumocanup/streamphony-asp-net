import { useState } from 'react';
import Box from '@mui/material/Box';
import AppBar from '../../shared/appBar/AppBar';
import PersistentDrawer from './components/PersistentDrawer';
import Feed from './components/Feed';
import { Main } from './styles/MainStyle';
import { DrawerHeader } from './styles/DrawerHeaderStyle';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';

const drawerWidth = 240;

const Home = () => {
  const [open, setOpen] = useState(true);

  const handleDrawerOpen = () => {
    setOpen(true);
  };

  const handleDrawerClose = () => {
    setOpen(false);
  };

  return (
    <>
      <Helmet>
        <title>{APP_TITLE}</title>
        <meta
          name="description"
          content="Home page of the music streaming app"
        />
      </Helmet>

      <Box
        sx={{
          display: 'flex',
          bgcolor: 'background.default',
        }}
      >
        <AppBar
          open={open}
          handleDrawerOpen={handleDrawerOpen}
          drawerWidth={drawerWidth}
        />

        <PersistentDrawer
          open={open}
          handleDrawerClose={handleDrawerClose}
          drawerWidth={drawerWidth}
        />

        <Main open={open} drawerWidth={drawerWidth}>
          <DrawerHeader />
          <Feed />
        </Main>
      </Box>
    </>
  );
};

export default Home;
