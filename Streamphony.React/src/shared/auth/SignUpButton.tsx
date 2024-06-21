import { Button } from '@mui/material';
import { useAuth0 } from '@auth0/auth0-react';

const SignUpButton = () => {
  const { loginWithRedirect } = useAuth0();

  return (
    <Button onClick={() => loginWithRedirect()} sx={{ mr: 2 }}>
      Sign Up
    </Button>
  );
};

export default SignUpButton;
