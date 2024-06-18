import React from 'react';
import CreateIconButton from './components/CreateIconButton';
import CreateMenu from './components/CreateMenu';

const CreateNew = () => {
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
      <CreateIconButton open={open} openMenu={openMenu} />

      <CreateMenu anchorEl={anchorEl} open={open} closeMenu={closeMenu} />
    </>
  );
};

export default CreateNew;
