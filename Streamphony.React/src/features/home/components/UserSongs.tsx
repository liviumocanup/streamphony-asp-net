import Section from './Section';
import useGetCurrentUserSongs from '../../studio/hooks/useGetSongs';

const UserSongs = ({ user }) => {
  const { data: songs, isPending, isError } = useGetCurrentUserSongs();
  const items =
    songs?.map((song) => ({
      name: song.title,
      coverUrl: song.coverUrl,
      resourceUrl: song.audioUrl,
      description: 'Artist',
      resource: song,
    })) || [];

  console.log('SENDING REQUEST');

  // if (isPending) return <div>Loading...</div>;

  if (isError) return <div>Error...</div>;

  return (
    <>
      {'Hello'}
      <Section
        items={items}
        sectionTitle="Your Songs"
        imageVariant="circular"
      />
    </>
  );
};

export default UserSongs;
