import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import CabinetSection from './CabinetSection';
import useAuthContext from '../../../hooks/context/useAuthContext';
import { useNavigate } from 'react-router-dom';
import { HOME_ROUTE } from '../../../routes/routes';

const AccountCabSection = () => {
  const { logOut } = useAuthContext();
  const navigate = useNavigate();

  const handleLogOut = () => {
    logOut();
    navigate(HOME_ROUTE);
  };

  const securityItems = [
    {
      name: 'Close Account',
      icon: <DeleteOutlineOutlinedIcon />,
      onClick: () => {},
    },
    { name: 'Log Out', icon: <ExitToAppIcon />, onClick: handleLogOut },
  ];

  return (
    <CabinetSection
      sectionName="Security and Privacy"
      sectionItems={securityItems}
    />
  );
};

export default AccountCabSection;
