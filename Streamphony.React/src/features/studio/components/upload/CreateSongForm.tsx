import { InferType } from 'yup';
import { useLocation, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { ACCOUNT_ROUTE, STUDIO_ROUTE } from '../../../../routes/routes';
import { Alert, Box, Button, Typography } from '@mui/material';
import LoadingSpinner from '../../../../shared/LoadingSpinner';
import createSongSchema from '../ValidationSchema';
import useCreateSong from '../../hooks/useCreateSong';
import FormInput from '../../../auth/components/FormInput';
import ImageGuidelineText from './ImageGuidelineText';
import {
  AUDIO_CONTENT_TYPE,
  BlobType,
  IMAGE_CONTENT_TYPE,
} from '../../../../shared/constants';
import UploadImageBox from './UploadImageBox';
import UploadBlobButton from './UploadBlobButton';
import { useEffect, useState } from 'react';
import useUploadBlob from '../../hooks/useUploadBlob';
import { BlobFile } from '../../../../shared/Interfaces';

type FormData = InferType<typeof createSongSchema>;

const CreateSongForm = () => {
  const navigate = useNavigate();
  const location = useLocation();
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

  const {
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(createSongSchema),
    defaultValues: {
      title: songBlob!.name,
      genre: '',
      album: '',
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
        console.log('Missing cover or song file');
        return;
      }

      const apiData = {
        title: data.title,
        coverFileId: coverFileId,
        audioFileId: songFileId,
        genre: data.genre,
        album: data.album,
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
        And what Genre does it have?
      </Typography>
      <FormInput
        name={'genre'}
        type={'text'}
        label={'Genre'}
        control={control}
        errors={errors}
      />

      <Typography fontWeight={'bold'} sx={{ mb: 1.5 }}>
        Is it part of an Album?
      </Typography>
      <FormInput
        name={'album'}
        type={'text'}
        label={'Album'}
        control={control}
        errors={errors}
      />

      <Box sx={{ display: 'flex', justifyContent: 'flex-end', mt: 2, mb: 2 }}>
        <Button
          onClick={navigateBack}
          variant="outlined"
          aria-label="Cancel"
          disabled={isFormPending}
          sx={{ mr: 3 }}
        >
          Cancel
        </Button>

        <Button
          variant="contained"
          type="submit"
          aria-label="Create"
          disabled={isFormPending}
        >
          {isFormPending || isSongPending || isImagePending ? (
            <LoadingSpinner />
          ) : (
            'Create'
          )}
        </Button>
      </Box>

      {songFormError && (
        <Alert variant="outlined" severity="error">
          {songFormError.message}
        </Alert>
      )}
    </form>
  );
};

export default CreateSongForm;
