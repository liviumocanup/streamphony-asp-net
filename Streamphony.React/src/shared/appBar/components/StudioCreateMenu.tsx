import { Menu } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import {
  ADD_ALBUM_ROUTE,
  ADD_PLAYLIST_ROUTE,
  ADD_SONG_ROUTE,
} from '../../../routes/routes';
import MenuItemWithIcon from './MenuItemWithIcon';
import {
  Album,
  FileUploadOutlined,
  PlaylistAddOutlined,
} from '@mui/icons-material';
import { useState } from 'react';
import UploadDialog from '../../../features/studio/components/upload/UploadDialog';

interface CreateMenuProps {
  anchorEl: null | HTMLElement;
  open: boolean;
  closeMenu: () => void;
}

interface Blob {
  name: string;
  url: string;
  file: File;
}

const StudioCreateMenu = ({ anchorEl, open, closeMenu }: CreateMenuProps) => {
  const navigate = useNavigate();
  const [uploadDialogOpen, setUploadDialogOpen] = useState(false);

  const handleUploadSong = () => {
    setUploadDialogOpen(true);
  };

  const handleDialogClose = (songBlob?: Blob) => {
    setUploadDialogOpen(false);

    if (songBlob) {
      navigateToPage(ADD_SONG_ROUTE, { songBlob });
    }
  };

  const navigateToPage = (path: string, state?: any) => {
    closeMenu();
    navigate(path, { state });
  };

  const navigateToAddAlbum = () => navigateToPage(ADD_ALBUM_ROUTE);
  const navigateToAddPlaylist = () => navigateToPage(ADD_PLAYLIST_ROUTE);

  const itemList = [
    <MenuItemWithIcon
      key="song"
      text="Upload a Song"
      icon={<FileUploadOutlined fontSize="small" />}
      onClick={handleUploadSong}
    />,

    <MenuItemWithIcon
      key="album"
      text="Create an Album"
      icon={<Album fontSize="small" />}
      onClick={navigateToAddAlbum}
    />,

    <MenuItemWithIcon
      key="playlist"
      text="New playlist"
      icon={<PlaylistAddOutlined fontSize="small" />}
      onClick={navigateToAddPlaylist}
    />,
  ];

  return (
    <>
      <Menu
        anchorEl={anchorEl}
        id="create-menu"
        open={open}
        onClose={closeMenu}
        onClick={closeMenu}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
        disableScrollLock={true}
      >
        {itemList}
      </Menu>
      <UploadDialog open={uploadDialogOpen} onClose={handleDialogClose} />
    </>
  );
};

export default StudioCreateMenu;
