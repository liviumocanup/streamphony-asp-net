import { Box, Typography, useTheme } from "@mui/material";

interface FeedSectionProps {
    title: string;
    children: React.ReactNode;
}

const FeedSection = ({ title, children }: FeedSectionProps) => {
    const theme = useTheme();

    return (
        <Box sx={{ ml: 2, mb: 4 }}>
            <Typography
                variant="h5"
                align="left"
                sx={{
                    pb: 1,
                    pt: 2,
                    color: theme.palette.text.primary
                }}
            >
                {title}
            </Typography>
            {children}
        </Box >
    );
}

export default FeedSection;