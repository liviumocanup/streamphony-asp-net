import { yupResolver } from '@hookform/resolvers/yup';
import { logInSchema } from './ValidationSchema';
import { useForm } from 'react-hook-form';
import { InferType } from 'yup';
import { Alert, Button } from '@mui/material';
import { useLocation, useNavigate } from 'react-router-dom';
import PasswordInput from './PasswordInput';
import LoadingSpinner from '../../../shared/LoadingSpinner';
import useLogIn from '../hooks/useLogIn';
import { HOME_ROUTE } from '../../../routes/routes';
import UsernameInput from './UsernameInput';

type FormData = InferType<typeof logInSchema>;

const LogInForm = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { mutateAsync: logIn, error, isPending } = useLogIn();

  const {
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(logInSchema),
    defaultValues: {
      username: '',
      password: '',
    },
  });

  const onSubmit = async (data: FormData) => {
    try {
      await logIn(data);
      const from = location.state?.from?.pathname || HOME_ROUTE;
      navigate(from);
    } catch (err) {
      // TODO: Remove console call
      console.error('Login failed:', err);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="Form">
      <UsernameInput control={control} errors={errors} />

      <PasswordInput control={control} errors={errors} />

      <Button
        variant="contained"
        type="submit"
        disabled={isPending}
        aria-label="Log In"
        sx={{ mb: 2 }}
      >
        {isPending ? <LoadingSpinner /> : 'Log In'}
      </Button>

      {error && (
        <Alert variant="outlined" severity="error">
          {error.message}
        </Alert>
      )}
    </form>
  );
};

export default LogInForm;
