import { artists, albums, playlists } from '../../../shared/dummy_data';
import useFetchImages from '../hooks/useFetchImages';
import { Suspense } from 'react';
import Section from './Section';
import FallbackSection from './fallback/FallbackSection';
import UserSongs from './UserSongs';
import FallbackFeed from './fallback/FallbackFeed';
import { useAuth0 } from '@auth0/auth0-react';
import useGetAlbums from '../hooks/useGetAlbums';

const Feed = () => {
  const { user, isAuthenticated, isLoading } = useAuth0();

  if (isLoading) return <FallbackFeed />;

  return (
    <>
      {isAuthenticated ? (
        <Suspense fallback={<FallbackSection imageVariant={'circular'} />}>
          <UserSongs user={user} />
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
