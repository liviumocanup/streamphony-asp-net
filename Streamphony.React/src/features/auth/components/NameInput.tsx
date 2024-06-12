import { Box, TextField } from '@mui/material';
import { Controller, FieldErrors } from 'react-hook-form';

interface NameInputProps {
  control: any;
  errors: FieldErrors<{ firstName: string; lastName: string }>;
}

const NameInput = ({ control, errors }: NameInputProps) => {
  return (
    <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 2 }}>
      <Controller
        name="firstName"
        control={control}
        rules={{ required: 'First name required' }}
        render={({ field }) => (
          <TextField
            {...field}
            id="first-name-textfield"
            type="text"
            label="First Name"
            error={!!errors.firstName}
            helperText={errors.firstName?.message}
            sx={{ mr: 1, flex: 1 }}
            aria-invalid={!!errors.firstName}
          />
        )}
      />

      <Controller
        name="lastName"
        control={control}
        rules={{ required: 'Last name required' }}
        render={({ field }) => (
          <TextField
            {...field}
            id="last-name-textfield"
            type="text"
            label="Last Name"
            error={!!errors.lastName}
            helperText={errors.lastName?.message}
            sx={{ ml: 1, flex: 1 }}
            aria-invalid={!!errors.lastName}
          />
        )}
      />
    </Box>
  );
};

export default NameInput;
