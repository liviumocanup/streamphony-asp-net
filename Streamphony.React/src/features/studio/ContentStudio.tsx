import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import Box from '@mui/material/Box';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';
import { Toolbar, Typography } from '@mui/material';
import DashboardTabs from './components/DashboardTabs';

const ContentStudio = () => {
  return (
    <>
      <Helmet>
        <title>Artist Content - {APP_TITLE}</title>
        <meta name="description" content="Your content studio" />
      </Helmet>

      <AppBarWrapper
        showCreate={true}
        children={
          <Box sx={{ flexGrow: 1, mt: 8 }} className="CenteredContainer">
            <Toolbar />

            <Box sx={{ width: '100%', ml: 11, mb: 3 }}>
              <Typography variant="h5" fontWeight={'bold'}>
                Profile content
              </Typography>
            </Box>

            <DashboardTabs />
          </Box>
        }
      />
    </>
  );
};

export default ContentStudio;
