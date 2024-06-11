import { Avatar, Button, IconButton } from '@mui/material';
import Home from '@mui/icons-material/Home';
import { useNavigate } from 'react-router-dom';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { appTitle } from '../../shared/constants';
import useAuthContext from '../../hooks/context/useAuthContext';

const AccountCabinet = () => {
  const { isLoggedIn, handleLogOut } = useAuthContext();
  const navigate = useNavigate();

  const navigateHome = () => navigate('/');
  const logOut = () => {
    handleLogOut();
    navigateHome();
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
            onClick={logOut}
            variant="contained"
            aria-label="Log Out"
            sx={{ mt: 2 }}
          >
            Log Out
          </Button>
          <IconButton
            onClick={navigateHome}
            aria-label="Go Home"
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
