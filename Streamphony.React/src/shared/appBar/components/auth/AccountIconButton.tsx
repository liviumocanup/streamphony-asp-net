import { Avatar, IconButton, Skeleton, Tooltip } from '@mui/material';
import React from 'react';
import useTokenStorage from '../../../../hooks/localStorage/useTokenStorage';
import useGetCurrentArtistDetails from '../../../../hooks/useGetCurrentArtistDetails';
import useAuthContext from '../../../../hooks/context/useAuthContext';

interface AccountIconButtonProps {
  open: boolean;
  openMenu: (event: React.MouseEvent<HTMLElement>) => void;
}

const AccountIconButton = ({ open, openMenu }: AccountIconButtonProps) => {
  const { isArtist } = useAuthContext();
  const { getTokenClaims } = useTokenStorage();
  const { data: artist, isPending, isLoading } = useGetCurrentArtistDetails();

  const tokenClaims = getTokenClaims();
  const firstName = tokenClaims?.firstName || '';
  const lastName = tokenClaims?.lastName || '';
  const pfpUrl = artist?.pfpUrl || '';

  if (isPending && isLoading)
    return <Skeleton variant={'circular'} width={35} height={35} />;

  return (
    <Tooltip title="Account Settings">
      <IconButton
        onClick={openMenu}
        size="small"
        aria-controls={open ? 'account-menu' : undefined}
        aria-haspopup="true"
        aria-expanded={open ? 'true' : undefined}
      >
        {isArtist ? (
          <Avatar src={pfpUrl} sx={{ width: 35, height: 35 }} />
        ) : (
          <Avatar
            sx={{
              bgcolor: 'primary.main',
              width: 35,
              height: 35,
              fontSize: 15,
            }}
          >
            {firstName[0] + lastName[0]}
          </Avatar>
        )}
      </IconButton>
    </Tooltip>
  );
};

export default AccountIconButton;
