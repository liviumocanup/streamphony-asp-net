import { Box, Button } from '@mui/material';
import LoadingSpinner from '../../../shared/LoadingSpinner';

interface Props {
  isPending: boolean;
  navigateBack: () => void;
}

const FormNavigationButtons = ({ isPending, navigateBack }: Props) => {
  return (
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
  );
};

export default FormNavigationButtons;
