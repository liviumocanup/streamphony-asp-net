import { Skeleton } from '@mui/material';
import React from 'react';

interface HomeSkeletonProps {
  variant?: 'circular' | 'rounded';
  color?:
    | 'inherit'
    | 'primary'
    | 'secondary'
    | 'text.primary'
    | 'text.secondary'
    | 'background.paper';
  children?: React.ReactNode;
}

const HomeSkeleton = ({ variant, color, children }: HomeSkeletonProps) => {
  return (
    <Skeleton animation="wave" variant={variant} sx={{ bgcolor: color }}>
      {children}
    </Skeleton>
  );
};

export default HomeSkeleton;
