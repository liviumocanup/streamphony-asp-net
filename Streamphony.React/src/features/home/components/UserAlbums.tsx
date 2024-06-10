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
