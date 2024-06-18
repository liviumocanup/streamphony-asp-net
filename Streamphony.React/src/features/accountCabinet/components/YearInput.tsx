import FormInput from '../../auth/components/FormInput';

interface YearInputProps {
  control: any;
  errors: any;
}

const YearInput = ({ control, errors }: YearInputProps) => {
  return (
    <FormInput
      name={'year'}
      type={'text'}
      label={'Year'}
      control={control}
      errors={errors}
      sx={{ flex: 1, mb: 1 }}
      inputProps={{ maxLength: 4, inputMode: 'numeric', pattern: '[0-9]*' }}
    />
  );
};

export default YearInput;
