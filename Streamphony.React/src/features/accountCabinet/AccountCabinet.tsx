import { Box, Toolbar } from '@mui/material';
import '../../App.css';
import { Helmet } from 'react-helmet-async';
import { APP_TITLE } from '../../shared/constants';
import AccountCabSection from './components/AccountCabSection';
import SecurityCabSection from './components/SecurityCabSection';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';
import useTokenStorage from '../../hooks/localStorage/useTokenStorage';
import useAuthContext from '../../hooks/context/useAuthContext';

const AccountCabinet = () => {
  const { isArtist } = useAuthContext();
  const { getTokenClaims } = useTokenStorage();

  const tokenClaims = getTokenClaims();
  const firstName = tokenClaims?.firstName || '';
  const lastName = tokenClaims?.lastName || '';

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

            <AccountCabSection isArtist={isArtist} />

            <SecurityCabSection />
          </Box>
        }
      />
    </>
  );
};

export default AccountCabinet;
