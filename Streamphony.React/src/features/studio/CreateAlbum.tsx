import { Box, Typography } from '@mui/material';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';
import CreateAlbumForm from './components/upload/CreateAlbumForm';

const CreateAlbum = () => {
  return (
    <>
      <Helmet>
        <title>Add Album - {APP_TITLE}</title>
        <meta name="description" content="Release a new album" />
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
                <CreateAlbumForm />
              </Box>
            </Box>
          </Box>
        }
      />
    </>
  );
};

export default CreateAlbum;
