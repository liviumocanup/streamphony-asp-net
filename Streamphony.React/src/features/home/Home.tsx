import { lazy, Suspense, useState } from 'react';
import Box from '@mui/material/Box';
import AppBar from '../../shared/appBar/AppBar';
import PersistentDrawer from '../../shared/drawer/PersistentDrawer';
import { Main } from '../../shared/drawer/styles/MainStyle';
import { DrawerHeader } from '../../shared/drawer/styles/DrawerHeaderStyle';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';
import AudioPlayer from '../../shared/audioPlayer/AudioPlayer';
import FallbackFeed from './components/fallback/FallbackFeed';
import Feed from './components/Feed';

const Home = () => {
  // const Feed = lazy(() => import('./components/Feed'));

  return (
    <>
      <Helmet>
        <title>{APP_TITLE}</title>
        <meta
          name="description"
          content="Home page of the music streaming app"
        />
      </Helmet>

      <AppBarWrapper
        children={
          <>
            <Suspense fallback={<FallbackFeed />}>
              <Feed />
            </Suspense>

            <AudioPlayer />
          </>
        }
      />
    </>
  );
};

export default Home;
