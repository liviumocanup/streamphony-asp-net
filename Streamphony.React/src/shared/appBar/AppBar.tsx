import { Button, IconButton, Toolbar, Typography } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { Link } from 'react-router-dom';
import { APP_TITLE } from '../constants';
import UserAvatar from './UserAvatar';
import ThemeToggleButton from './components/ThemeToggleButton';
import useAuthContext from '../../hooks/context/useAuthContext';
import { StyledAppBar } from './AppBarStyle';
import { ReactNode } from 'react';

interface AppBarProps {
  open: boolean;
  handleDrawerOpen: () => void;
  drawerWidth?: number;
  hideToggleOnOpen?: boolean;
  avatarItems?: ReactNode[];
}

const AppBar = ({
  open,
  handleDrawerOpen,
  drawerWidth = 0,
  hideToggleOnOpen = true,
  avatarItems,
}: AppBarProps) => {
  const { isLoggedIn } = useAuthContext();

  return (
    <StyledAppBar
      open={open}
      elevation={0}
      drawerWidth={drawerWidth}
      sx={{
        color: 'text.primary',
        bgcolor: 'background.default',
      }}
    >
      <Toolbar>
        <IconButton
          color="inherit"
          aria-label="open sidebar"
          onClick={handleDrawerOpen}
          edge="start"
          sx={{
            ml: '1px',
            mr: 2,
            ...(hideToggleOnOpen && open && { display: 'none' }),
          }}
        >
          <MenuIcon />
        </IconButton>

        <Typography variant="h4" align="left" sx={{ flexGrow: 1 }}>
          <Link to="/" style={{ textDecoration: 'none', color: 'inherit' }}>
            {APP_TITLE}
          </Link>
        </Typography>

        <ThemeToggleButton />

        {isLoggedIn ? (
          <UserAvatar menuItems={avatarItems} />
        ) : (
          <>
            <Button
              component={Link}
              to="/signUp"
              color="inherit"
              sx={{ mr: 1 }}
            >
              Sign Up
            </Button>

            <Button
              component={Link}
              to="/logIn"
              color="inherit"
              variant="outlined"
            >
              Log in
            </Button>
          </>
        )}
      </Toolbar>
    </StyledAppBar>
  );
};

export default AppBar;
