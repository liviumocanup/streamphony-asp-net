import { Avatar, IconButton, Tooltip } from '@mui/material';
import React from 'react';
import useArtistContext from '../../hooks/context/useArtistContext';
import useAuth from '../../hooks/useAuth';
import LoadingSpinner from '../LoadingSpinner';

interface AccountIconButtonProps {
  open: boolean;
  openMenu: (event: React.MouseEvent<HTMLElement>) => void;
}

const AccountIconButton = ({ open, openMenu }: AccountIconButtonProps) => {
  const { isArtistLinked, pfpUrl } = useArtistContext();
  const { user, isLoading } = useAuth();
  console.log('user', user?.picture);

  if (isLoading) return <LoadingSpinner />;

  return (
    <Tooltip title="Account Settings">
      <IconButton
        onClick={openMenu}
        size="small"
        aria-controls={open ? 'account-menu' : undefined}
        aria-haspopup="true"
        aria-expanded={open ? 'true' : undefined}
      >
        {isArtistLinked ? (
          <Avatar src={user?.picture} sx={{ width: 35, height: 35 }} />
        ) : (
          <Avatar
            sx={{
              bgcolor: 'primary.main',
              src: user?.picture,
              width: 35,
              height: 35,
              fontSize: 15,
            }}
          />
        )}
      </IconButton>
    </Tooltip>
  );
};

export default AccountIconButton;
