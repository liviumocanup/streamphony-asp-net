import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { Alert, Button, TextField } from '@mui/material';
import { InferType } from 'yup';
import { registerSchema } from './ValidationSchema';
import { useNavigate } from 'react-router-dom';
import PasswordInput from './PasswordInput';
import LoadingSpinner from '../../../shared/LoadingSpinner';
import useSignUp from '../hooks/useSignUp';
import { HOME_ROUTE } from '../../../routes/routes';

type FormData = InferType<typeof registerSchema>;

const SignUpForm = () => {
  const navigate = useNavigate();
  const { mutateAsync: signUp, error, isPending } = useSignUp();

  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(registerSchema),
  });

  const onSubmit = async (data: FormData) => {
    console.log(data);
    try {
      await signUp(data);
      navigate(HOME_ROUTE);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="Form">
      <TextField
        {...register('firstName')}
        label="First Name"
        error={!!errors.firstName}
        helperText={errors.firstName?.message}
        sx={{ mb: 2 }}
      />

      <TextField
        {...register('lastName')}
        label="Last Name"
        error={!!errors.lastName}
        helperText={errors.lastName?.message}
        sx={{ mb: 2 }}
      />

      <TextField
        {...register('username')}
        label="Username"
        error={!!errors.username}
        helperText={errors.username?.message}
        sx={{ mb: 2 }}
      />

      <TextField
        {...register('email')}
        type="email"
        label="Email"
        error={!!errors.email}
        helperText={errors.email?.message}
        sx={{ mb: 2 }}
      />

      <PasswordInput control={control} errors={errors} />

      <Button
        variant="contained"
        type="submit"
        aria-label="Sign Up"
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
