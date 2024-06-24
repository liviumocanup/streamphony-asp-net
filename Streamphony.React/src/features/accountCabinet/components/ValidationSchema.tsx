import { number, object, string } from 'yup';
import { MIN_AGE, MIN_YEAR } from '../../../shared/constants';

const currentYear = new Date().getFullYear();
const dayError =
  'Please enter the day of your birth date by entering a number between 1 and 31.';
const yearError =
  'Please enter the year of your birth date using four digits (e.g., 1990).';

const registerArtistSchema = object({
  stageName: string().required('Name is required'),
  day: number()
    .typeError(dayError)
    .min(1, dayError)
    .max(31, dayError)
    .required('Day is required'),
  month: number().required('Select your birth month.'),
  year: number()
    .typeError(yearError)
    .min(MIN_YEAR, `Please enter a birth year from ${MIN_YEAR} onwards.`)
    .max(currentYear - MIN_AGE, 'You are too young to register as an artist.')
    .required(yearError),
}).required();

export default registerArtistSchema;
