import { FieldErrors } from 'react-hook-form';
import FormInput from './FormInput';

interface EmailInputProps {
  control: any;
  errors: FieldErrors<{ email: string }>;
}

const EmailInput = ({ control, errors }: EmailInputProps) => {
  return (
    <FormInput
      name="email"
      type="email"
      label="Email"
      control={control}
      errors={errors}
    />
  );
};

export default EmailInput;
