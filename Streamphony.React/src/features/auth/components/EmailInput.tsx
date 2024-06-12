import { Controller, FieldErrors } from 'react-hook-form';
import { TextField } from '@mui/material';

interface EmailInputProps {
  control: any;
  errors: FieldErrors<{ email: string }>;
}

const EmailInput = ({ control, errors }: EmailInputProps) => {
  return (
    <Controller
      name="email"
      control={control}
      rules={{ required: 'Email required' }}
      render={({ field }) => (
        <TextField
          {...field}
          id="email-textfield"
          type="email"
          label="Email"
          error={!!errors.email}
          helperText={errors.email?.message}
          sx={{ mb: 2 }}
          aria-invalid={!!errors.email}
        />
      )}
    />
  );
};

export default EmailInput;
