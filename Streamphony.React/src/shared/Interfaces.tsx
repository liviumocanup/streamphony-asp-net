export interface LogInData {
  username: string;
  password: string;
}

export interface SignUpData {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  password: string;
}

export interface RegisterArtistData {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  profilePictureUrl?: string;
}

export interface CreateSongData {
  title: string;
  duration: string;
  url: string;
  ownerId: string;
  genreId?: string;
  albumId?: string;
}

export interface Album {
  id: number;
  title: string;
  releaseDate: string;
  coverImageUrl: string;
}

export interface Item {
  name: string;
  description: string;
  url?: string;
}

export interface UrlArray {
  [key: string]: string;
}
