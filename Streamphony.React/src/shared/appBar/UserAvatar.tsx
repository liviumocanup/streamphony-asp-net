import React from 'react';
import AccountIconButton from './components/auth/AccountIconButton';
import UserMenu from './components/auth/UserMenu';

interface UserAvatarProps {
  menuItems?: React.ReactNode[];
}

const UserAvatar = ({ menuItems }: UserAvatarProps) => {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const openMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const closeMenu = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <AccountIconButton open={open} openMenu={openMenu} />

      <UserMenu
        anchorEl={anchorEl}
        open={open}
        closeMenu={closeMenu}
        items={menuItems}
      />
    </>
  );
};

export default UserAvatar;
