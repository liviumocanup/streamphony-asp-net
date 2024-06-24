import React, { ReactElement } from 'react';
import { Artist, ArtistRole } from './EntitiesInterfaces';
import { ArtistDetails, SongDetails } from './EntityDetailsInterfaces';

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
  stageName: string;
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

export interface EditSongData {
  id: string;
  title: string;
  coverBlobId: string;
}

export interface CreateAlbumData {
  title: string;
  releaseDate: string;
  coverFileId: string;
  songIds: string[];
  collaborators?: {
    [key: string]: ArtistRole;
  };
}

export enum ItemType {
  SONG = 'songs',
  ALBUM = 'albums',
  ARTIST = 'artists',
  PLAYLIST = 'playlists',
}

export interface Item {
  name: string;
  description: string;
  coverUrl: string;
  resourceUrl: string;
  resource: any;
}

export interface Track {
  id: string;
  coverUrl: string;
  audioUrl: string;
  title: string;
  artist: string;
  album: string;
  duration: number;
  stoppedAt: number;
  autoPlay: boolean;
  resource: SongDetails[];
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

export interface ArtistCollaborator {
  artist: ArtistDetails;
  role: string[];
}
