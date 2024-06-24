import { FieldErrors } from 'react-hook-form';
import { Box } from '@mui/material';
import DayInput from './DayInput';
import YearInput from './YearInput';
import MonthInput from './MonthInput';

interface DateOfBirthInputProps {
  setValue: any;
  control: any;
  errors: FieldErrors<{ day: number; month: string; year: number }>;
}

const DateOfBirthInput = ({ control, errors }: DateOfBirthInputProps) => {
  return (
    <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 2 }}>
      <DayInput control={control} errors={errors} />

      <MonthInput control={control} errors={errors} />

      <YearInput control={control} errors={errors} />
    </Box>
  );
};

export default DateOfBirthInput;
