import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import Box from '@mui/material/Box';
import AppBarWrapper from '../accountCabinet/AppBarWrapper';
import { Toolbar, Typography } from '@mui/material';
import ContentTabs from './components/ContentTabs';

const ContentStudio = () => {
  return (
    <>
      <Helmet>
        <title>Artist Content - ${APP_TITLE}</title>
        <meta name="description" content="Your content studio" />
      </Helmet>

      <AppBarWrapper showCreate={true} />

      <Box sx={{ flexGrow: 1, mt: 8 }} className="WidthCentered">
        <Toolbar />

        <Box sx={{ width: '100%', ml: 11, mb: 3 }}>
          <Typography variant="h5" fontWeight={'bold'}>
            Profile content
          </Typography>
        </Box>

        <ContentTabs />
      </Box>
    </>
  );
};

export default ContentStudio;
