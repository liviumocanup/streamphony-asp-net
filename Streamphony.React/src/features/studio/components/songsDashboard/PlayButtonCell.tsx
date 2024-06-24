import { IconButton } from '@mui/material';
import { PlayArrow } from '@mui/icons-material';
import { useMemo } from 'react';
import useAudioPlayerContext from '../../../../hooks/context/useAudioPlayerContext';
import { Song } from '../../../../shared/interfaces/Interfaces';
import { durationToSeconds } from '../../../../shared/utils';

interface PlayButtonCellProps {
  index: number;
  isHovered: boolean;
  song: Song;
}

const prepareData = (song: Song) => {
  return {
    ...song,
    artist: song.artistName,
    album: song.albumName,
    stoppedAt: 0,
    autoPlay: false,
    resource: [song],
    duration: durationToSeconds(song.duration),
  };
};

const PlayButtonCell = ({ index, isHovered, song }: PlayButtonCellProps) => {
  const { isPlaying, togglePlay, replacePlaylist } = useAudioPlayerContext();

  const handleClick = () => {
    if (isPlaying) {
      togglePlay();
    } else {
      replacePlaylist([prepareData(song)]);
    }
  };

  return useMemo(
    () =>
      isHovered ? (
        <IconButton onClick={handleClick} sx={{ m: 0, p: 0 }}>
          <PlayArrow sx={{ color: 'text.primary', fontSize: 20 }} />
        </IconButton>
      ) : (
        index + 1
      ),
    [handleClick, index, isHovered],
  );
};

export default PlayButtonCell;
