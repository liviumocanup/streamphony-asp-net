import React, { useState } from 'react';
import { IconButton, ListItemIcon, Menu, MenuItem } from '@mui/material';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import DeleteOutlineRoundedIcon from '@mui/icons-material/DeleteOutlineRounded';
import ModeEditOutlinedIcon from '@mui/icons-material/ModeEditOutlined';
import IosShareOutlinedIcon from '@mui/icons-material/IosShareOutlined';
import { ItemType } from '../../../shared/interfaces/Interfaces';
import useDelete from '../../viewDetails/hooks/useDelete';
import LoadingSpinner from '../../../shared/LoadingSpinner';
import { useNavigate } from 'react-router-dom';

interface Props {
  itemId: string;
  itemType: ItemType;
}

const MoreOptionsButton = ({ itemId, itemType }: Props) => {
  const { mutate: deleteEntity, isPending, isError, isSuccess } = useDelete();
  const [menuOpen, setMenuOpen] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const navigate = useNavigate();

  if (isSuccess) window.location.reload();

  const endpoint =
    itemType === ItemType.SONG
      ? 'songs'
      : itemType === ItemType.ALBUM
        ? 'albums'
        : 'artists';

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setMenuOpen(true);
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setMenuOpen(false);
    setAnchorEl(null);
  };

  const handleEdit = () => {
    navigate(`/edit/${endpoint}/${itemId}`);
    handleClose();
  };

  const handleDelete = () => {
    deleteEntity({ endpoint, entityId: itemId });
    handleClose();
  };

  return (
    <>
      <IconButton
        onClick={handleClick}
        size="small"
        sx={{ position: 'absolute', right: 0, top: -8 }}
      >
        {isPending ? (
          <LoadingSpinner />
        ) : (
          <MoreHorizIcon sx={{ color: 'action.active' }} />
        )}
      </IconButton>
      <Menu
        anchorEl={anchorEl}
        open={menuOpen}
        onClose={handleClose}
        anchorOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        transformOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
      >
        <MenuItem onClick={handleEdit}>
          <ListItemIcon>
            <IosShareOutlinedIcon />
          </ListItemIcon>
          Share
        </MenuItem>

        <MenuItem onClick={handleEdit}>
          <ListItemIcon>
            <ModeEditOutlinedIcon />
          </ListItemIcon>
          Edit
        </MenuItem>

        <MenuItem onClick={handleDelete}>
          <ListItemIcon>
            <DeleteOutlineRoundedIcon />
          </ListItemIcon>
          Delete
        </MenuItem>
      </Menu>
    </>
  );
};

export default MoreOptionsButton;
