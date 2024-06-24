import { Avatar, Box, Typography } from '@mui/material';
import { ReactNode } from 'react';

interface HeaderProps {
  type: string;
  name: string;
  coverUrl: string;
  children: ReactNode;
}

const ProfileHeaderWrapper = ({
  type,
  name,
  coverUrl,
  children,
}: HeaderProps) => {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'row' }}>
      <Avatar
        src={coverUrl}
        variant={'rounded'}
        alt={name}
        sx={{
          width: 230,
          height: 230,
          mr: 2,
          zIndex: 100,
          boxShadow: '0px 0px 40px rgba(0, 0, 0, 0.3)',
        }}
      />
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'flex-end',
          zIndex: 100,
        }}
      >
        <Typography
          variant={'subtitle2'}
          fontWeight={'medium'}
          sx={{ ml: 0.5 }}
        >
          {type}
        </Typography>
        <Typography
          variant={'h1'}
          fontWeight={'bold'}
          noWrap
          overflow={'auto'}
          width={'70vw'}
        >
          {name}
        </Typography>

        {children}
      </Box>
    </Box>
  );
};

export default ProfileHeaderWrapper;
