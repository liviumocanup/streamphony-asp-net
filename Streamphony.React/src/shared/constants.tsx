export const API_URL = 'http://localhost:5207/api';
// process.env.REACT_APP_DE;

const AUTH_PATH = 'auth';
export const LOG_IN_ENDPOINT = `${AUTH_PATH}/login`;
export const SIGN_UP_ENDPOINT = `${AUTH_PATH}/register`;
export const REFRESH_TOKEN_ENDPOINT = `${AUTH_PATH}/refresh`;

export const UPLOAD_BLOB_ENDPOINT = 'blobs';

export const SONG_ENDPOINT = 'songs';
export const CURRENT_USER_SONGS_ENDPOINT = `${SONG_ENDPOINT}/user/current`;
export const SONG_FILTERED_ENDPOINT = `${SONG_ENDPOINT}/filtered`;

export const ARTIST_ENDPOINT = 'artists';
export const CURRENT_USER_ARTIST_ENDPOINT = `${ARTIST_ENDPOINT}/current`;
export const ARTIST_FILTERED_ENDPOINT = `${ARTIST_ENDPOINT}/filtered`;

export const ALBUM_ENDPOINT = 'albums';
export const CURRENT_USER_ALBUMS_ENDPOINT = `${ALBUM_ENDPOINT}/user/current`;

export const USER_ENDPOINT = 'users';

export const GENRE_ENDPOINT = 'genres';
export const GENRE_FILTERED_ENDPOINT = `${GENRE_ENDPOINT}/filtered`;

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

export const APP_GREEN = '#1DB954';
