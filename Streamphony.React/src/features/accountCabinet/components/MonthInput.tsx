import {
  FormControl,
  FormHelperText,
  InputLabel,
  MenuItem,
  Select,
} from '@mui/material';
import { Controller } from 'react-hook-form';
import { MONTHS } from '../../../shared/constants';

interface MonthInputProps {
  control: any;
  errors: any;
}

const MonthInput = ({ control, errors }: MonthInputProps) => {
  return (
    <FormControl sx={{ mr: 2, flex: 1, mb: 1 }}>
      <InputLabel id={'month-field'} error={!!errors.month}>
        Month
      </InputLabel>
      <Controller
        name={'month'}
        control={control}
        render={({ field }) => (
          <Select
            {...field}
            id={'month-field'}
            label={'Month'}
            aria-invalid={!!errors.month}
            error={!!errors.month}
          >
            {MONTHS.map((month) => (
              <MenuItem key={month.value} value={month.value}>
                {month.label}
              </MenuItem>
            ))}
          </Select>
        )}
      />
      <FormHelperText error={!!errors.month}>
        {errors.month?.message}
      </FormHelperText>
    </FormControl>
  );
};

export default MonthInput;
