import { createContext } from 'react';
import { Track } from '../shared/interfaces/Interfaces';
import { fallbackTrack } from '../shared/utils';

type AudioPlayerContextType = {
  currentTrack: Track;
  isPlaying: boolean;
  togglePlay: () => void;
  updateStoppedAt: (time: number) => void;
  playlist: Track[];
  playNext: () => void;
  playPrevious: () => void;
  addToPlaylist: (track: Track) => void;
  removeFromPlaylist: (index: number) => void;
  replacePlaylist: (playlist: Track[]) => void;
  loop: boolean;
  toggleLoop: () => void;
};

const defaultContextValue: AudioPlayerContextType = {
  currentTrack: fallbackTrack,
  isPlaying: false,
  togglePlay: () => {},
  updateStoppedAt: () => {},
  playlist: [],
  playNext: () => {},
  playPrevious: () => {},
  addToPlaylist: () => {},
  removeFromPlaylist: () => {},
  replacePlaylist: () => {},
  loop: false,
  toggleLoop: () => {},
};

const AudioPlayerContext =
  createContext<AudioPlayerContextType>(defaultContextValue);

export default AudioPlayerContext;
