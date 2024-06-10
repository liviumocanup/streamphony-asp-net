import { Box, Typography } from '@mui/material';
import HomeSkeleton from '../../../../shared/HomeSkeleton';
import React from 'react';

interface FallbackSectionContainerProps {
  children: React.ReactNode;
}

const FallbackSectionContainer = ({
  children,
}: FallbackSectionContainerProps) => {
  return (
    <Box sx={{ ml: 2, mb: 4 }}>
      <Typography variant="h5" sx={{ pb: 1, pt: 2 }} width="10vw">
        <HomeSkeleton color="background.paper" />
      </Typography>

      {children}
    </Box>
  );
};

export default FallbackSectionContainer;
