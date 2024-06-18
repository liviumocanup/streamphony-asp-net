import { Box, List, Typography } from '@mui/material';
import RoundedHoverButton from '../../../shared/RoundedHoverButton';
import { ReactElement } from 'react';

interface Item {
  name: string;
  icon: ReactElement;
  onClick: () => void;
}

interface CabinetSectionProps {
  sectionName: string;
  sectionItems: Item[];
}

const CabinetSection = ({ sectionName, sectionItems }: CabinetSectionProps) => {
  return (
    <Box
      sx={{
        bgcolor: 'background.paper',
        width: '50rem',
        borderRadius: '10px',
        mb: 2,
      }}
    >
      <Typography variant="h5" sx={{ p: 2, fontWeight: 'bold' }}>
        {sectionName}
      </Typography>
      <List sx={{ flexGrow: 1 }}>
        {sectionItems.map((item: Item) => (
          <RoundedHoverButton
            key={item.name}
            item={item}
            hasVisibleBackground={true}
            hasVisibleCaret={true}
          />
        ))}
      </List>
    </Box>
  );
};

export default CabinetSection;
