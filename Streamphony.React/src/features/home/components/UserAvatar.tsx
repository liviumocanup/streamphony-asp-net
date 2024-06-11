import {
  Avatar,
  Divider,
  IconButton,
  ListItemIcon,
  Menu,
  MenuItem,
  Tooltip,
} from '@mui/material';
import React from 'react';
import { Logout, PersonOutline, Settings } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import useAuthStatus from '../../../hooks/useAuthStatus';

const UserAvatar = () => {
  const navigate = useNavigate();
  const { handleLogOut } = useAuthStatus();
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const openMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const closeMenu = () => {
    setAnchorEl(null);
  };

  const navigateToPage = (path: string) => {
    closeMenu();
    navigate(path);
  };

  const navigateToAccount = () => navigateToPage('/account');
  const navigateToSettings = () => navigateToPage('/settings');
  const logOut = () => {
    handleLogOut();
    navigate('/');
    navigate(0);
  };

  return (
    <>
      <Tooltip title="Account Settings">
        <IconButton
          onClick={openMenu}
          size="small"
          aria-controls={open ? 'account-menu' : undefined}
          aria-haspopup="true"
          aria-expanded={open ? 'true' : undefined}
        >
          <Avatar sx={{ width: 32, height: 32 }}>U</Avatar>
        </IconButton>
      </Tooltip>

      <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={open}
        onClose={closeMenu}
        onClick={closeMenu}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
        disableScrollLock={true}
      >
        <MenuItem onClick={navigateToAccount}>
          <ListItemIcon>
            <PersonOutline fontSize="small" />
          </ListItemIcon>
          Account
        </MenuItem>

        <Divider />

        <MenuItem onClick={navigateToSettings}>
          <ListItemIcon>
            <Settings fontSize="small" />
          </ListItemIcon>
          Settings
        </MenuItem>

        <MenuItem onClick={logOut}>
          <ListItemIcon>
            <Logout fontSize="small" />
          </ListItemIcon>
          Log out
        </MenuItem>
      </Menu>
    </>
  );
};

export default UserAvatar;
