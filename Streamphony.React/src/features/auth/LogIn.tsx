import { Box, IconButton, Link, Typography, useTheme } from "@mui/material";
import LogInForm from "./components/LogInForm";
import './Auth.css';
import { Helmet } from "react-helmet-async";
import { Link as RouterLink } from 'react-router-dom';
import Home from "@mui/icons-material/Home";

const LogIn = () => {
    const theme = useTheme();

    return (
        <>
            <Helmet>
                <title>Log In</title>
                <meta name="description" content="Log in to your account" />
            </Helmet>

            <Box className="FormContainer">
                <Typography variant="h3" fontWeight="bold" sx={{ color: theme.palette.text.primary }}>
                    Log In
                </Typography>

                <LogInForm />

                <Typography sx={{ color: theme.palette.text.primary }}>
                    Don't have an account? <Link component={RouterLink} to="/signUp">Sign Up.</Link>
                </Typography>

                <IconButton component={RouterLink} to="/" aria-label="Go back" sx={{ mt: 2 }}>
                    <Home />
                </IconButton>
            </Box>
        </>
    );
}

export default LogIn;