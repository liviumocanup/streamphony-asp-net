import { Controller, FieldErrors } from 'react-hook-form';
import { TextField } from '@mui/material';

interface UsernameInputProps {
  control: any;
  errors: FieldErrors<{ username: string }>;
}

const UsernameInput = ({ control, errors }: UsernameInputProps) => {
  return (
    <Controller
      name="username"
      control={control}
      rules={{ required: 'Username required' }}
      render={({ field }) => (
        <TextField
          {...field}
          id="username-textfield"
          type="text"
          label="Username"
          error={!!errors.username}
          helperText={errors.username?.message}
          sx={{ mb: 2 }}
          aria-invalid={!!errors.username}
        />
      )}
    />
  );
};

export default UsernameInput;
