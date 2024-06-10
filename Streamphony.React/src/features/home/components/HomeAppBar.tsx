import {
  Button,
  Divider,
  IconButton,
  Toolbar,
  Typography,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { AppBar } from '../styles/AppBarStyle';
import MaterialUISwitch from '../styles/ThemeSwitchStyle';
import { Link } from 'react-router-dom';
import useAuthStatus from '../../../hooks/useAuthStatus';
import useThemeContext from '../../../hooks/context/useThemeContext';
import { appTitle } from '../../../shared/constants';
import UserAvatar from './UserAvatar';

interface HomeAppBarProps {
  open: boolean;
  handleDrawerOpen: () => void;
  drawerWidth: number;
}

const HomeAppBar = ({
  open,
  handleDrawerOpen,
  drawerWidth,
}: HomeAppBarProps) => {
  const { currentTheme, toggleTheme } = useThemeContext();
  const isLoggedIn = useAuthStatus();

  return (
    <AppBar
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
          sx={{ mr: 2, ...(open && { display: 'none' }) }}
        >
          <MenuIcon />
        </IconButton>

        <Typography variant="h4" align="left" sx={{ flexGrow: 1 }}>
          {appTitle}
        </Typography>

        <MaterialUISwitch
          checked={currentTheme.palette.mode === 'dark'}
          onChange={toggleTheme}
          sx={{ mr: 2 }}
        />

        {isLoggedIn ? (
          <UserAvatar />
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
      <Divider />
    </AppBar>
  );
};

export default HomeAppBar;
