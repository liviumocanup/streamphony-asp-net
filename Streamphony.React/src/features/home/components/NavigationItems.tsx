import {
  List,
  ListItemIcon,
  ListItemText,
  Divider,
  Box,
  ListItemButton,
} from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import LibraryMusicIcon from '@mui/icons-material/LibraryMusic';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import SettingsIcon from '@mui/icons-material/Settings';

const menuItems = [
  { name: 'Home', icon: <HomeIcon /> },
  { name: 'Your Library', icon: <LibraryMusicIcon /> },
  { name: 'Settings', icon: <SettingsIcon /> },
  { name: 'Log Out', icon: <ExitToAppIcon /> },
];

const NavigationItems = () => {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
      <List sx={{ flexGrow: 1 }}>
        {menuItems.slice(0, 2).map((item) => (
          <ListItemButton key={item.name}>
            <ListItemIcon>{item.icon}</ListItemIcon>
            <ListItemText primary={item.name} />
          </ListItemButton>
        ))}
      </List>
      <Divider />
      <List>
        {menuItems.slice(2).map((item) => (
          <ListItemButton key={item.name}>
            <ListItemIcon>{item.icon}</ListItemIcon>
            <ListItemText primary={item.name} />
          </ListItemButton>
        ))}
      </List>
    </Box>
  );
};

export default NavigationItems;
