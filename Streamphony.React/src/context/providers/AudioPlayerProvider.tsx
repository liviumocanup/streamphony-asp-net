import { ReactNode, useMemo } from 'react';
import AudioPlayerContext from '../AudioPlayerContext';
import useManagePlayer from '../../hooks/context/useManagePlayer';

const AudioPlayerProvider = ({ children }: { children: ReactNode }) => {
  const player = useManagePlayer();

  const contextValue = useMemo(() => player, [player]);

  return (
    <AudioPlayerContext.Provider value={contextValue}>
      {children}
    </AudioPlayerContext.Provider>
  );
};

export default AudioPlayerProvider;
