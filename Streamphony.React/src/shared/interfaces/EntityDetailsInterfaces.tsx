import {
  Album,
  Artist,
  ArtistRole,
  BlobFile,
  Song,
} from './EntitiesInterfaces';

export interface SongDetails {
  id: string;
  title: string;
  coverUrl: string;
  audioUrl: string;
  duration: string;
  artist: Artist;
  album?: Album;
  genre?: string;
  coverBlob: BlobFile;
  createdAt: string;
  updatedAt: string;
}

export interface ArtistDetails {
  id: string;
  stageName: string;
  dateOfBirth: string;
  profilePictureUrl: string;
  uploadedSongs: Song[];
  ownedAlbums: Album[];
  createdAt: string;
  updatedAt: string;
}

export interface ArtistCollaborators {
  artist: Artist;
  role: ArtistRole;
}

export interface AlbumDetails {
  id: string;
  title: string;
  releaseDate: string;
  totalDuration: string;
  coverUrl: string;
  owner: Artist;
  collaborators: ArtistCollaborators[];
  songs: Song[];
  createdAt: string;
  updatedAt: string;
}
