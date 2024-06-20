import { useState } from 'react';

const useManagePlayer = () => {
  const [currentTrack, setTrack] = useState({
    coverUrl: '',
    audioUrl: '',
    title: '',
    artist: '',
    duration: 0,
    stoppedAt: 0,
    autoPlay: false,
  });
  const [isPlaying, setIsPlaying] = useState(false);

  const togglePlay = () => {
    setIsPlaying(!isPlaying);
  };

  const updateStoppedAt = (time: number) => {
    setTrack((prevTrack) => ({
      ...prevTrack,
      stoppedAt: time,
    }));
  };

  return { currentTrack, setTrack, isPlaying, togglePlay, updateStoppedAt };
};

export default useManagePlayer;
