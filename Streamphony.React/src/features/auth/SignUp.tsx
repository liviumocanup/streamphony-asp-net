import { Box, IconButton, Link, Typography, useTheme } from "@mui/material";
import './Auth.css';
import SignUpForm from "./components/SignUpForm";
import { Helmet } from "react-helmet-async";
import { Link as RouterLink } from 'react-router-dom';
import Home from "@mui/icons-material/Home";

const SignUp = () => {
    const theme = useTheme();

    return (
        <>
            <Helmet>
                <title>Sign Up</title>
                <meta name="description" content="Sign up for an account" />
            </Helmet>

            <Box className="FormContainer">
                <Typography variant="h3" fontWeight="bold" sx={{ color: theme.palette.text.primary }}>
                    Sign Up
                </Typography>

                <SignUpForm />

                <Typography sx={{ color: theme.palette.text.primary }}>
                    Already have an account? <Link component={RouterLink} to="/logIn">Log in.</Link>
                </Typography>

                <IconButton component={RouterLink} to="/" aria-label="Go back" sx={{ mt: 2 }}>
                    <Home />
                </IconButton>
            </Box>
        </>
    );
}

export default SignUp;
