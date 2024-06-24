import { List, Divider, Box } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import LibraryMusicIcon from '@mui/icons-material/LibraryMusic';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import SettingsIcon from '@mui/icons-material/Settings';
import RoundedHoverButton from '../RoundedHoverButton';
import useAuthContext from '../../hooks/context/useAuthContext';
import { NavigateFunction, useNavigate } from 'react-router-dom';
import {
  HOME_ROUTE,
  LIBRARY_ROUTE,
  SETTINGS_ROUTE,
  STUDIO_ROUTE,
} from '../../routes/routes';
import DrawOutlinedIcon from '@mui/icons-material/DrawOutlined';
import { ReactElement } from 'react';

interface MenuItems {
  name: string;
  icon: ReactElement;
  onClick: () => void;
}

interface SidebarItemsProps {
  menuItems?: MenuItems[];
}

const defaultMenuItems = (navigate: NavigateFunction, logOut: () => void) => [
  { name: 'Home', icon: <HomeIcon />, onClick: () => navigate(HOME_ROUTE) },
  {
    name: 'Your Library',
    icon: <LibraryMusicIcon />,
    onClick: () => navigate(LIBRARY_ROUTE),
  },
  {
    name: 'Content Studio',
    icon: <DrawOutlinedIcon />,
    onClick: () => navigate(STUDIO_ROUTE),
  },
  {
    name: 'Settings',
    icon: <SettingsIcon />,
    onClick: () => navigate(SETTINGS_ROUTE),
  },
  { name: 'Log Out', icon: <ExitToAppIcon />, onClick: logOut },
];

const SidebarItems = ({ menuItems }: SidebarItemsProps) => {
  const navigate = useNavigate();
  const { logOut } = useAuthContext();

  const items = menuItems || defaultMenuItems(navigate, logOut);

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        height: '100%',
        mr: 2,
        ml: 1,
        pl: 1,
      }}
    >
      <List sx={{ flexGrow: 1 }}>
        {items.slice(0, 3).map((item) => (
          <RoundedHoverButton key={item.name} item={item} />
        ))}
      </List>
      <Divider />
      <List>
        {items.slice(3).map((item) => (
          <RoundedHoverButton key={item.name} item={item} />
        ))}
      </List>
    </Box>
  );
};

export default SidebarItems;
