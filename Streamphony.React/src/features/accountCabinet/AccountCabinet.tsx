import { Box, Toolbar } from '@mui/material';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import AccountCabSection from './components/AccountCabSection';
import SecurityCabSection from './components/SecurityCabSection';
import AppBarWrapper from './AppBarWrapper';
import LoadingSpinner from '../../shared/LoadingSpinner';
import useGetUserDetails from '../../hooks/useGetUserDetails';
import useTokenStorage from '../../hooks/localStorage/useTokenStorage';

const AccountCabinet = () => {
  const { data: userDetails, isPending } = useGetUserDetails();
  const { getUserClaims } = useTokenStorage();
  const { firstName, lastName } = getUserClaims();

  if (isPending) {
    return <LoadingSpinner />;
  }

  return (
    <>
      <Helmet>
        <title>{`${firstName} ${lastName} - ${APP_TITLE}`}</title>
        <meta name="description" content="Your account settings" />
      </Helmet>

      <AppBarWrapper />

      <Box sx={{ flexGrow: 1, mt: 8 }} className="WidthCentered">
        <Toolbar />

        <AccountCabSection isArtist={userDetails!.isArtistLinked} />

        <SecurityCabSection />
      </Box>
    </>
  );
};

export default AccountCabinet;
