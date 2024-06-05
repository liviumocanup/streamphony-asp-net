import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import { Button, TextField } from "@mui/material";
import { InferType } from "yup";
import { registerSchema } from "./ValidationSchema";

type FormData = InferType<typeof registerSchema>;

const SignUpForm = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<FormData>({
        resolver: yupResolver(registerSchema)
    });

    const onSubmit = (data: FormData) => console.log(data);

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="Form">
            <TextField
                {...register("firstName")}
                label="First Name"
                error={!!errors.firstName}
                helperText={errors.firstName?.message}
                sx={{ mb: 2 }}
            />

            <TextField
                {...register("lastName")}
                label="Last Name"
                error={!!errors.lastName}
                helperText={errors.lastName?.message}
                sx={{ mb: 2 }}
            />

            <TextField
                {...register("username")}
                label="Username"
                error={!!errors.username}
                helperText={errors.username?.message}
                sx={{ mb: 2 }}
            />

            <TextField
                {...register("email")}
                type="email"
                label="Email"
                error={!!errors.email}
                helperText={errors.email?.message}
                sx={{ mb: 2 }}
            />

            <TextField
                {...register("password")}
                type="password"
                label="Password"
                error={!!errors.password}
                helperText={errors.password?.message}
                sx={{ mb: 2 }}
            />

            <Button variant="contained" type="submit" aria-label="Sign Up">
                Sign Up
            </Button>
        </form>
    );
}

export default SignUpForm;