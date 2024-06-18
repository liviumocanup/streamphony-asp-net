import { InputBaseComponentProps, SxProps, TextField } from '@mui/material';
import { Controller } from 'react-hook-form';
import { Theme } from '@mui/material/styles';
import { ChangeEvent } from 'react';

interface FormInputProps {
  name: string;
  type: string;
  label: string;
  control: any;
  errors: any;
  sx?: SxProps<Theme>;
  inputProps?: InputBaseComponentProps;
  handleInput?: (event: ChangeEvent<HTMLInputElement>) => void;
}

const FormInput = ({
  name,
  type,
  label,
  control,
  errors,
  sx = { mb: 2 },
  inputProps,
  handleInput,
}: FormInputProps) => {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field }) => (
        <TextField
          fullWidth={true}
          {...field}
          id={`${name}-field`}
          type={type}
          label={label}
          error={!!errors[name]}
          helperText={errors[name]?.message}
          sx={sx}
          aria-invalid={!!errors[name]}
          inputProps={inputProps}
          {...(handleInput && { onChange: handleInput })}
        />
      )}
    />
  );
};

export default FormInput;
