import {
  FormControl,
  TextField,
  MenuItem,
  Checkbox,
  ListItemText,
} from '@mui/material';
import { ArtistCollaborator } from '../../../shared/interfaces/Interfaces';

interface Props {
  artistCollaborator: ArtistCollaborator;
  roles: string[];
  onChange: (artistId: string, newRoles: string[]) => void;
}

const ArtistRoleAssignment = ({
  artistCollaborator,
  roles,
  onChange,
}: Props) => {
  return (
    <FormControl fullWidth margin="normal">
      <TextField
        select
        label={`Roles for ${artistCollaborator.artist.stageName}`}
        value={artistCollaborator.roles}
        onChange={(e) => onChange(artistCollaborator.artist.id, e.target.value)}
        SelectProps={{
          multiple: true,
          renderValue: (selected) => (selected as string[]).join(', '),
        }}
      >
        {roles.map((role) => (
          <MenuItem key={role} value={role}>
            <Checkbox checked={artistCollaborator.roles.includes(role)} />
            <ListItemText primary={role} />
          </MenuItem>
        ))}
      </TextField>
    </FormControl>
  );
};

export default ArtistRoleAssignment;
