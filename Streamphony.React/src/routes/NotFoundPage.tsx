import { Link } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';
import { Box, Button, Typography } from '@mui/material';
import '../App.css';
import { appTitle } from '../shared/constants';

const NotFoundPage = () => {
  return (
    <>
      <Helmet>
        <title>Page not found - {appTitle}</title>
        <meta name="description" content="Page not found" />
      </Helmet>

      <Box className="Centered">
        <Typography
          variant="h4"
          fontWeight="bold"
          sx={{ color: 'text.primary', mb: 6 }}
        >
          404
        </Typography>

        <Typography
          variant="h3"
          fontWeight="bold"
          sx={{ color: 'text.primary', mb: 2 }}
        >
          Page not found
        </Typography>

        <Typography sx={{ color: 'text.secondary' }}>
          We can’t seem to find the page you are looking for.
        </Typography>

        <Button variant={'outlined'} component={Link} to="/" sx={{ mt: 5 }}>
          Home
        </Button>
      </Box>
    </>
  );
};

export default NotFoundPage;
