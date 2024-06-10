import { Box, Typography } from '@mui/material';
import React from 'react';

interface SectionContainerProps {
  sectionTitle: string;
  children: React.ReactNode;
}

const SectionContainer = ({
  sectionTitle,
  children,
}: SectionContainerProps) => {
  return (
    <Box sx={{ ml: 2, mb: 4 }}>
      <Typography
        variant="h5"
        align="left"
        sx={{
          pb: 1,
          pt: 2,
          color: 'text.primary',
        }}
      >
        {sectionTitle}
      </Typography>

      {children}
    </Box>
  );
};

export default SectionContainer;
