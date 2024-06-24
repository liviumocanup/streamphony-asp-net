import { Box, Typography } from '@mui/material';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import CreateSongForm from './components/upload/CreateSongForm';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';
import { useLocation } from 'react-router-dom';

const CreateSong = () => {
  return (
    <>
      <Helmet>
        <title>Add Song - {APP_TITLE}</title>
        <meta name="description" content="Upload a new song" />
      </Helmet>

      <AppBarWrapper
        showCreate={true}
        children={
          <Box sx={{ mt: 7 }} className="CenteredContainer">
            <Box sx={{ width: '60rem' }}>
              <Typography variant="h4" sx={{ fontWeight: 'bold', mb: 3 }}>
                Create your Release
              </Typography>

              <Box
                sx={{
                  p: 4,
                  borderRadius: '10px',
                  bgcolor: 'background.paper',
                }}
              >
                <CreateSongForm />
              </Box>
            </Box>
          </Box>
        }
      />
    </>
  );
};

export default CreateSong;
