import { CircularProgress, SxProps } from '@mui/material';
import { Theme } from '@mui/material/styles';

interface LoadingSpinnerProps {
  sx?: SxProps<Theme>;
}

const LoadingSpinner = ({
  sx = { color: 'background.default' },
}: LoadingSpinnerProps) => {
  return <CircularProgress size={25} sx={sx} />;
};

export default LoadingSpinner;
