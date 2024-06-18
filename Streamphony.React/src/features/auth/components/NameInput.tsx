import { Box } from '@mui/material';
import { FieldErrors } from 'react-hook-form';
import FormInput from './FormInput';

interface NameInputProps {
  control: any;
  errors: FieldErrors<{ firstName: string; lastName: string }>;
}

const NameInput = ({ control, errors }: NameInputProps) => {
  return (
    <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 2 }}>
      <FormInput
        name={'firstName'}
        type={'text'}
        label={'First Name'}
        control={control}
        errors={errors}
        sx={{ mr: 2, flex: 1, mb: 1 }}
      />

      <FormInput
        name={'lastName'}
        type={'text'}
        label={'Last Name'}
        control={control}
        errors={errors}
        sx={{ flex: 1, mb: 1 }}
      />
    </Box>
  );
};

export default NameInput;
