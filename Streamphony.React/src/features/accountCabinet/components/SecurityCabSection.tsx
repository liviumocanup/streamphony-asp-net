import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import CabinetSection from './CabinetSection';
import useAuthContext from '../../../hooks/context/useAuthContext';

const AccountCabSection = () => {
  const { handleLogOut } = useAuthContext();

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
