import { useState } from 'react';
import Box from '@mui/material/Box';
import HomeAppBar from './components/HomeAppBar';
import Sidebar from './components/Sidebar';
import Feed from './components/Feed';
import { Main } from './styles/MainStyle';
import { DrawerHeader } from './styles/DrawerHeaderStyle';
import { useTheme } from '@mui/material';

const drawerWidth = 240;

interface HomeProps {
    toggleTheme: () => void;
}

const Home = ({ toggleTheme }: HomeProps) => {
    const theme = useTheme();
    const [open, setOpen] = useState(false);

    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const handleDrawerClose = () => {
        setOpen(false);
    };

    return (
        <Box sx={{ display: 'flex', backgroundColor: theme.palette.background.default }}>
            <HomeAppBar toggleTheme={toggleTheme} open={open} handleDrawerOpen={handleDrawerOpen} drawerWidth={drawerWidth} />
            <Sidebar open={open} handleDrawerClose={handleDrawerClose} drawerWidth={drawerWidth} />
            <Main open={open} drawerWidth={drawerWidth}>
                <DrawerHeader />
                <Feed />
            </Main>
        </Box>
    );
}

export default Home;