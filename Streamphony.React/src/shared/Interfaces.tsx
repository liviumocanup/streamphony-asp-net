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

export interface Album {
  id: number;
  title: string;
  releaseDate: string;
  coverImageUrl: string;
}

export interface Item {
  name: string;
  description: string;
}

export interface UrlArray {
  [key: string]: string;
}
