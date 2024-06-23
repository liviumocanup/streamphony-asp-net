import React, { Dispatch, SetStateAction, useEffect, useState } from 'react';
import {
  Autocomplete,
  TextField,
  FormControl,
  CircularProgress,
} from '@mui/material';
import useGetFilteredArtists from '../hooks/useGetFilteredArtists';
import ArtistRoleAssignment from './ArtistRoleAssignment';
import { ArtistCollaborator } from '../../../shared/interfaces/Interfaces';
import { ArtistDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';

interface Props {
  selectedArtists: ArtistCollaborator[];
  setSelectedArtists: Dispatch<SetStateAction<ArtistCollaborator[]>>;
}

const roles = ['Performer', 'Writer', 'Producer'];

const ArtistSelectInput = ({ selectedArtists, setSelectedArtists }: Props) => {
  const [inputValue, setInputValue] = useState('');
  const {
    mutate: fetchArtists,
    data: artists,
    isPending,
  } = useGetFilteredArtists();

  useEffect(() => {
    if (inputValue.trim().length > 0) {
      fetchArtists(inputValue);
    }
  }, [inputValue]);

  const handleArtistChange = (
    _event: React.SyntheticEvent,
    newValue: ArtistDetails[],
  ) => {
    const updatedArtists = newValue.map((newArtist) => {
      const existingArtist = selectedArtists.find(
        (sa) => sa.artist.id === newArtist.id,
      );
      if (existingArtist) {
        return existingArtist;
      }
      return { artist: newArtist, roles: [roles[0]] };
    });

    setSelectedArtists(updatedArtists);
  };

  const handleRoleChange = (artistId: string, newRoles: string[]) => {
    const updatedArtists = selectedArtists.map((artistCollaborator) =>
      artistCollaborator.artist.id === artistId
        ? { ...artistCollaborator, roles: newRoles }
        : artistCollaborator,
    );
    setSelectedArtists(updatedArtists);
  };

  return (
    <FormControl fullWidth>
      <Autocomplete
        multiple
        inputValue={inputValue}
        onInputChange={(_event, newInputValue, reason) => {
          if (reason === 'input') setInputValue(newInputValue);
        }}
        onChange={handleArtistChange}
        options={artists || []}
        getOptionLabel={(option) => option.stageName}
        value={selectedArtists.map(
          (artistCollaborator) => artistCollaborator.artist,
        )}
        loading={isPending}
        isOptionEqualToValue={(option, value) => option.id === value.id}
        renderInput={(params) => (
          <TextField
            {...params}
            variant="outlined"
            label="Artists"
            placeholder="Type to search artists..."
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
        renderOption={(props, option, { selected }) => {
          const { key, ...restProps } = props;
          return (
            <li
              key={key}
              {...restProps}
              style={{ fontWeight: selected ? 700 : 400 }}
            >
              {option.stageName}
            </li>
          );
        }}
      />
      {selectedArtists.map((artistCollaborator) => (
        <ArtistRoleAssignment
          key={artistCollaborator.artist.id}
          artistCollaborator={artistCollaborator}
          roles={roles}
          onChange={handleRoleChange}
        />
      ))}
    </FormControl>
  );
};

export default ArtistSelectInput;
