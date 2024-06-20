import React from 'react';
import StudioCreateIconButton from './components/StudioCreateIconButton';
import StudioCreateMenu from './components/StudioCreateMenu';

const StudioCreateButton = () => {
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
      <StudioCreateIconButton open={open} openMenu={openMenu} />

      <StudioCreateMenu anchorEl={anchorEl} open={open} closeMenu={closeMenu} />
    </>
  );
};

export default StudioCreateButton;
