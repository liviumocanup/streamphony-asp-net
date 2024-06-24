import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { Alert, Button } from '@mui/material';
import { InferType } from 'yup';
import { registerUserSchema } from './ValidationSchema';
import { useNavigate } from 'react-router-dom';
import PasswordInput from './PasswordInput';
import LoadingSpinner from '../../../shared/LoadingSpinner';
import useSignUp from '../hooks/useSignUp';
import { HOME_ROUTE } from '../../../routes/routes';
import UsernameInput from './UsernameInput';
import EmailInput from './EmailInput';
import NameInput from './NameInput';

type FormData = InferType<typeof registerUserSchema>;

const SignUpForm = () => {
  const navigate = useNavigate();
  const { mutateAsync: signUp, error, isPending } = useSignUp();

  const {
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(registerUserSchema),
    defaultValues: {
      firstName: '',
      lastName: '',
      email: '',
      username: '',
      password: '',
    },
  });

  const onSubmit = async (data: FormData) => {
    try {
      await signUp(data);
      navigate(HOME_ROUTE);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="Form">
      <NameInput control={control} errors={errors} />

      <EmailInput control={control} errors={errors} />

      <UsernameInput control={control} errors={errors} />

      <PasswordInput control={control} errors={errors} />

      <Button
        variant="contained"
        type="submit"
        aria-label="Sign Up"
        disabled={isPending}
        sx={{ mb: 2 }}
      >
        {isPending ? <LoadingSpinner /> : 'Sign Up'}
      </Button>

      {error && (
        <Alert variant="outlined" severity="error">
          {error.message}
        </Alert>
      )}
    </form>
  );
};

export default SignUpForm;
