import { artists, albums, playlists } from '../../../shared/dummy_data';
import useFetchImages from '../hooks/useFetchImages';
import { Suspense } from 'react';
import Section from './Section';
import FallbackSection from './fallback/FallbackSection';
import useAuthContext from '../../../hooks/context/useAuthContext';
import UserSongs from './UserSongs';
import AudioPlayer from '../../../shared/audioPlayer/AudioPlayer';

interface FeedProps {
  open: boolean;
  drawerWidth: number;
}

const Feed = ({ open, drawerWidth }: FeedProps) => {
  const { isLoggedIn } = useAuthContext();

  return (
    <>
      {isLoggedIn ? (
        <Suspense fallback={<FallbackSection imageVariant={'circular'} />}>
          <UserSongs />
        </Suspense>
      ) : null}

      {/*<Suspense fallback={<FallbackSection imageVariant={'circular'} />}>*/}
      {/*  <PopularArtists />*/}
      {/*</Suspense>*/}

      {/*<Suspense fallback={<FallbackSection />}>*/}
      {/*  <PopularAlbums />*/}
      {/*</Suspense>*/}

      {/*<Suspense fallback={<FallbackSection />}>*/}
      {/*  <FeaturedPlaylists />*/}
      {/*</Suspense>*/}

      <AudioPlayer
        url="http://127.0.0.1:10000/devstoreaccount1/draft/songs/0fce21a9-d4d8-482d-87ef-261b8076f221"
        title="Song Title"
        artist="Artist Name"
        isDrawerOpen={open}
        drawerWidth={drawerWidth}
      />
    </>
  );
};

const PopularArtists = () => {
  const { imageUrls: artistImages } = useFetchImages(artists);

  return (
    <Section
      items={artists}
      imageUrls={artistImages}
      sectionTitle="Popular artists"
      imageVariant="circular"
    />
  );
};

const PopularAlbums = () => {
  const { imageUrls: albumImages } = useFetchImages(albums);

  return (
    <Section
      items={albums}
      imageUrls={albumImages}
      sectionTitle="Popular albums"
    />
  );
};

const FeaturedPlaylists = () => {
  const { imageUrls: playlistImages } = useFetchImages(playlists);

  return (
    <Section
      items={playlists}
      imageUrls={playlistImages}
      sectionTitle="Featured playlists"
    />
  );
};

export default Feed;
