import { Button, IconButton, Tooltip } from '@mui/material';
import CloudUploadOutlinedIcon from '@mui/icons-material/CloudUploadOutlined';
import React from 'react';

interface CreateIconButtonProps {
  open: boolean;
  openMenu: (event: React.MouseEvent<HTMLElement>) => void;
}

const CreateIconButton = ({ open, openMenu }: CreateIconButtonProps) => {
  return (
    <Tooltip title="Create Menu" sx={{ mr: 2 }}>
      <Button
        component="label"
        variant="outlined"
        color="primary"
        startIcon={<CloudUploadOutlinedIcon />}
        onClick={openMenu}
        aria-controls={open ? 'create-menu' : undefined}
        aria-haspopup="true"
        aria-expanded={open ? 'true' : undefined}
      >
        Create
      </Button>
    </Tooltip>
  );
};

export default CreateIconButton;
