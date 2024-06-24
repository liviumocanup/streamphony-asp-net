import { object, string } from 'yup';

export const createSongSchema = object({
  title: string()
    .required('Title is required')
    .max(50, 'Title should not be longer than 50 characters.'),
  genre: string(),
}).required();

export const createAlbumSchema = object({
  title: string().required('Title is required'),
  releaseDate: string(),
}).required();

export const editSongSchema = object({
  title: string()
    .required('Title is required')
    .max(50, 'Title should not be longer than 50 characters.'),
}).required();
