import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { Alert, Box, Button } from '@mui/material';
import { InferType } from 'yup';
import registerArtistSchema from './ValidationSchema';
import { useNavigate } from 'react-router-dom';
import LoadingSpinner from '../../../shared/LoadingSpinner';
import { ACCOUNT_ROUTE, HOME_ROUTE } from '../../../routes/routes';
import useRegisterArtist from '../hooks/useRegisterArtist';
import NameInput from '../../auth/components/NameInput';
import DateOfBirthInput from './DateOfBirthInput';
import ProfilePictureInput from './ProfilePictureInput';
import FormSection from './FormSection';
import { RegisterArtistData } from '../../../shared/Interfaces';
import { format } from 'date-fns';

type FormData = InferType<typeof registerArtistSchema>;

const RegisterArtistForm = () => {
  const navigate = useNavigate();
  const { mutateAsync: registerArtist, error, isPending } = useRegisterArtist();

  const {
    handleSubmit,
    setValue,
    control,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(registerArtistSchema),
    defaultValues: {
      firstName: '',
      lastName: '',
      day: '',
      month: '',
      year: '',
      profilePicture: '',
    },
  });

  const navigateBack = () => {
    navigate(ACCOUNT_ROUTE);
  };

  const onSubmit = async (data: FormData) => {
    const dateOfBirth = format(
      new Date(data.year, data.month, data.day),
      'yyyy-MM-dd',
    );

    const apiData: RegisterArtistData = {
      firstName: data.firstName,
      lastName: data.lastName,
      dateOfBirth: dateOfBirth,
      profilePictureUrl: data.profilePicture,
    };

    try {
      await registerArtist(apiData);
      navigate(HOME_ROUTE);
      console.log(data);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <FormSection
        sectionName={'Name'}
        description={'This name will appear on your Artist profile'}
      />
      <NameInput control={control} errors={errors} />

      <FormSection
        sectionName={'Date of Birth'}
        description={'You must be of legal age to register as an Artist'}
      />
      <DateOfBirthInput setValue={setValue} control={control} errors={errors} />

      <FormSection
        sectionName={'Profile Picture'}
        description={'Upload a profile picture for your Artist profile'}
      />
      <ProfilePictureInput control={control} errors={errors} />

      <Box sx={{ display: 'flex', justifyContent: 'flex-end', mt: 2, mb: 2 }}>
        <Button
          onClick={navigateBack}
          variant="outlined"
          aria-label="Cancel"
          disabled={isPending}
          sx={{ mr: 3 }}
        >
          Cancel
        </Button>

        <Button
          variant="contained"
          type="submit"
          aria-label="Register"
          disabled={isPending}
        >
          {isPending ? <LoadingSpinner /> : 'Register'}
        </Button>
      </Box>

      {error && (
        <Alert variant="outlined" severity="error">
          {error.message}
        </Alert>
      )}
    </form>
  );
};

export default RegisterArtistForm;
