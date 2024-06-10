import { Avatar, Button, IconButton } from '@mui/material';
import Home from '@mui/icons-material/Home';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import '../../App.css';
import useAuthStatus from '../../hooks/useAuthStatus';
import useTokenStorage from '../../hooks/localStorage/useTokenStorage';
import { Helmet } from 'react-helmet-async';
import { appTitle } from '../../shared/constants';

const AccountCabinet = () => {
  const isLoggedIn = useAuthStatus();
  const { removeToken } = useTokenStorage();
  const navigate = useNavigate();

  const handleLogOut = () => {
    removeToken();
    navigate('/');
  };

  return (
    <>
      <Helmet>
        <title>Username - {appTitle}</title>
        <meta name="description" content="Your account settings" />
      </Helmet>

      {isLoggedIn ? (
        <div className="Centered">
          <Avatar sx={{ bgcolor: 'secondary.main' }}>U</Avatar>
          <Button
            onClick={handleLogOut}
            variant="contained"
            aria-label="Log Out"
            sx={{ mt: 2 }}
          >
            Log Out
          </Button>
          <IconButton
            component={RouterLink}
            to="/"
            aria-label="Go back"
            sx={{ mt: 2 }}
          >
            <Home />
          </IconButton>
        </div>
      ) : (
        <h1>Not Logged In</h1>
      )}
    </>
  );
};

export default AccountCabinet;
