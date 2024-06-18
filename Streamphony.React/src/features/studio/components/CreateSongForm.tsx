import { InferType } from 'yup';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { ACCOUNT_ROUTE, STUDIO_ROUTE } from '../../../routes/routes';
import { Alert, Avatar, Box, Button, Typography } from '@mui/material';
import LoadingSpinner from '../../../shared/LoadingSpinner';
import createSongSchema from './ValidationSchema';
import useCreateSong from '../hooks/useCreateSong';
import FormInput from '../../auth/components/FormInput';

type FormData = InferType<typeof createSongSchema>;

const CreateSongForm = () => {
  const navigate = useNavigate();
  const { mutateAsync: createSong, error, isPending } = useCreateSong();

  const {
    handleSubmit,
    control,
    formState: { errors },
    watch,
  } = useForm<FormData>({
    resolver: yupResolver(createSongSchema),
    defaultValues: {
      title: '',
      duration: '',
      url: '',
    },
  });

  const imageUrl = watch('url');

  const navigateBack = () => {
    navigate(ACCOUNT_ROUTE);
  };

  const onSubmit = async (data: FormData) => {
    try {
      const apiData = {
        title: data.title,
        duration: data.duration,
        url: data.url,
        ownerId: emptyGuid,
      };

      console.log(apiData);

      await createSong(apiData);
      navigate(STUDIO_ROUTE);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 2 }}>
        <Box sx={{ mr: 2, flex: 1, mb: 1 }}>
          <Typography fontWeight={'bold'} sx={{ mb: 2 }}>
            Url
          </Typography>
          <FormInput
            name={'url'}
            type={'url'}
            label={'Url'}
            control={control}
            errors={errors}
          />
        </Box>

        <Avatar
          variant="rounded"
          src={imageUrl || ''}
          sx={{ mt: 1, flex: 0.3, height: '130px' }}
        />
      </Box>

      <Typography fontWeight={'bold'} sx={{ mb: 2 }}>
        What's this song called?
      </Typography>
      <FormInput
        name={'title'}
        type={'text'}
        label={'Title'}
        control={control}
        errors={errors}
      />

      <Typography fontWeight={'bold'} sx={{ mb: 2 }}>
        Duration
      </Typography>
      <FormInput
        name={'duration'}
        type={'text'}
        label={'Duration'}
        control={control}
        errors={errors}
      />

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
          aria-label="Create"
          disabled={isPending}
        >
          {isPending ? <LoadingSpinner /> : 'Create'}
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

export default CreateSongForm;
