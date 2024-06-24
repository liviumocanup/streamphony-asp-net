import { Suspense } from 'react';
import FallbackSection from './fallback/FallbackSection';
import PopularArtists from './PopularArtists';
import PopularSongs from './PopularSongs';
import PopularAlbums from './PopularAlbums';
import { Box } from '@mui/material';

const Feed = () => {
  return (
    <Box sx={{ mb: 15 }}>
      <Suspense fallback={<FallbackSection imageVariant={'circular'} />}>
        <PopularArtists imageVariant={'circular'} />
      </Suspense>

      <Suspense fallback={<FallbackSection />}>
        <PopularSongs imageVariant={'rounded'} />
      </Suspense>

      <Suspense fallback={<FallbackSection />}>
        <PopularAlbums imageVariant={'rounded'} />
      </Suspense>
    </Box>
  );
};

export default Feed;
