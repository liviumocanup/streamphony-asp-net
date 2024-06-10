import { artists, albums, playlists } from '../../../shared/dummy_data';
import useFetchImages from '../hooks/useFetchImages';
import UserAlbums from './UserAlbums';
import { Suspense } from 'react';
import Section from './Section';
import FallbackSection from './fallback/FallbackSection';
import useAuthStatus from '../../../hooks/useAuthStatus';

const Feed = () => {
  const isLoggedIn = useAuthStatus();

  return (
    <>
      {isLoggedIn ? (
        <Suspense fallback={<FallbackSection imageVariant={'circular'} />}>
          <UserAlbums />
        </Suspense>
      ) : null}

      <Suspense fallback={<FallbackSection imageVariant={'circular'} />}>
        <PopularArtists />
      </Suspense>

      <Suspense fallback={<FallbackSection />}>
        <PopularAlbums />
      </Suspense>

      <Suspense fallback={<FallbackSection />}>
        <FeaturedPlaylists />
      </Suspense>
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
