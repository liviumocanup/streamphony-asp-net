import { Drawer } from '@mui/material';
import '../../features/home/Home.css';
import PlaylistSidebarItems from './PlaylistSidebarItems';

const drawerWidth = 500;

interface SidebarProps {
  open: boolean;
  handleDrawerClose: () => void;
}

const TemporaryDrawer = ({ open, handleDrawerClose }: SidebarProps) => {
  return (
    <Drawer
      anchor="right"
      open={open}
      onClose={handleDrawerClose}
      sx={{
        width: `${drawerWidth}px`,
        '& .MuiDrawer-paper': {
          borderTopLeftRadius: 10,
          borderBottomLeftRadius: 10,
          bgcolor: 'background.default',
        },
      }}
    >
      <PlaylistSidebarItems />
    </Drawer>
  );
};

export default TemporaryDrawer;
