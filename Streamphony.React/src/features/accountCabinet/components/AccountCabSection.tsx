import PersonAddAlt1OutlinedIcon from '@mui/icons-material/PersonAddAlt1Outlined';
import EditOutlinedIcon from '@mui/icons-material/EditOutlined';
import CabinetSection from './CabinetSection';
import { useNavigate } from 'react-router-dom';
import {
  EDIT_PROFILE_ROUTE,
  REGISTER_ARTIST_ROUTE,
} from '../../../routes/routes';

const AccountCabSection = () => {
  const navigate = useNavigate();

  const navigateToRegister = () => navigate(REGISTER_ARTIST_ROUTE);
  const navigateToEdit = () => navigate(EDIT_PROFILE_ROUTE);

  const accountItems = [
    {
      name: 'Register your Artist',
      icon: <PersonAddAlt1OutlinedIcon />,
      onClick: navigateToRegister,
    },
    {
      name: 'Edit Profile',
      icon: <EditOutlinedIcon />,
      onClick: navigateToEdit,
    },
  ];

  return <CabinetSection sectionName="Account" sectionItems={accountItems} />;
};

export default AccountCabSection;
