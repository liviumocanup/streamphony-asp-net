import { Box, SxProps, Typography } from '@mui/material';
import { BlobFile } from '../../../../shared/interfaces/Interfaces';
import { displayAlert } from '../../../../shared/utils';
import { Theme } from '@mui/material/styles';

interface ImageGuidelineTextProps {
  isPending: boolean;
  error: Error | null;
  blob: BlobFile | null;
  sx?: SxProps<Theme>;
}

const ImageGuidelineText = ({
  isPending,
  error,
  blob,
  sx,
}: ImageGuidelineTextProps) => {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column' }}>
      <Typography fontWeight="bold" sx={{ mt: 2 }}>
        Image guidelines
      </Typography>
      <Box
        component="ul"
        sx={{
          listStyleType: 'disc',
          p: 0,
          mt: 2,
          ml: 1.5,
          mr: 4,
          fontSize: '0.85em',
          color: 'text.secondary',
        }}
      >
        <li>Square, at least 300x300px.</li>
        <li>File formats: PNG, JPEG.</li>
      </Box>

      {displayAlert({ isPending, error, isBlobNull: blob === null, sx })}
    </Box>
  );
};

export default ImageGuidelineText;
