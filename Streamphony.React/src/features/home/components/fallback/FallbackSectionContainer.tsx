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
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
        }}
      >
        <Typography variant="h5" sx={{ ml: 4, pb: 1, pt: 2 }} width="10vw">
          <HomeSkeleton color="background.paper" />
        </Typography>
      </Box>
      {children}
    </Box>
  );
};

export default FallbackSectionContainer;
