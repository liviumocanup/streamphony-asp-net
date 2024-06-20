import { object, string } from 'yup';

export const createSongSchema = object({
  title: string().required('Title is required'),
  genre: string(),
  album: string(),
}).required();

export default createSongSchema;
