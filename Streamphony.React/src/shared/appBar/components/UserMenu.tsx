import { Divider, Menu } from '@mui/material';
import { Logout, PersonOutline, Settings } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import useAuthContext from '../../../hooks/context/useAuthContext';
import BarMenuItem from './BarMenuItem';
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
  const { logOut } = useAuthContext();

  const navigateToPage = (path: string) => {
    closeMenu();
    navigate(path);
  };

  const navigateToAccount = () => navigateToPage(ACCOUNT_ROUTE);
  const navigateToSettings = () => navigateToPage(SETTINGS_ROUTE);
  const handleLogOut = () => {
    logOut();
    navigate(HOME_ROUTE);
  };

  const itemList = [
    <BarMenuItem
      key="account"
      text="Account"
      icon={<PersonOutline fontSize="small" />}
      onClick={navigateToAccount}
    />,

    <Divider key="divider" />,

    <BarMenuItem
      key="settings"
      text="Settings"
      icon={<Settings fontSize="small" />}
      onClick={navigateToSettings}
    />,

    <BarMenuItem
      key="logout"
      text="Log out"
      icon={<Logout fontSize="small" />}
      onClick={handleLogOut}
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
