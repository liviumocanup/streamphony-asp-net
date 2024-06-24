import { useNavigate, useParams } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';
import { Box, IconButton, Typography } from '@mui/material';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import EditSongForm from './components/EditSongForm';
import { STUDIO_ROUTE } from '../../routes/routes';
import useGetSong from '../viewDetails/hooks/useGetSong';

const EditSong = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { data: song, isLoading, isError } = useGetSong(id);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError) {
    return <div>Error</div>;
  }

  const navigateBack = () => {
    navigate(STUDIO_ROUTE);
  };

  return (
    <>
      <Helmet>
        <title>Edit {song.title} Details</title>
        <meta
          name="description"
          content={`Edit Details for Song ${song.title}.`}
        />
      </Helmet>

      <AppBarWrapper
        children={
          <Box sx={{ mt: 7 }} className="CenteredContainer">
            <Box sx={{ width: '40rem' }}>
              <IconButton
                onClick={navigateBack}
                aria-label="Go Back"
                sx={{ bgcolor: 'background.paper', p: '12px', mb: 5 }}
              >
                <ArrowBackIosNewIcon />
              </IconButton>

              <Typography variant="h3" sx={{ fontWeight: 'bold', mb: 3 }}>
                Edit Song
              </Typography>

              <EditSongForm song={song} />
            </Box>
          </Box>
        }
      />
    </>
  );
};

export default EditSong;
