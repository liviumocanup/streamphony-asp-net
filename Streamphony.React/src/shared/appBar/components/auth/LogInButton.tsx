import { Link } from 'react-router-dom';
import { Button } from '@mui/material';
import { APP_GREEN } from '../../../constants';
import { LOG_IN_ROUTE } from '../../../../routes/routes';

const LogInButton = () => {
  return (
    <Button
      component={Link}
      to={LOG_IN_ROUTE}
      variant="contained"
      sx={{
        bgcolor: APP_GREEN,
        '&:hover': {
          bgcolor: APP_GREEN,
          color: 'white',
        },
      }}
    >
      Log in
    </Button>
  );
};

export default LogInButton;
