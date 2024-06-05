import { yupResolver } from "@hookform/resolvers/yup";
import { logInSchema } from "./ValidationSchema";
import { useForm } from "react-hook-form";
import { InferType } from "yup";
import { Button, TextField } from "@mui/material";

type FormData = InferType<typeof logInSchema>;

const LogInForm = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<FormData>({
        resolver: yupResolver(logInSchema)
    });

    const onSubmit = (data: FormData) => console.log(data);

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="Form">
            <TextField
                {...register("username")}
                label="Username"
                error={!!errors.username}
                helperText={errors.username?.message}
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

            <Button variant="contained" type="submit" aria-label="Log In">
                Log In
            </Button>
        </form>
    );
}

export default LogInForm;