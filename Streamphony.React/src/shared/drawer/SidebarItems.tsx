import { List, Divider, Box } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import LibraryMusicIcon from '@mui/icons-material/LibraryMusic';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import SettingsIcon from '@mui/icons-material/Settings';
import RoundedHoverButton from '../RoundedHoverButton';
import useAuthContext from '../../hooks/context/useAuthContext';
import { useNavigate } from 'react-router-dom';
import { HOME_ROUTE, LIBRARY_ROUTE, SETTINGS_ROUTE } from '../../routes/routes';

const SidebarItems = () => {
  const navigate = useNavigate();
  const { handleLogOut } = useAuthContext();

  const navigateHome = () => navigate(HOME_ROUTE);
  const navigateLibrary = () => navigate(LIBRARY_ROUTE);
  const navigateSettings = () => navigate(SETTINGS_ROUTE);

  const menuItems = [
    { name: 'Home', icon: <HomeIcon />, onClick: navigateHome },
    {
      name: 'Your Library',
      icon: <LibraryMusicIcon />,
      onClick: navigateLibrary,
    },
    { name: 'Settings', icon: <SettingsIcon />, onClick: navigateSettings },
    { name: 'Log Out', icon: <ExitToAppIcon />, onClick: handleLogOut },
  ];

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
        {menuItems.slice(0, 2).map((item) => (
          <RoundedHoverButton key={item.name} item={item} />
        ))}
      </List>
      <Divider />
      <List>
        {menuItems.slice(2).map((item) => (
          <RoundedHoverButton key={item.name} item={item} />
        ))}
      </List>
    </Box>
  );
};

export default SidebarItems;
