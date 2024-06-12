import { ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import { ArrowForwardIos as Caret } from '@mui/icons-material';
import { ReactElement } from 'react';

interface RoundedHoverButtonProps {
  item: {
    name: string;
    icon: ReactElement;
    onClick: () => void;
  };
  hasVisibleBackground?: boolean;
  hasVisibleCaret?: boolean;
  mb?: string;
}

const RoundedHoverButton = ({
  item,
  hasVisibleBackground = false,
  hasVisibleCaret = false,
  mb = '0.5rem',
}: RoundedHoverButtonProps) => (
  <ListItemButton
    onClick={item.onClick}
    key={item.name}
    sx={{
      mb: mb,
      transition: 'all 0.4s ease',
      '&:hover': {
        borderRadius: '10px',
        backgroundColor: 'action.hover',
      },
      '& .MuiListItemIcon-root': {
        minWidth: '40px',
        marginRight: '0.5rem',
        ...(hasVisibleBackground && {
          backgroundColor: 'background.default',
          borderRadius: '7px',
          color: '#fff',
          padding: '0.5rem',
          marginRight: '1rem',
        }),
      },
    }}
  >
    <ListItemIcon>{item.icon}</ListItemIcon>
    <ListItemText primary={item.name} />
    {hasVisibleCaret ? <Caret /> : null}
  </ListItemButton>
);

export default RoundedHoverButton;
