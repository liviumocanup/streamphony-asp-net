import { Box, Toolbar } from '@mui/material';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import AccountCabSection from './components/AccountCabSection';
import SecurityCabSection from './components/SecurityCabSection';
import AppBarWrapper from './AppBarWrapper';
import LoadingSpinner from '../../shared/LoadingSpinner';
import useTokenStorage from '../../hooks/localStorage/useTokenStorage';
import useArtistContext from '../../hooks/context/useArtistContext';

const AccountCabinet = () => {
  const { isArtistLinked } = useArtistContext();
  const { getUserClaims } = useTokenStorage();
  const { firstName, lastName } = getUserClaims();

  return (
    <>
      <Helmet>
        <title>{`${firstName} ${lastName} - ${APP_TITLE}`}</title>
        <meta name="description" content="Your account settings" />
      </Helmet>

      <AppBarWrapper
        children={
          <Box className="CenteredContainer">
            <Toolbar />

            <AccountCabSection isArtist={isArtistLinked} />

            <SecurityCabSection />
          </Box>
        }
      />
    </>
  );
};

export default AccountCabinet;
