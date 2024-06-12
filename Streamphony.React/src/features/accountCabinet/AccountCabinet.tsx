import { Box } from '@mui/material';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import { useState } from 'react';
import AppBar from '../../shared/appBar/AppBar';
import TemporaryDrawer from './components/TemporaryDrawer';
import AccountCabSection from './components/AccountCabSection';
import SecurityCabSection from './components/SecurityCabSection';
import { DrawerHeader } from '../home/styles/DrawerHeaderStyle';

const AccountCabinet = () => {
  const [open, setOpen] = useState(false);

  const handleDrawerOpen = () => {
    setOpen(true);
  };

  const handleDrawerClose = () => {
    setOpen(false);
  };

  return (
    <>
      <Helmet>
        <title>Username - {APP_TITLE}</title>
        <meta name="description" content="Your account settings" />
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
          hideToggleOnOpen={false}
        />

        <TemporaryDrawer open={open} handleDrawerClose={handleDrawerClose} />
      </Box>

      <Box sx={{ flexGrow: 1, mt: 8 }} className="WidthCentered">
        <DrawerHeader />

        <AccountCabSection />

        <SecurityCabSection />
      </Box>
    </>
  );
};

export default AccountCabinet;
