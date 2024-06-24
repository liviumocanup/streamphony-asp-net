import React, { useState } from 'react';
import { Controller, FieldErrors } from 'react-hook-form';
import {
  FormControl,
  InputLabel,
  OutlinedInput,
  InputAdornment,
  IconButton,
  FormHelperText,
} from '@mui/material';
import { Visibility, VisibilityOff } from '@mui/icons-material';

interface PasswordInputProps {
  control: any;
  errors: FieldErrors<{ password: string }>;
}

const PasswordInput = ({ control, errors }: PasswordInputProps) => {
  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword(!showPassword);
  const handleMouseDownPassword = (
    event: React.MouseEvent<HTMLButtonElement>,
  ) => event.preventDefault();

  return (
    <FormControl variant="outlined" sx={{ mb: 2 }}>
      <InputLabel id="outlined-adornment-password" error={!!errors.password}>
        Password
      </InputLabel>
      <Controller
        name="password"
        control={control}
        render={({ field }) => (
          <OutlinedInput
            {...field}
            id="outlined-adornment-password"
            type={showPassword ? 'text' : 'password'}
            endAdornment={
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            }
            label="Password"
            error={!!errors.password}
          />
        )}
      />
      <FormHelperText error={!!errors.password}>
        {errors.password?.message}
      </FormHelperText>
    </FormControl>
  );
};

export default PasswordInput;
