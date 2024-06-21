import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import CabinetSection from './CabinetSection';
import { useAuth0 } from '@auth0/auth0-react';

const AccountCabSection = () => {
  const { logout } = useAuth0();

  const securityItems = [
    {
      name: 'Close Account',
      icon: <DeleteOutlineOutlinedIcon />,
      onClick: () => {},
    },
    {
      name: 'Log Out',
      icon: <ExitToAppIcon />,
      onClick: () =>
        logout({ logoutParams: { returnTo: window.location.origin } }),
    },
  ];

  return (
    <CabinetSection
      sectionName="Security and Privacy"
      sectionItems={securityItems}
    />
  );
};

export default AccountCabSection;
