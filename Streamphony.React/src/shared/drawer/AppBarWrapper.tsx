import { Box } from '@mui/material';
import '../../App.css';
import { ReactNode, useState } from 'react';
import AppBar from '../appBar/AppBar';
import PersistentDrawer from './PersistentDrawer';
import { DrawerHeader } from './styles/DrawerHeaderStyle';
import { Main } from './styles/MainStyle';
import AudioPlayer from '../audioPlayer/AudioPlayer';

interface AppBarWrapperProps {
  children: ReactNode;
  showCreate?: boolean;
  showPlayer?: boolean;
}

const drawerWidth = 240;

const AppBarWrapper = ({
  children,
  showCreate = false,
  showPlayer = false,
}: AppBarWrapperProps) => {
  const [open, setOpen] = useState(false);

  const handleDrawerOpen = () => {
    setOpen(true);
  };

  const handleDrawerClose = () => {
    setOpen(false);
  };

  return (
    <Box
      sx={{
        display: 'flex',
        bgcolor: 'background.default',
      }}
    >
      <AppBar
        open={open}
        handleDrawerOpen={handleDrawerOpen}
        showCreate={showCreate}
        drawerWidth={drawerWidth}
      />

      <PersistentDrawer
        open={open}
        handleDrawerClose={handleDrawerClose}
        drawerWidth={drawerWidth}
      />

      <Main open={open} drawerWidth={drawerWidth}>
        <DrawerHeader />
        {children}
        {showPlayer && (
          <AudioPlayer isDrawerOpen={open} drawerWidth={drawerWidth} />
        )}
      </Main>
    </Box>
  );
};

export default AppBarWrapper;
