import { createContext } from 'react';

interface Track {
  coverUrl: string;
  audioUrl: string;
  title: string;
  artist: string;
  duration: number;
  stoppedAt: number;
  autoPlay: boolean;
}

type AudioPlayerContextType = {
  currentTrack: Track;
  setTrack: (track: Track) => void;
  isPlaying: boolean;
  togglePlay: () => void;
  updateStoppedAt: (time: number) => void;
};

const defaultContextValue: AudioPlayerContextType = {
  currentTrack: {
    coverUrl: '',
    audioUrl: '',
    title: '',
    artist: '',
    duration: 0,
    stoppedAt: 0,
    autoPlay: false,
  },
  setTrack: () => {},
  isPlaying: false,
  togglePlay: () => {},
  updateStoppedAt: () => {},
};

const AudioPlayerContext =
  createContext<AudioPlayerContextType>(defaultContextValue);

export default AudioPlayerContext;
