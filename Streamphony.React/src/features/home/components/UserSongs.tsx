import Section from './Section';
import useGetCurrentArtistSongs from '../../studio/hooks/useGetCurrentArtistSongs';
import { ItemType } from '../../../shared/interfaces/Interfaces';
import { SongDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';

const UserSongs = () => {
  const {
    data: songs,
    isPending,
    isLoading,
    isError,
  } = useGetCurrentArtistSongs();
  const items =
    songs?.map((song: SongDetails) => ({
      name: song.title,
      coverUrl: song.coverUrl,
      resourceUrl: song.audioUrl,
      description: 'Artist',
      resource: song,
      resourceType: ItemType.SONG,
    })) || [];

  if (isPending && isLoading) return <div>Loading...</div>;

  if (isError) return <div>Error...</div>;

  return (
    <Section items={items} sectionTitle="Your Songs" imageVariant="circular" />
  );
};

export default UserSongs;
