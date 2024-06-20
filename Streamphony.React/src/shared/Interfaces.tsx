import React, { ReactElement } from 'react';

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
  profilePictureId: string;
}

export interface CreateSongData {
  title: string;
  coverFileId: string;
  audioFileId: string;
  genreId?: string;
  albumId?: string;
}

export interface Song {
  id: string;
  title: string;
  coverUrl: string;
  audioUrl: string;
  duration: string;
  genreId: string;
  albumId: string;
  ownerId: string;
  createdAt: string;
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
  coverUrl: string;
  resourceUrl: string;
  resource: any;
}

export interface BlobFile {
  name: string;
  url: string;
  file: File;
  id?: string;
}

export interface TableHeader {
  label: string;
  propertyName: string;
  centered?: boolean;
  width?: string;
  icon?: ReactElement;
  renderCell?: (
    item: any,
    index: number,
    isHovered: boolean,
  ) => React.ReactNode;
}
