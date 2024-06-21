export const AUTH0_DOMAIN = 'dev-jvf2cje0ljzcgdis.us.auth0.com';
export const AUTH0_CLIENT_ID = 'M3cFuX1na4lgS5QcR4se1bIEYoirhTTG';

export const API_URL = 'http://localhost:5207/api';
// process.env.REACT_APP_DE;
export const LOG_IN_ENDPOINT = 'auth/login';
export const SIGN_UP_ENDPOINT = 'auth/register';
export const REGISTER_ARTIST_ENDPOINT = 'artists';
export const CREATE_SONG_ENDPOINT = 'songsDashboard';
export const UPLOAD_BLOB_ENDPOINT = 'blobs';

export const LS_TOKEN_KEY = 'token';
export const LS_THEME_KEY = 'theme';

export const APP_TITLE = 'Streamphony';

export const MONTHS = [
  { value: 0, label: 'January' },
  { value: 1, label: 'February' },
  { value: 2, label: 'March' },
  { value: 3, label: 'April' },
  { value: 4, label: 'May' },
  { value: 5, label: 'June' },
  { value: 6, label: 'July' },
  { value: 7, label: 'August' },
  { value: 8, label: 'September' },
  { value: 9, label: 'October' },
  { value: 10, label: 'November' },
  { value: 11, label: 'December' },
];

export const MIN_YEAR = 1900;
export const MIN_AGE = 13;

export const IMAGE_CONTENT_TYPE = 'image/png, image/jpeg';
export const AUDIO_CONTENT_TYPE = 'audio/mpeg, audio/wav';

export enum BlobType {
  Song = 'Song',
  ProfilePicture = 'ProfilePicture',
  SongCover = 'SongCover',
  AlbumCover = 'AlbumCover',
}

export const APP_GREEN = '#4caf50';
