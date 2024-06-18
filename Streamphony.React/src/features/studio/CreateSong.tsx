import { Box, IconButton, Typography } from '@mui/material';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import { DrawerHeader } from '../home/styles/DrawerHeaderStyle';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import { useNavigate } from 'react-router-dom';
import { STUDIO_ROUTE } from '../../routes/routes';
import CreateSongForm from './components/CreateSongForm';
import AppBarWrapper from '../accountCabinet/AppBarWrapper';

const CreateSong = () => {
  const navigate = useNavigate();

  const navigateBack = () => {
    navigate(STUDIO_ROUTE);
  };

  return (
    <>
      <Helmet>
        <title>Add Song - {APP_TITLE}</title>
        <meta name="description" content="Upload a new song" />
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
            Create your Release
          </Typography>

          <CreateSongForm />
        </Box>
      </Box>
    </>
  );
};

export default CreateSong;
