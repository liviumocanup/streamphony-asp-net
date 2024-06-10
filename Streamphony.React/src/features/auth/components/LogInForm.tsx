import { yupResolver } from '@hookform/resolvers/yup';
import { logInSchema } from './ValidationSchema';
import { useForm } from 'react-hook-form';
import { InferType } from 'yup';
import { Alert, Button, TextField } from '@mui/material';
import { useLocation, useNavigate } from 'react-router-dom';
import PasswordInput from './PasswordInput';
import LoadingSpinner from '../../../shared/LoadingSpinner';
import useLogIn from '../hooks/useLogIn';

type FormData = InferType<typeof logInSchema>;

const LogInForm = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { mutateAsync: logIn, error, isPending } = useLogIn();

  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(logInSchema),
  });

  const onSubmit = async (data: FormData) => {
    try {
      await logIn(data);
      const from = location.state?.from?.pathname || '/';
      navigate(from);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="Form">
      <TextField
        {...register('username')}
        label="Username"
        error={!!errors.username}
        helperText={errors.username?.message}
        sx={{ mb: 2 }}
      />

      <PasswordInput control={control} errors={errors} />

      <Button
        variant="contained"
        type="submit"
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
