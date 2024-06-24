import { object, string } from 'yup';

export const registerUserSchema = object({
  firstName: string().required('First name is required'),
  lastName: string().required('Last name is required'),
  username: string()
    .min(3, 'Username must be at least 3 characters')
    .required(),
  email: string().email('Email is not valid').required('Email is required'),
  password: string()
    .min(5, 'Password must be at least 5 characters')
    .required('Password is required'),
}).required();

export const logInSchema = object({
  username: string()
    .min(3, 'Username must be at least 3 characters')
    .required(),
  password: string()
    .min(5, 'Password must be at least 5 characters')
    .required('Password is required'),
}).required();
