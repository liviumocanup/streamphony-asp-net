import { ChangeEvent, useState } from 'react';
import { Avatar, Box, IconButton } from '@mui/material';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import LoadingSpinner from '../../../../shared/LoadingSpinner';
import { BlobFile } from '../../../../shared/interfaces/Interfaces';

interface UploadImageBoxProps {
  contentType: string;
  onFileChange: (blob: BlobFile) => void;
  isPending: boolean;
  initialBlob?: BlobFile | null;
  avatarVariant?: 'rounded' | 'circular';
}

const UploadImageBox = ({
  contentType,
  onFileChange,
  isPending,
  initialBlob = null,
  avatarVariant = 'rounded',
}: UploadImageBoxProps) => {
  const [blob, setBlob] = useState<BlobFile | null>(initialBlob);
  const [isHovered, setIsHovered] = useState(true);

  const handleBlobChange = (event: ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0] || null;
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const result = reader.result?.toString() || '';

        const blob = {
          name: file.name,
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
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
      sx={{
        position: 'relative',
        height: '150px',
        width: '150px',
        mb: 5,
        mr: 4,
        '&:hover': {
          cursor: 'pointer',
          transition: 'all ease 0.3s',
        },
      }}
    >
      <Avatar
        variant={avatarVariant}
        src={blob?.url || ''}
        sx={{
          height: '150px',
          width: '150px',
        }}
      />

      <IconButton
        disabled={isPending}
        component="label"
        sx={{
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          borderRadius: avatarVariant === 'circular' ? '50%' : 'inherit',
          opacity: isHovered ? 1 : 0,
          transition: 'opacity 0.3s ease',
          bgcolor: 'text.disabled',
          '&:hover': {
            bgcolor: 'text.disabled',
          },
        }}
      >
        {isPending ? (
          <LoadingSpinner />
        ) : (
          <>
            <CloudUploadIcon
              sx={{ fontSize: 40, color: 'background.default' }}
            />
            <input
              type="file"
              hidden
              accept={contentType}
              onChange={handleBlobChange}
            />
          </>
        )}
      </IconButton>
    </Box>
  );
};

export default UploadImageBox;
