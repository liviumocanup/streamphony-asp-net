import useGetAlbums from '../hooks/useGetAlbums';
import Section from './Section';
import { Album, Item, UrlArray } from '../../../shared/Interfaces';

const UserAlbums = () => {
  const { data: albums } = useGetAlbums();
  const imageUrls: UrlArray = albums.reduce(
    (acc: { [key: string]: string }, album: Album) => {
      acc[album.title] = album.coverImageUrl;
      return acc;
    },
    {} as UrlArray,
  );

  const albumsToItems = (albums: Album[]): Item[] => {
    return albums.map((album: Album) => ({
      name: album.title,
      description: album.releaseDate,
    }));
  };

  return (
    <Section
      items={albumsToItems(albums)}
      imageUrls={imageUrls}
      sectionTitle="Your Albums"
      imageVariant="circular"
    />
  );
};

export default UserAlbums;

/*
import Section from './Section';
import useGetCurrentUserSongs from '../../studio/hooks/useGetSongs';

const UserSongs = () => {
  const { data: songs, isPending, isError } = useGetCurrentUserSongs();
  const items = songs?.map((song) => ({
    name: song.title,
    url: song.url,
    description: song.title,
  }));

  if (isPending) return <div>Loading...</div>;

  if (isError) return <div>Error...</div>;

  return (
    <Section items={items} sectionTitle="Your Albums" imageVariant="circular" />
  );
};

export default UserSongs;

 */
