import { Song } from '../../../shared/interfaces/Interfaces';
import {
  Autocomplete,
  Avatar,
  CircularProgress,
  FormControl,
  ListItem,
  ListItemAvatar,
  ListItemText,
  TextField,
} from '@mui/material';
import React, { Dispatch, SetStateAction, useEffect, useState } from 'react';
import useGetFilterSongsForArtist from '../hooks/useGetFilterSongsForArtist';
import { formatDuration } from '../../../shared/utils';

interface SongSelectProps {
  selectedSongs: Song[];
  setSelectedSongs: Dispatch<SetStateAction<Song[]>>;
}

const SongSelect = ({ selectedSongs, setSelectedSongs }: SongSelectProps) => {
  const [inputValue, setInputValue] = useState('');
  const {
    mutate: fetchSongs,
    data: songs,
    isPending,
  } = useGetFilterSongsForArtist();

  useEffect(() => {
    fetchSongs(inputValue);
  }, [fetchSongs, inputValue]);

  const handleSongChange = (_event: React.SyntheticEvent, newValue: Song[]) => {
    const updatedSongs = newValue.map((newSong) => {
      const existingSong = selectedSongs.find((ss) => ss.id === newSong.id);
      if (existingSong) {
        return existingSong;
      }
      return newSong;
    });

    setSelectedSongs(updatedSongs);
  };

  return (
    <FormControl fullWidth>
      <Autocomplete
        multiple
        inputValue={inputValue}
        onInputChange={(_event, newInputValue, reason) => {
          if (reason === 'input') setInputValue(newInputValue);
        }}
        onChange={handleSongChange}
        options={songs || []}
        getOptionLabel={(option) => option.title}
        value={selectedSongs.map((song) => song)}
        loading={isPending}
        isOptionEqualToValue={(option, value) => option.id === value.id}
        renderInput={(params) => (
          <TextField
            {...params}
            variant="outlined"
            label="Songs"
            placeholder="Type to search songs..."
            InputProps={{
              ...params.InputProps,
              endAdornment: (
                <>
                  {isPending ? (
                    <CircularProgress color="inherit" size={20} />
                  ) : null}
                  {params.InputProps.endAdornment}
                </>
              ),
            }}
          />
        )}
        renderOption={(props, option, { selected }) => (
          <ListItem {...props} style={{ fontWeight: selected ? 700 : 400 }}>
            <ListItemAvatar>
              <Avatar src={option.coverUrl} alt={option.title} />
            </ListItemAvatar>
            <ListItemText
              primary={option.title}
              secondary={`Duration: ${formatDuration({ timeSpan: option.duration })}`}
            />
          </ListItem>
        )}
      />
    </FormControl>
  );
};

export default SongSelect;
