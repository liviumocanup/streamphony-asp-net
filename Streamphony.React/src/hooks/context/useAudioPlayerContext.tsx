import { useContext } from 'react';
import AudioPlayerContext from '../../context/AudioPlayerContext';

const useAudioPlayerContext = () => useContext(AudioPlayerContext);

export default useAudioPlayerContext;
