import { Button, Divider, IconButton, Toolbar, Typography, useTheme } from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import { AppBar } from "../styles/AppBarStyle";
import MaterialUISwitch from "../styles/ThemeSwitchStyle";

interface HomeAppBarProps {
    toggleTheme: () => void;
    open: boolean;
    handleDrawerOpen: () => void;
    drawerWidth: number;
}

const HomeAppBar = ({ toggleTheme, open, handleDrawerOpen, drawerWidth }: HomeAppBarProps) => {
    const theme = useTheme();

    return (
        <AppBar
            open={open}
            elevation={0}
            drawerWidth={drawerWidth}
            sx={{
                color: theme.palette.text.primary,
                backgroundColor: theme.palette.background.default,
            }}
        >
            <Toolbar>
                <IconButton
                    color="inherit"
                    aria-label="open sidebar"
                    onClick={handleDrawerOpen}
                    edge="start"
                    sx={{ mr: 2, ...(open && { display: 'none' }) }}
                >
                    <MenuIcon />
                </IconButton>
                <Typography variant="h4" align="left" sx={{ flexGrow: 1 }}>
                    Streamphony
                </Typography>
                <MaterialUISwitch checked={theme.palette.mode === 'dark'} onChange={toggleTheme} />
                <Button color="inherit" sx={{ mr: 1 }}>Sign Up</Button>
                <Button color="inherit" variant="outlined">Log in</Button>
            </Toolbar>
            <Divider />
        </AppBar>
    );
}

export default HomeAppBar;