export interface Song {
  id: string;
  title: string;
  coverUrl: string;
  audioUrl: string;
  duration: string;
  createdAt: string;
  updatedAt: string;
}

export interface Artist {
  id: string;
  stageName: string;
  dateOfBirth: string;
  profilePictureUrl: string;
  createdAt: string;
  updatedAt: string;
}

export enum ArtistRole {
  Performer = 'Performer',
  Writer = 'Writer',
  Producer = 'Producer',
}

export interface Album {
  id: string;
  title: string;
  releaseDate: string;
  totalDuration: string;
  coverUrl: string;
  createdAt: string;
  updatedAt: string;
}

export interface BlobFile {
  name: string;
  url: string;
  file: File;
  id?: string;
}

export enum BlobType {
  Song = 'Song',
  ProfilePicture = 'ProfilePicture',
  SongCover = 'SongCover',
  AlbumCover = 'AlbumCover',
}

export interface Genre {
  id: string;
  name: string;
  description: string;
  createdAt: string;
  updatedAt: string;
}
