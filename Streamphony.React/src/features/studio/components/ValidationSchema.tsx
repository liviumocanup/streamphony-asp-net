import { object, string } from 'yup';

export const createSongSchema = object({
  title: string().required('Title is required'),
  duration: string().required('Duration is required'),
  url: string().url('URL must be a valid URL').required('URL is required'),
}).required();

export default createSongSchema;
