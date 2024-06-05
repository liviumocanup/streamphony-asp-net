import { IconButton, Drawer, useTheme } from '@mui/material';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import NavigationItems from './NavigationItems';
import { DrawerHeader } from '../styles/DrawerHeaderStyle';
import "../Home.css";

interface SidebarProps {
    open: boolean;
    handleDrawerClose: () => void;
    drawerWidth: number;
}

const Sidebar = ({ open, handleDrawerClose, drawerWidth }: SidebarProps) => {
    const theme = useTheme();

    return (
        <Drawer
            variant="persistent"
            anchor="left"
            open={open}
            sx={{
                width: drawerWidth,
                flexShrink: 0,
                '& .MuiDrawer-paper': {
                    width: drawerWidth,
                    boxSizing: 'border-box',
                    backgroundColor: theme.palette.background.paper,
                },
            }}
        >
            <DrawerHeader>
                <IconButton onClick={handleDrawerClose}>
                    <ChevronLeftIcon />
                </IconButton>
            </DrawerHeader>
            {/* <Divider /> */}
            <NavigationItems />
        </Drawer>
    );
};

export default Sidebar;
