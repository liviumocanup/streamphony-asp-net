import { Suspense } from 'react';
import FallbackSection from './fallback/FallbackSection';
import useAuthContext from '../../../hooks/context/useAuthContext';
import UserSongs from './UserSongs';
import PopularArtists from './PopularArtists';
import PopularSongs from './PopularSongs';
import PopularAlbums from './PopularAlbums';
import { Box } from '@mui/material';

const Feed = () => {
  const { isLoggedIn } = useAuthContext();

  return (
    <Box sx={{ mb: 15 }}>
      {isLoggedIn ? (
        <Suspense fallback={<FallbackSection imageVariant={'circular'} />}>
          <UserSongs />
        </Suspense>
      ) : null}

      <Suspense fallback={<FallbackSection imageVariant={'circular'} />}>
        <PopularArtists imageVariant={'rounded'} />
      </Suspense>

      <Suspense fallback={<FallbackSection />}>
        <PopularSongs imageVariant={'circular'} />
      </Suspense>

      <Suspense fallback={<FallbackSection />}>
        <PopularAlbums imageVariant={'rounded'} />
      </Suspense>

      {/*<Suspense fallback={<FallbackSection />}>*/}
      {/*  <FeaturedPlaylists />*/}
      {/*</Suspense>*/}
    </Box>
  );
};

// const PopularArtists = () => {
//   const { imageUrls: artistImages } = useFetchImages(artists);
//
//   return (
//     <Section
//       items={artists}
//       imageUrls={artistImages}
//       sectionTitle="Popular artists"
//       imageVariant="circular"
//     />
//   );
// };

// const PopularAlbums = () => {
//   const { imageUrls: albumImages } = useFetchImages(albums);
//
//   return (
//     <Section
//       items={albums}
//       imageUrls={albumImages}
//       sectionTitle="Popular albums"
//     />
//   );
// };
//
// const FeaturedPlaylists = () => {
//   const { imageUrls: playlistImages } = useFetchImages(playlists);
//
//   return (
//     <Section
//       items={playlists}
//       imageUrls={playlistImages}
//       sectionTitle="Featured playlists"
//     />
//   );
// };

export default Feed;
