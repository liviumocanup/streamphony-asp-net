import { Box, Typography } from '@mui/material';
import React from 'react';

interface SectionContainerProps {
  sectionTitle: string;
  children: React.ReactNode;
  totalRecords: number;
  pageSize: number;
  pageNumber: number;
}

const SectionContainer = ({
  sectionTitle,
  children,
  totalRecords,
  pageSize,
  pageNumber,
}: SectionContainerProps) => {
  return (
    <Box sx={{ ml: 2, mb: 4 }}>
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
        }}
      >
        <Typography
          variant="h5"
          align="left"
          sx={{
            ml: 4,
            pb: 1,
            pt: 2,
            color: 'text.primary',
          }}
        >
          {sectionTitle}
        </Typography>

        <Typography
          align="left"
          sx={{
            mr: 6,
            pb: 1,
            pt: 3,
            color: 'text.primary',
          }}
        >
          Page {pageNumber} of {Math.ceil(totalRecords / pageSize)}
        </Typography>
      </Box>

      {children}
    </Box>
  );
};

export default SectionContainer;
