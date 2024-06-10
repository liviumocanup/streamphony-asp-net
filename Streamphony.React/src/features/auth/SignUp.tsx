import { Box, IconButton, Link, Typography } from '@mui/material';
import './Auth.css';
import '../../App.css';
import SignUpForm from './components/SignUpForm';
import { Helmet } from 'react-helmet-async';
import { Link as RouterLink } from 'react-router-dom';
import Home from '@mui/icons-material/Home';
import { appTitle } from '../../shared/constants';

const SignUp = () => {
  return (
    <>
      <Helmet>
        <title>Sign Up - {appTitle}</title>
        <meta name="description" content="Sign up for an account" />
      </Helmet>

      <Box className="Centered">
        <Typography variant="h3" fontWeight="bold">
          Sign Up
        </Typography>

        <SignUpForm />

        <Typography sx={{ color: 'text.secondary' }}>
          Already have an account?{' '}
          <Link component={RouterLink} to="/logIn">
            Log in.
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

export default SignUp;
