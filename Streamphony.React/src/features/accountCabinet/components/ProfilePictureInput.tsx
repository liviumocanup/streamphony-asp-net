import FormInput from '../../auth/components/FormInput';
import { FieldErrors } from 'react-hook-form';

interface ProfilePictureInputProps {
  control: any;
  errors: FieldErrors<{ profilePicture: string }>;
}

const ProfilePictureInput = ({ control, errors }: ProfilePictureInputProps) => {
  return (
    <FormInput
      name={'profilePicture'}
      type={'url'}
      label={'Profile Picture'}
      control={control}
      errors={errors}
    />
  );
};

export default ProfilePictureInput;
