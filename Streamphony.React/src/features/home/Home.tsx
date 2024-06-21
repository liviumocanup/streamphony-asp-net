import Box from '@mui/material/Box';
import Feed from './components/Feed';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';

const Home = () => {
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
        <AppBarWrapper showPlayer={true} children={<Feed />} />
      </Box>
    </>
  );
};

export default Home;
