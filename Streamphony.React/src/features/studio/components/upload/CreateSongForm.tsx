import { InferType } from 'yup';
import { useLocation, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { ACCOUNT_ROUTE, STUDIO_ROUTE } from '../../../../routes/routes';
import { Alert, Box, Snackbar, Typography } from '@mui/material';
import { createSongSchema } from '../ValidationSchema';
import useCreateSong from '../../hooks/useCreateSong';
import FormInput from '../../../auth/components/FormInput';
import ImageGuidelineText from './ImageGuidelineText';
import {
  AUDIO_CONTENT_TYPE,
  IMAGE_CONTENT_TYPE,
} from '../../../../shared/constants';
import UploadImageBox from './UploadImageBox';
import UploadBlobButton from './UploadBlobButton';
import { useEffect, useState } from 'react';
import useUploadBlob from '../../hooks/useUploadBlob';
import {
  BlobFile,
  BlobType,
} from '../../../../shared/interfaces/EntitiesInterfaces';
import FormNavigationButtons from '../FormNavigationButtons';
import GenreSelect from '../GenreSelect';
import { SongDetails } from '../../../../shared/interfaces/EntityDetailsInterfaces';

type FormData = InferType<typeof createSongSchema>;

const CreateSongForm = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const [genre, setGenre] = useState<SongDetails[]>([]);
  const [open, setOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [songBlob, setSongBlob] = useState<BlobFile | null>(
    location.state?.songBlob,
  );
  const [coverBlob, setCoverBlob] = useState<BlobFile | null>(null);
  const {
    mutateAsync: createSong,
    error: songFormError,
    isPending: isFormPending,
  } = useCreateSong();
  const {
    mutateAsync: uploadSongBlob,
    error: songError,
    isPending: isSongPending,
  } = useUploadBlob();
  const {
    mutateAsync: uploadImageBlob,
    error: imageError,
    isPending: isImagePending,
  } = useUploadBlob();

  const handleSongFileChange = async (blob: BlobFile) => {
    const response = await uploadSongBlob({
      file: blob.file,
      blobType: BlobType.Song,
    });

    if (response) {
      blob.url = response.url;
      blob.id = response.id;

      setSongBlob(blob);
    }
  };

  const handleCoverFileChange = async (blob: BlobFile) => {
    const response = await uploadImageBlob({
      file: blob.file,
      blobType: BlobType.SongCover,
    });

    if (response) {
      blob.url = response.url;
      blob.id = response.id;

      setCoverBlob(blob);
    }
  };

  useEffect(() => {
    handleSongFileChange(location.state?.songBlob);
  }, []);

  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }
    setOpen(false);
  };

  const {
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(createSongSchema),
    defaultValues: {
      title: songBlob!.name,
      genre: '',
    },
  });

  const navigateBack = () => {
    navigate(ACCOUNT_ROUTE);
  };

  const onSubmit = async (data: FormData) => {
    try {
      const coverFileId = coverBlob?.id;
      const songFileId = songBlob?.id;

      if (!coverFileId || !songFileId) {
        setSnackbarMessage('Missing cover. Please upload a picture.');
        setOpen(true);
        return;
      }

      const apiGenre = genre[0];

      if (!apiGenre) {
        const apiData = {
          title: data.title,
          coverFileId: coverFileId,
          audioFileId: songFileId,
        };

        await createSong(apiData);
        navigate(STUDIO_ROUTE);
        return;
      }

      const apiData = {
        title: data.title,
        coverFileId: coverFileId,
        audioFileId: songFileId,
        genreId: genre[0].id,
      };

      await createSong(apiData);
      navigate(STUDIO_ROUTE);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
        }}
      >
        <Box sx={{ display: 'flex', flexDirection: 'row' }}>
          <UploadImageBox
            contentType={IMAGE_CONTENT_TYPE}
            onFileChange={handleCoverFileChange}
            isPending={isImagePending}
          />

          <ImageGuidelineText
            isPending={isImagePending}
            error={imageError}
            blob={coverBlob}
          />
        </Box>

        <UploadBlobButton
          contentType={AUDIO_CONTENT_TYPE}
          initialBlob={songBlob}
          onFileChange={handleSongFileChange}
          error={songError}
          isPending={isSongPending}
        />
      </Box>

      <Typography fontWeight={'bold'} sx={{ mb: 1.5 }}>
        What's this song called?
      </Typography>
      <FormInput
        name={'title'}
        type={'text'}
        control={control}
        errors={errors}
      />

      <Typography fontWeight={'bold'} sx={{ mb: 1.5 }}>
        Does it have a Genre?
      </Typography>
      <GenreSelect selectedGenre={genre} setSelectedGenre={setGenre} />

      <FormNavigationButtons
        isPending={isFormPending || isImagePending}
        navigateBack={navigateBack}
      />

      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert onClose={handleClose} severity="error" sx={{ width: '100%' }}>
          {snackbarMessage}
        </Alert>
      </Snackbar>

      {songFormError && (
        <Alert variant="outlined" severity="error">
          {songFormError.message}
        </Alert>
      )}
    </form>
  );
};

export default CreateSongForm;
