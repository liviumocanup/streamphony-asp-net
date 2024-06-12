import { Box, IconButton, Link, Typography } from '@mui/material';
import LogInForm from './components/LogInForm';
import './Auth.css';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { Link as RouterLink } from 'react-router-dom';
import Home from '@mui/icons-material/Home';
import { APP_TITLE } from '../../shared/constants';

const LogIn = () => {
  return (
    <>
      <Helmet>
        <title>Log In - {APP_TITLE}</title>
        <meta name="description" content="Log in to your account" />
      </Helmet>

      <Box className="WidthCentered HeightCentered">
        <Typography variant="h3" fontWeight="bold">
          Log In
        </Typography>

        <LogInForm />

        <Typography sx={{ color: 'text.secondary' }}>
          Don't have an account?{' '}
          <Link component={RouterLink} to="/signUp">
            Sign Up.
          </Link>
        </Typography>

        <IconButton
          component={RouterLink}
          to="/"
          aria-label="Go back"
          sx={{ mt: 2 }}
        >
          <Home />
        </IconButton>
      </Box>
    </>
  );
};

export default LogIn;
