import Drawer from '@mui/material/Drawer';
import SidebarItems from '../../../shared/drawer/SidebarItems';
import { DrawerHeader } from '../../home/styles/DrawerHeaderStyle';
import { IconButton, Typography } from '@mui/material';
import { APP_TITLE } from '../../../shared/constants';
import MenuIcon from '@mui/icons-material/Menu';

interface TemporaryDrawerProps {
  open: boolean;
  handleDrawerClose: () => void;
}

const TemporaryDrawer = ({ open, handleDrawerClose }: TemporaryDrawerProps) => {
  return (
    <Drawer
      open={open}
      onClose={handleDrawerClose}
      sx={{
        flexShrink: 0,
        p: 1,
        '& .MuiDrawer-paper': {
          boxSizing: 'border-box',
          bgcolor: 'background.paper',
        },
      }}
    >
      <DrawerHeader>
        <IconButton
          color="inherit"
          aria-label="close sidebar"
          onClick={handleDrawerClose}
          edge="start"
          sx={{ ml: 1, mr: 2 }}
        >
          <MenuIcon />
        </IconButton>

        <Typography variant="h4" align="left" sx={{ flexGrow: 1 }}>
          {APP_TITLE}
        </Typography>
      </DrawerHeader>

      <SidebarItems />
    </Drawer>
  );
};

export default TemporaryDrawer;
