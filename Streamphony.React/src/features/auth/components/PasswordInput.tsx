import React, { useState } from 'react';
import { Controller } from 'react-hook-form';
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
  errors: any;
}

const PasswordInput = ({ control, errors }: PasswordInputProps) => {
  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword(!showPassword);
  const handleMouseDownPassword = (
    event: React.MouseEvent<HTMLButtonElement>,
  ) => event.preventDefault();

  return (
    <FormControl variant="outlined" sx={{ mb: 2 }}>
      <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
      <Controller
        name="password"
        control={control}
        rules={{ required: 'Password required' }}
        render={({ field }) => (
          <OutlinedInput
            {...field}
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
