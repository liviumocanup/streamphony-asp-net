import { FieldErrors } from 'react-hook-form';
import FormInput from './FormInput';

interface UsernameInputProps {
  control: any;
  errors: FieldErrors<{ username: string }>;
}

const UsernameInput = ({ control, errors }: UsernameInputProps) => {
  return (
    <FormInput
      name={'username'}
      type={'text'}
      label={'Username'}
      control={control}
      errors={errors}
    />
  );
};

export default UsernameInput;
