import { styled } from '@mui/material/styles';

interface MainProps {
  open: boolean;
  drawerWidth: number;
}

export const Main = styled('main', {
  shouldForwardProp: (prop) => prop !== 'open',
})<MainProps>(({ theme, open, drawerWidth }) => ({
  flexGrow: 1,
  backgroundColor: theme.palette.background.default,
  padding: theme.spacing(1),
  transition: theme.transitions.create('margin', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  marginLeft: `-${drawerWidth}px`,
  ...(open && {
    transition: theme.transitions.create('margin', {
      easing: theme.transitions.easing.easeOut,
      duration: theme.transitions.duration.enteringScreen,
    }),
    marginLeft: 0,
  }),
}));
