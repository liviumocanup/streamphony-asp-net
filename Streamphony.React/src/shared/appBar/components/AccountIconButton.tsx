import { Avatar, IconButton, Tooltip } from '@mui/material';
import React from 'react';

interface AccountIconButtonProps {
  open: boolean;
  openMenu: (event: React.MouseEvent<HTMLElement>) => void;
}

const AccountIconButton = ({ open, openMenu }: AccountIconButtonProps) => {
  return (
    <Tooltip title="Account Settings">
      <IconButton
        onClick={openMenu}
        size="small"
        aria-controls={open ? 'account-menu' : undefined}
        aria-haspopup="true"
        aria-expanded={open ? 'true' : undefined}
      >
        <Avatar sx={{ width: 35, height: 35 }}>U</Avatar>
      </IconButton>
    </Tooltip>
  );
};

export default AccountIconButton;
