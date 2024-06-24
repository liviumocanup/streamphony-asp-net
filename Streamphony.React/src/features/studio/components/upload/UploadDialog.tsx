import {
  Dialog,
  DialogTitle,
  DialogContent,
  Button,
  DialogActions,
} from '@mui/material';
import { ChangeEvent, useState } from 'react';
import { AUDIO_CONTENT_TYPE } from '../../../../shared/constants';
import { BlobFile } from '../../../../shared/interfaces/Interfaces';

interface UploadDialogProps {
  open: boolean;
  onClose: (fileName?: BlobFile) => void;
}

const UploadDialog = ({ open, onClose }: UploadDialogProps) => {
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = (event: ChangeEvent<HTMLInputElement>) => {
    const selectedFile = event.target.files?.[0] || null;
    setFile(selectedFile);
  };

  const handleUpload = () => {
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const result = reader.result?.toString() || '';

        const fileNameWithoutExtension = file.name
          .split('.')
          .slice(0, -1)
          .join('.');

        const songBlob = {
          name: fileNameWithoutExtension,
          url: result,
          file: file,
        };

        onClose(songBlob);
      };
      reader.readAsDataURL(file);
    } else {
      onClose();
    }
  };

  return (
    <Dialog open={open} onClose={() => onClose()}>
      <DialogTitle>Upload a Song</DialogTitle>
      <DialogContent>
        <input
          type="file"
          onChange={handleFileChange}
          accept={AUDIO_CONTENT_TYPE}
        />
      </DialogContent>

      <DialogActions>
        <Button onClick={() => onClose()}>Cancel</Button>
        <Button onClick={handleUpload} disabled={!file}>
          Upload
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default UploadDialog;
