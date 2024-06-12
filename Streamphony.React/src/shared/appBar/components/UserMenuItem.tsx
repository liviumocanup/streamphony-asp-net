import { ListItemIcon, MenuItem } from '@mui/material';
import { ReactNode } from 'react';

interface UserMenuItemProps {
  text: string;
  icon: ReactNode;
  onClick: () => void;
}

const UserMenuItem = ({ text, icon, onClick }: UserMenuItemProps) => {
  return (
    <MenuItem onClick={onClick}>
      <ListItemIcon>{icon}</ListItemIcon>
      {text}
    </MenuItem>
  );
};

export default UserMenuItem;
