import { Link } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';
import { Box, Button, Typography } from '@mui/material';
import '../App.css';
import { APP_TITLE } from '../shared/constants';
import { HOME_ROUTE } from './routes';

const NotFoundPage = () => {
  return (
    <>
      <Helmet>
        <title>Page not found - {APP_TITLE}</title>
        <meta name="description" content="Page not found" />
      </Helmet>

      <Box className="WidthCentered HeightCentered">
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

        <Button
          variant={'outlined'}
          component={Link}
          to={HOME_ROUTE}
          sx={{ mt: 5 }}
        >
          Home
        </Button>
      </Box>
    </>
  );
};

export default NotFoundPage;
