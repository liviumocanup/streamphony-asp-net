import {
  Autocomplete,
  CircularProgress,
  FormControl,
  ListItem,
  ListItemText,
  TextField,
} from '@mui/material';
import React, { Dispatch, SetStateAction, useEffect, useState } from 'react';
import { Genre } from '../../../shared/interfaces/EntitiesInterfaces';
import useGetFilterGenres from '../hooks/useGetFilterGenres';

interface Props {
  selectedGenre: Genre;
  setSelectedGenre: Dispatch<SetStateAction<Genre>>;
}

const GenreSelect = ({ selectedGenre, setSelectedGenre }: Props) => {
  const [inputValue, setInputValue] = useState('');
  const { mutate: fetchGenres, data: genres, isPending } = useGetFilterGenres();

  useEffect(() => {
    fetchGenres(inputValue);
  }, [fetchGenres, inputValue]);

  const handleGenreChange = (
    _event: React.SyntheticEvent,
    newValue: Genre[],
  ) => {
    const updatedGenre = newValue.map((newGenre) => {
      if (selectedGenre.id === newGenre.id) {
        return selectedGenre;
      }
      return newGenre;
    });

    setSelectedGenre(updatedGenre);
  };

  return (
    <FormControl fullWidth>
      <Autocomplete
        multiple
        inputValue={inputValue}
        onInputChange={(_event, newInputValue, reason) => {
          if (reason === 'input') setInputValue(newInputValue);
        }}
        onChange={handleGenreChange}
        options={genres || []}
        getOptionLabel={(option) => option.name}
        value={selectedGenre}
        loading={isPending}
        isOptionEqualToValue={(option, value) => option.id === value.id}
        renderInput={(params) => (
          <TextField
            {...params}
            variant="outlined"
            label="Genre"
            placeholder="Type to search genres..."
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
            <ListItemText
              primary={option.name}
              secondary={`Description: ${option.description}`}
            />
          </ListItem>
        )}
      />
    </FormControl>
  );
};

export default GenreSelect;
