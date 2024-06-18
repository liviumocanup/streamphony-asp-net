import FormInput from '../../auth/components/FormInput';

interface DayInputProps {
  control: any;
  errors: any;
}

const DayInput = ({ control, errors }: DayInputProps) => {
  return (
    <FormInput
      name={'day'}
      type={'text'}
      label={'Day'}
      control={control}
      errors={errors}
      sx={{ mr: 2, flex: 0.5, mb: 1 }}
      inputProps={{ maxLength: 2, inputMode: 'numeric' }}
    />
  );
};

export default DayInput;
