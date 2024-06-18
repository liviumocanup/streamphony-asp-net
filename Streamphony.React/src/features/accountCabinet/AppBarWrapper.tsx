import { Box } from '@mui/material';
import '../../App.css';
import { useState } from 'react';
import AppBar from '../../shared/appBar/AppBar';
import TemporaryDrawer from './components/TemporaryDrawer';

interface AppBarWrapperProps {
  showCreate?: boolean;
}

const AppBarWrapper = ({ showCreate = false }: AppBarWrapperProps) => {
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
        hideToggleOnOpen={false}
        showCreate={showCreate}
      />

      <TemporaryDrawer open={open} handleDrawerClose={handleDrawerClose} />
    </Box>
  );
};

export default AppBarWrapper;
