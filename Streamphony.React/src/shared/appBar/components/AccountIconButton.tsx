import { Avatar, IconButton, Tooltip } from '@mui/material';
import React from 'react';
import useArtistContext from '../../../hooks/context/useArtistContext';
import useTokenStorage from '../../../hooks/localStorage/useTokenStorage';

interface AccountIconButtonProps {
  open: boolean;
  openMenu: (event: React.MouseEvent<HTMLElement>) => void;
}

const AccountIconButton = ({ open, openMenu }: AccountIconButtonProps) => {
  const { isArtistLinked, pfpUrl } = useArtistContext();
  const { getUserClaims } = useTokenStorage();
  const { firstName, lastName } = getUserClaims();

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
          <Avatar src={pfpUrl} sx={{ width: 35, height: 35 }} />
        ) : (
          <Avatar
            sx={{
              bgcolor: 'primary.main',
              width: 35,
              height: 35,
              fontSize: 15,
            }}
          >
            {firstName[0] + lastName[0]}
          </Avatar>
        )}
      </IconButton>
    </Tooltip>
  );
};

export default AccountIconButton;
