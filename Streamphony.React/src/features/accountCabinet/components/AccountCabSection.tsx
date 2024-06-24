import PersonAddAlt1OutlinedIcon from '@mui/icons-material/PersonAddAlt1Outlined';
import EditOutlinedIcon from '@mui/icons-material/EditOutlined';
import FaceRetouchingNaturalTwoToneIcon from '@mui/icons-material/FaceRetouchingNaturalTwoTone';
import CabinetSection from './CabinetSection';
import { useNavigate } from 'react-router-dom';
import LinkOffIcon from '@mui/icons-material/LinkOff';
import {
  EDIT_ACCOUNT_ROUTE,
  EDIT_ARTIST_ROUTE,
  REGISTER_ARTIST_ROUTE,
} from '../../../routes/routes';

interface AccountCabSectionProps {
  isArtist: boolean;
}

const AccountCabSection = ({ isArtist }: AccountCabSectionProps) => {
  const navigate = useNavigate();

  const navigateToArtistEdit = () => navigate(EDIT_ARTIST_ROUTE);
  const navigateToRegister = () => navigate(REGISTER_ARTIST_ROUTE);
  const navigateToAccountEdit = () => navigate(EDIT_ACCOUNT_ROUTE);

  const artistOption = isArtist
    ? {
        name: 'Edit your Artist Details',
        icon: <FaceRetouchingNaturalTwoToneIcon />,
        onClick: navigateToArtistEdit,
      }
    : {
        name: 'Register your Artist',
        icon: <PersonAddAlt1OutlinedIcon />,
        onClick: navigateToRegister,
      };

  const unlinkArtist = isArtist
    ? {
        name: 'Unlink your Artist Account',
        icon: <LinkOffIcon />,
        onClick: () => {},
      }
    : null;

  const accountItems = [
    artistOption,
    {
      name: 'Edit Personal Details',
      icon: <EditOutlinedIcon />,
      onClick: navigateToAccountEdit,
    },
    unlinkArtist,
  ].filter((item) => item !== null);

  return <CabinetSection sectionName="Account" sectionItems={accountItems} />;
};

export default AccountCabSection;
