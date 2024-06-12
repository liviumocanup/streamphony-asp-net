import { Divider, Menu } from '@mui/material';
import { Logout, PersonOutline, Settings } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import useAuthContext from '../../../hooks/context/useAuthContext';
import UserMenuItem from './UserMenuItem';
import { ReactNode } from 'react';
import {
  ACCOUNT_ROUTE,
  HOME_ROUTE,
  SETTINGS_ROUTE,
} from '../../../routes/routes';

interface UserMenuProps {
  anchorEl: null | HTMLElement;
  open: boolean;
  closeMenu: () => void;
  items?: ReactNode[];
}

const UserMenu = ({ anchorEl, open, closeMenu, items }: UserMenuProps) => {
  const navigate = useNavigate();
  const { handleLogOut } = useAuthContext();

  const navigateToPage = (path: string) => {
    closeMenu();
    navigate(path);
  };

  const navigateToAccount = () => navigateToPage(ACCOUNT_ROUTE);
  const navigateToSettings = () => navigateToPage(SETTINGS_ROUTE);
  const logOut = () => {
    handleLogOut();
    navigate(HOME_ROUTE);
  };

  const itemList = [
    <UserMenuItem
      text="Account"
      icon={<PersonOutline fontSize="small" />}
      onClick={navigateToAccount}
    />,

    <Divider />,

    <UserMenuItem
      text="Settings"
      icon={<Settings fontSize="small" />}
      onClick={navigateToSettings}
    />,

    <UserMenuItem
      text="Log out"
      icon={<Logout fontSize="small" />}
      onClick={logOut}
    />,
  ];

  return (
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
      {items ? items : itemList}
    </Menu>
  );
};

export default UserMenu;
