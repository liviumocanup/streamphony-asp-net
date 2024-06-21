import { Link } from 'react-router-dom';
import { Button } from '@mui/material';
import { SIGN_UP_ROUTE } from '../../../../routes/routes';

const SignUpButton = () => {
  return (
    <Button component={Link} to={SIGN_UP_ROUTE} color="inherit" sx={{ mr: 1 }}>
      Sign Up
    </Button>
  );
};

export default SignUpButton;
