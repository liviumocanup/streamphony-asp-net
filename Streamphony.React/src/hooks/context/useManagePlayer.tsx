import { useState } from 'react';
import { Track } from '../../shared/interfaces/Interfaces';
import { fallbackTrack } from '../../shared/utils';

const useManagePlayer = () => {
  const [playlist, setPlaylist] = useState<Track[]>([]);
  const [currentTrackIndex, setCurrentTrackIndex] = useState(0);
  const [isPlaying, setIsPlaying] = useState(false);
  const [loop, setLoop] = useState(false);

  const currentTrack = playlist[currentTrackIndex] || fallbackTrack;

  const togglePlay = () => {
    setIsPlaying(!isPlaying);
  };

  const toggleLoop = () => {
    setLoop(!loop);
  };

  const updateStoppedAt = (time: number) => {
    const updatedPlaylist = [...playlist];
    updatedPlaylist[currentTrackIndex] = {
      ...updatedPlaylist[currentTrackIndex],
      stoppedAt: time,
    };
    setPlaylist(updatedPlaylist);
  };

  const playNext = () => {
    const nextIndex = currentTrackIndex + 1;

    if (nextIndex >= playlist.length) {
      setCurrentTrackIndex(0);
      if (!loop) {
        setIsPlaying(false);
        return;
      }
    }

    setCurrentTrackIndex(nextIndex);
    setIsPlaying(nextIndex < playlist.length);
  };

  const playPrevious = () => {
    const prevIndex =
      (currentTrackIndex - 1 + playlist.length) % playlist.length;
    setCurrentTrackIndex(prevIndex);
    setIsPlaying(true);
  };

  const addToPlaylist = (track: Track) => {
    setPlaylist([...playlist, track]);
  };

  const removeFromPlaylist = (index: number) => {
    const newPlaylist = playlist.filter((_, i) => i !== index);
    setPlaylist(newPlaylist);

    if (index === currentTrackIndex && playlist.length > 1) {
      playNext();
    } else {
      setCurrentTrackIndex(0);
    }
  };

  const replacePlaylist = (newPlaylist: Track[]) => {
    setPlaylist(newPlaylist);
    setCurrentTrackIndex(0);
    setIsPlaying(true);
  };

  return {
    playlist,
    currentTrack,
    isPlaying,
    togglePlay,
    updateStoppedAt,
    playNext,
    playPrevious,
    addToPlaylist,
    removeFromPlaylist,
    replacePlaylist,
    loop,
    toggleLoop,
  };
};

export default useManagePlayer;
