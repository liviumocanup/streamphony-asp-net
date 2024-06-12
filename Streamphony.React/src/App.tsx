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
  HOME_ROUTE,
  LOG_IN_ROUTE,
  SIGN_UP_ROUTE,
} from './routes/routes';

const queryClient = new QueryClient();

const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
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
        <Route path={HOME_ROUTE} element={<Home />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </QueryClientProvider>
  );
};

export default App;
