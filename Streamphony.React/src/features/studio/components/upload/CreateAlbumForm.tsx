import { InferType } from 'yup';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { ACCOUNT_ROUTE, STUDIO_ROUTE } from '../../../../routes/routes';
import { Alert, Box, Snackbar, Typography } from '@mui/material';
import { createAlbumSchema } from '../ValidationSchema';
import useCreateAlbum from '../../hooks/useCreateAlbum';
import FormInput from '../../../auth/components/FormInput';
import ImageGuidelineText from './ImageGuidelineText';
import { IMAGE_CONTENT_TYPE } from '../../../../shared/constants';
import UploadImageBox from './UploadImageBox';
import { useState } from 'react';
import useUploadBlob from '../../hooks/useUploadBlob';
import ArtistSelectInput from '../ArtistSelectInput';
import FormNavigationButtons from '../FormNavigationButtons';
import AlbumPreview from '../AlbumPreview';
import SongSelect from '../SongSelect';
import { format } from 'date-fns';
import {
  BlobFile,
  BlobType,
} from '../../../../shared/interfaces/EntitiesInterfaces';
import {
  ArtistCollaborators,
  SongDetails,
} from '../../../../shared/interfaces/EntityDetailsInterfaces';

type FormData = InferType<typeof createAlbumSchema>;

const CreateAlbumForm = () => {
  const navigate = useNavigate();
  const [selectedArtists, setSelectedArtists] = useState<ArtistCollaborators[]>(
    [],
  );
  const [open, setOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [albumSongs, setAlbumSongs] = useState<SongDetails[]>([]);
  const [coverBlob, setCoverBlob] = useState<BlobFile | null>(null);
  const {
    mutateAsync: createAlbum,
    error: albumFormError,
    isPending: isFormPending,
  } = useCreateAlbum();
  const {
    mutateAsync: uploadImageBlob,
    error: imageError,
    isPending: isImagePending,
  } = useUploadBlob();

  const handleCoverFileChange = async (blob: BlobFile) => {
    const response = await uploadImageBlob({
      file: blob.file,
      blobType: BlobType.AlbumCover,
    });

    if (response) {
      blob.url = response.url;
      blob.id = response.id;

      setCoverBlob(blob);
    }
  };

  const {
    handleSubmit,
    control,
    formState: { errors },
    watch,
  } = useForm<FormData>({
    resolver: yupResolver(createAlbumSchema),
    defaultValues: {
      title: '',
      releaseDate: '',
    },
  });

  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }
    setOpen(false);
  };

  const onSubmit = async (data: FormData) => {
    try {
      const coverFileId = coverBlob?.id;

      if (!coverFileId) {
        setSnackbarMessage('Missing cover. Please upload a picture.');
        setOpen(true);
        return;
      }

      const songs = albumSongs.map((song) => song.id);

      if (songs.length === 0) {
        setSnackbarMessage('Please add at least one song to the album.');
        setOpen(true);
        return;
      }

      const roleMapping = {
        Performer: 0,
        Writer: 1,
        Producer: 2,
      };

      const collaborators = selectedArtists.reduce(
        (acc, artistCollaborator) => {
          acc[artistCollaborator.artist.id] = artistCollaborator.roles.map(
            (role) => roleMapping[role],
          );
          return acc;
        },
        {},
      );

      const apiData = {
        title: data.title,
        releaseDate: format(new Date(), 'yyyy-MM-dd'),
        coverFileId: coverFileId,
        songIds: songs,
        collaborators: collaborators,
      };

      console.log(apiData);

      await createAlbum(apiData);
      navigate(STUDIO_ROUTE);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
      <Box sx={{ width: '50%' }}>
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
            What's this album called?
          </Typography>
          <FormInput
            name={'title'}
            label={'Title'}
            type={'text'}
            control={control}
            errors={errors}
          />

          <Typography fontWeight={'bold'} sx={{ mb: 1.5 }}>
            Add songs to this album.
          </Typography>
          <SongSelect
            selectedSongs={albumSongs}
            setSelectedSongs={setAlbumSongs}
          />

          <Typography fontWeight={'bold'} sx={{ mb: 1.5, mt: 2 }}>
            Add artists who collaborated on this release.
          </Typography>
          <ArtistSelectInput
            selectedArtists={selectedArtists}
            setSelectedArtists={setSelectedArtists}
          />

          <FormNavigationButtons
            isPending={isFormPending || isImagePending}
            navigateBack={() => navigate(ACCOUNT_ROUTE)}
          />

          <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
            <Alert
              onClose={handleClose}
              severity="error"
              sx={{ width: '100%' }}
            >
              {snackbarMessage}
            </Alert>
          </Snackbar>

          {albumFormError && (
            <Alert variant="outlined" severity="error">
              {albumFormError.message}
            </Alert>
          )}
        </form>
      </Box>
      <Box sx={{ width: '40%' }}>
        <AlbumPreview
          coverUrl={coverBlob?.url}
          artistName={'YOU'}
          title={watch('title')}
          songs={albumSongs}
        />
      </Box>
    </Box>
  );
};

export default CreateAlbumForm;
