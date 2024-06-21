import { Button } from '@mui/material';
import { APP_GREEN } from '../constants';
import { useAuth0 } from '@auth0/auth0-react';

const LogInButton = () => {
  const { loginWithRedirect } = useAuth0();

  return (
    <Button
      onClick={() => loginWithRedirect()}
      variant="contained"
      sx={{
        bgcolor: APP_GREEN,
        '&:hover': {
          bgcolor: APP_GREEN,
        },
      }}
    >
      Log in
    </Button>
  );
};

export default LogInButton;
