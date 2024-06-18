import Home from './features/home/Home';
import SignUp from './features/auth/SignUp';
import { Route, Routes } from 'react-router-dom';
import LogIn from './features/auth/LogIn';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import AccountCabinet from './features/accountCabinet/AccountCabinet';
import ProtectedRoute from './routes/ProtectedRoute';
import NotFoundPage from './routes/NotFoundPage';
import GuestRoute from './routes/GuestRoute';
import {
  ACCOUNT_ROUTE,
  ADD_SONG_ROUTE,
  HOME_ROUTE,
  LOG_IN_ROUTE,
  REGISTER_ARTIST_ROUTE,
  SIGN_UP_ROUTE,
  STUDIO_ROUTE,
} from './routes/routes';
import RegisterArtist from './features/accountCabinet/RegisterArtist';
import AuthProvider from './context/providers/AuthProvider';
import ContentStudio from './features/studio/ContentStudio';
import CreateSong from './features/studio/CreateSong';

const queryClient = new QueryClient();

const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <Routes>
          <Route
            path={SIGN_UP_ROUTE}
            element={
              <GuestRoute>
                <SignUp />
              </GuestRoute>
            }
          />
          <Route
            path={LOG_IN_ROUTE}
            element={
              <GuestRoute>
                <LogIn />
              </GuestRoute>
            }
          />
          <Route
            path={ACCOUNT_ROUTE}
            element={
              <ProtectedRoute>
                <AccountCabinet />
              </ProtectedRoute>
            }
          />
          <Route
            path={REGISTER_ARTIST_ROUTE}
            element={
              <ProtectedRoute>
                <RegisterArtist />
              </ProtectedRoute>
            }
          />
          <Route
            path={STUDIO_ROUTE}
            element={
              <ProtectedRoute>
                <ContentStudio />
              </ProtectedRoute>
            }
          />
          <Route
            path={ADD_SONG_ROUTE}
            element={
              <ProtectedRoute>
                <CreateSong />
              </ProtectedRoute>
            }
          />
          <Route path={HOME_ROUTE} element={<Home />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </AuthProvider>
    </QueryClientProvider>
  );
};

export default App;
