import { IconButton, Drawer } from '@mui/material';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import NavigationItems from './NavigationItems';
import { DrawerHeader } from '../styles/DrawerHeaderStyle';
import '../Home.css';

interface SidebarProps {
  open: boolean;
  handleDrawerClose: () => void;
  drawerWidth: number;
}

const Sidebar = ({ open, handleDrawerClose, drawerWidth }: SidebarProps) => {
  return (
    <Drawer
      variant="persistent"
      anchor="left"
      open={open}
      sx={{
        width: `${drawerWidth}px`,
        flexShrink: 0,
        '& .MuiDrawer-paper': {
          width: `${drawerWidth}px`,
          boxSizing: 'border-box',
          bgcolor: 'background.paper',
        },
      }}
    >
      <DrawerHeader>
        <IconButton onClick={handleDrawerClose}>
          <ChevronLeftIcon />
        </IconButton>
      </DrawerHeader>
      <NavigationItems />
    </Drawer>
  );
};

export default Sidebar;
