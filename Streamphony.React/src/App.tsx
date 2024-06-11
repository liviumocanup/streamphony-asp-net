import Home from './features/home/Home';
import SignUp from './features/auth/SignUp';
import { Route, Routes } from 'react-router-dom';
import LogIn from './features/auth/LogIn';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import AccountCabinet from './features/auth/AccountCabinet';
import ProtectedRoute from './routes/ProtectedRoute';
import NotFoundPage from './routes/NotFoundPage';
import GuestRoute from './routes/GuestRoute';

const queryClient = new QueryClient();

const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Routes>
        <Route
          path="/signUp"
          element={
            <GuestRoute>
              <SignUp />
            </GuestRoute>
          }
        />
        <Route
          path="/logIn"
          element={
            <GuestRoute>
              <LogIn />
            </GuestRoute>
          }
        />
        <Route
          path="/account"
          element={
            <ProtectedRoute>
              <AccountCabinet />
            </ProtectedRoute>
          }
        />
        <Route path="/" element={<Home />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </QueryClientProvider>
  );
};

export default App;
