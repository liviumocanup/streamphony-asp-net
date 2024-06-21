import { Divider, Menu } from '@mui/material';
import { Logout, PersonOutline, Settings } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import MenuItemWithIcon from '../appBar/components/MenuItemWithIcon';
import { ReactNode } from 'react';
import { ACCOUNT_ROUTE, SETTINGS_ROUTE } from '../../routes/routes';
import { useAuth0 } from '@auth0/auth0-react';

interface UserMenuProps {
  anchorEl: null | HTMLElement;
  open: boolean;
  closeMenu: () => void;
  items?: ReactNode[];
}

const UserMenu = ({ anchorEl, open, closeMenu, items }: UserMenuProps) => {
  const navigate = useNavigate();
  const { logout } = useAuth0();

  const navigateToPage = (path: string) => {
    closeMenu();
    navigate(path);
  };

  const navigateToAccount = () => navigateToPage(ACCOUNT_ROUTE);
  const navigateToSettings = () => navigateToPage(SETTINGS_ROUTE);

  const itemList = [
    <MenuItemWithIcon
      key="account"
      text="Account"
      icon={<PersonOutline fontSize="small" />}
      onClick={navigateToAccount}
    />,

    <Divider key="divider" />,

    <MenuItemWithIcon
      key="settings"
      text="Settings"
      icon={<Settings fontSize="small" />}
      onClick={navigateToSettings}
    />,

    <MenuItemWithIcon
      key="logout"
      text="Log out"
      icon={<Logout fontSize="small" />}
      onClick={() =>
        logout({ logoutParams: { returnTo: window.location.origin } })
      }
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
