import { Menu } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import {
  ADD_ALBUM_ROUTE,
  ADD_PLAYLIST_ROUTE,
  ADD_SONG_ROUTE,
} from '../../../routes/routes';
import BarMenuItem from './BarMenuItem';
import {
  Album,
  FileUploadOutlined,
  PlaylistAddOutlined,
} from '@mui/icons-material';

interface CreateMenuProps {
  anchorEl: null | HTMLElement;
  open: boolean;
  closeMenu: () => void;
}

const CreateMenu = ({ anchorEl, open, closeMenu }: CreateMenuProps) => {
  const navigate = useNavigate();

  const navigateToPage = (path: string) => {
    closeMenu();
    navigate(path);
  };

  const navigateToAddSong = () => navigateToPage(ADD_SONG_ROUTE);
  const navigateToAddAlbum = () => navigateToPage(ADD_ALBUM_ROUTE);
  const navigateToAddPlaylist = () => navigateToPage(ADD_PLAYLIST_ROUTE);

  const itemList = [
    <BarMenuItem
      key="song"
      text="Upload a Song"
      icon={<FileUploadOutlined fontSize="small" />}
      onClick={navigateToAddSong}
    />,

    <BarMenuItem
      key="album"
      text="Create an Album"
      icon={<Album fontSize="small" />}
      onClick={navigateToAddAlbum}
    />,

    <BarMenuItem
      key="playlist"
      text="New playlist"
      icon={<PlaylistAddOutlined fontSize="small" />}
      onClick={navigateToAddPlaylist}
    />,
  ];

  return (
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
  );
};

export default CreateMenu;
