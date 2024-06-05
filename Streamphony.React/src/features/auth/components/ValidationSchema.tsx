import { object, string } from "yup";

export const registerSchema = object({
    firstName: string().required('First name is required'),
    lastName: string().required('Last name is required'),
    username: string().min(3, 'Username must be at least 3 characters'),
    email: string().email('Email is not valid').required('Email is required'),
    password: string().min(3, 'Password must be at least 8 characters'),
}).required();

export const logInSchema = object({
    username: string().min(3, 'Username must be at least 3 characters'),
    password: string().min(3, 'Password must be at least 8 characters'),
}).required();