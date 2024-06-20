import { Box } from '@mui/material';
import '../../App.css';
import { ReactNode, useState } from 'react';
import AppBar from '../../shared/appBar/AppBar';
import PersistentDrawer from '../home/components/PersistentDrawer';
import { DrawerHeader } from '../home/styles/DrawerHeaderStyle';
import { Main } from '../home/styles/MainStyle';

interface AppBarWrapperProps {
  children: ReactNode;
  showCreate?: boolean;
}

const drawerWidth = 240;

const AppBarWrapper = ({
  children,
  showCreate = false,
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
      </Main>
    </Box>
  );
};

export default AppBarWrapper;
