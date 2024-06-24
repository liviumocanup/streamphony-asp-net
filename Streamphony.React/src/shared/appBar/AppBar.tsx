import { IconButton, Toolbar, Typography } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { Link } from 'react-router-dom';
import { APP_TITLE } from '../constants';
import UserAvatar from './UserAvatar';
import ThemeToggleButton from './components/ThemeToggleButton';
import useAuthContext from '../../hooks/context/useAuthContext';
import { StyledAppBar } from './AppBarStyle';
import { ReactNode } from 'react';
import { HOME_ROUTE } from '../../routes/routes';
import StudioCreateButton from './StudioCreateButton';
import LogInButton from './components/auth/LogInButton';
import SignUpButton from './components/auth/SignUpButton';

interface AppBarProps {
  open: boolean;
  handleDrawerOpen: () => void;
  drawerWidth?: number;
  showCreate?: boolean;
  avatarItems?: ReactNode[];
}

const AppBar = ({
  open,
  handleDrawerOpen,
  drawerWidth = 0,
  avatarItems,
  showCreate = false,
}: AppBarProps) => {
  const { isLoggedIn } = useAuthContext();

  return (
    <StyledAppBar
      open={open}
      elevation={1}
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
            ...(open && { display: 'none' }),
          }}
        >
          <MenuIcon />
        </IconButton>

        <Typography variant="h4" align="left" sx={{ flexGrow: 1 }}>
          <Link
            to={HOME_ROUTE}
            style={{ textDecoration: 'none', color: 'inherit' }}
          >
            {APP_TITLE}
          </Link>
        </Typography>

        <ThemeToggleButton />

        {showCreate && <StudioCreateButton />}

        {isLoggedIn ? (
          <UserAvatar menuItems={avatarItems} />
        ) : (
          <>
            <SignUpButton />

            <LogInButton />
          </>
        )}
      </Toolbar>
    </StyledAppBar>
  );
};

export default AppBar;
