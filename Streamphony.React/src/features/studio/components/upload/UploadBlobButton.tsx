import { ChangeEvent, useState } from 'react';
import { Box, Button, Typography } from '@mui/material';
import AudiotrackIcon from '@mui/icons-material/Audiotrack';
import FileUploadRoundedIcon from '@mui/icons-material/FileUploadRounded';
import LoadingSpinner from '../../../../shared/LoadingSpinner';
import { BlobFile } from '../../../../shared/interfaces/Interfaces';
import { displayAlert } from '../../../../shared/utils';

interface UploadBlobButtonProps {
  contentType: string;
  onFileChange: (blob: BlobFile) => void;
  isPending: boolean;
  error: any;
  initialBlob?: BlobFile | null;
}

const UploadBlobButton = ({
  contentType,
  onFileChange,
  isPending,
  error,
  initialBlob = null,
}: UploadBlobButtonProps) => {
  const [blob, setBlob] = useState<BlobFile | null>(initialBlob);

  const handleBlobChange = (event: ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0] || null;
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const result = reader.result?.toString() || '';

        const fileNameWithoutExtension = file.name
          .split('.')
          .slice(0, -1)
          .join('.');

        const blob = {
          name: fileNameWithoutExtension,
          url: result,
          file: file,
        };

        setBlob(blob);
        onFileChange(blob);
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'flex-end',
      }}
    >
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'row',
          mb: 1,
        }}
      >
        <AudiotrackIcon sx={{ mr: 1 }} />
        <Typography noWrap={true} maxWidth={'270px'}>
          {blob?.name || 'Upload a Song'}
        </Typography>
      </Box>

      <Box
        sx={{
          display: 'flex',
          flexDirection: 'row',
          mt: 1,
        }}
      >
        {displayAlert({ isPending, error, isBlobNull: blob === null })}

        <Button
          disabled={isPending}
          component="label"
          variant={'contained'}
          startIcon={<FileUploadRoundedIcon />}
          sx={{ textTransform: 'none', width: '140px', ml: 2 }}
        >
          {isPending ? (
            <LoadingSpinner
              sx={{ color: 'text.primary', width: '15px', height: '15px' }}
            />
          ) : (
            <>
              Replace file
              <input
                type="file"
                hidden
                accept={contentType}
                onChange={handleBlobChange}
              />
            </>
          )}
        </Button>
      </Box>
    </Box>
  );
};

export default UploadBlobButton;
