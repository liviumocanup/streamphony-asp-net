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
import FormSection from './FormSection';
import { BlobFile, RegisterArtistData } from '../../../shared/Interfaces';
import { format } from 'date-fns';
import UploadImageBox from '../../studio/components/upload/UploadImageBox';
import { BlobType, IMAGE_CONTENT_TYPE } from '../../../shared/constants';
import ImageGuidelineText from '../../studio/components/upload/ImageGuidelineText';
import { useState } from 'react';
import useUploadBlob from '../../studio/hooks/useUploadBlob';

type FormData = InferType<typeof registerArtistSchema>;

const RegisterArtistForm = () => {
  const navigate = useNavigate();
  const { mutateAsync: registerArtist, error, isPending } = useRegisterArtist();
  const [pfpBlob, setPfpBlob] = useState<BlobFile | null>(null);
  const {
    mutateAsync: uploadImageBlob,
    error: imageError,
    isPending: isImagePending,
  } = useUploadBlob();

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
    },
  });

  const handlePfpFileChange = async (blob: BlobFile) => {
    const response = await uploadImageBlob({
      file: blob.file,
      blobType: BlobType.ProfilePicture,
    });

    if (response) {
      blob.url = response.url;
      blob.id = response.id;

      setPfpBlob(blob);
    }
  };

  const navigateBack = () => {
    navigate(ACCOUNT_ROUTE);
  };

  const onSubmit = async (data: FormData) => {
    const pfpBlobId = pfpBlob?.id;
    if (!pfpBlobId) {
      console.log('Profile Picture is required');
      return;
    }

    const dateOfBirth = format(
      new Date(data.year, data.month, data.day),
      'yyyy-MM-dd',
    );

    const apiData: RegisterArtistData = {
      firstName: data.firstName,
      lastName: data.lastName,
      dateOfBirth: dateOfBirth,
      profilePictureId: pfpBlobId,
    };

    console.log('API DATA: ', apiData);

    try {
      await registerArtist(apiData);
      navigate(HOME_ROUTE);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <Box sx={{ display: 'flex', flexDirection: 'row' }}>
        <UploadImageBox
          contentType={IMAGE_CONTENT_TYPE}
          onFileChange={handlePfpFileChange}
          isPending={isImagePending}
          avatarVariant="circular"
        />

        <ImageGuidelineText
          isPending={isImagePending}
          error={imageError}
          blob={pfpBlob}
          sx={{ bgcolor: 'background.default' }}
        />
      </Box>

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
          {isPending || isImagePending ? <LoadingSpinner /> : 'Register'}
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
