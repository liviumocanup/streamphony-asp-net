import { Box, IconButton, Typography } from '@mui/material';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import { DrawerHeader } from '../home/styles/DrawerHeaderStyle';
import AppBarWrapper from './AppBarWrapper';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import RegisterArtistForm from './components/RegisterArtistForm';
import { useNavigate } from 'react-router-dom';
import { ACCOUNT_ROUTE } from '../../routes/routes';

const RegisterArtist = () => {
  const navigate = useNavigate();

  const navigateBack = () => {
    navigate(ACCOUNT_ROUTE);
  };

  return (
    <>
      <Helmet>
        <title>Register Artist - {APP_TITLE}</title>
        <meta name="description" content="Registering your Artist Profile" />
      </Helmet>

      <AppBarWrapper />

      <Box sx={{ flexGrow: 1, mt: 5 }} className="WidthCentered">
        <DrawerHeader />

        <Box sx={{ width: '40rem' }}>
          <IconButton
            onClick={navigateBack}
            aria-label="Go Back"
            sx={{ bgcolor: 'background.paper', p: '12px', mb: 5 }}
          >
            <ArrowBackIosNewIcon />
          </IconButton>

          <Typography variant="h3" sx={{ fontWeight: 'bold', mb: 3 }}>
            Register Artist
          </Typography>

          <RegisterArtistForm />
        </Box>
      </Box>
    </>
  );
};

export default RegisterArtist;
