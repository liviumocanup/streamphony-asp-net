import { InferType } from 'yup';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { Alert, Box, Snackbar, Typography } from '@mui/material';
import { useState } from 'react';
import {
  BlobFile,
  BlobType,
} from '../../../shared/interfaces/EntitiesInterfaces';
import { SongDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';
import useEditSong from '../hooks/useEditSong';
import { editSongSchema } from './ValidationSchema';
import useUploadBlob from '../hooks/useUploadBlob';
import { STUDIO_ROUTE } from '../../../routes/routes';
import UploadImageBox from './upload/UploadImageBox';
import { IMAGE_CONTENT_TYPE } from '../../../shared/constants';
import ImageGuidelineText from './upload/ImageGuidelineText';
import FormInput from '../../auth/components/FormInput';
import FormNavigationButtons from './FormNavigationButtons';

type FormData = InferType<typeof editSongSchema>;

interface Props {
  song: SongDetails;
}

const EditSongForm = ({ song }: Props) => {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [coverBlob, setCoverBlob] = useState<BlobFile | null>({
    name: song.coverBlob.name,
    url: song.coverUrl,
    file: '',
    id: song.coverBlob.id,
  });
  const {
    mutateAsync: editSong,
    error: songFormError,
    isPending: isFormPending,
  } = useEditSong();
  const {
    mutateAsync: uploadImageBlob,
    error: imageError,
    isPending: isImagePending,
  } = useUploadBlob();

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
    resolver: yupResolver(editSongSchema),
    defaultValues: {
      title: song.title,
    },
  });

  const navigateBack = () => {
    navigate(STUDIO_ROUTE);
  };

  const onSubmit = async (data: FormData) => {
    try {
      const coverFileId = coverBlob?.id;

      if (!coverFileId) {
        setSnackbarMessage('Missing cover. Please upload a picture.');
        setOpen(true);
        return;
      }

      const apiData = {
        id: song.id,
        title: data.title,
        coverBlobId: coverFileId,
      };

      await editSong(apiData);
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

export default EditSongForm;
