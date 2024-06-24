import { Box, Grid } from '@mui/material';
import FallbackSectionContainer from './FallbackSectionContainer';
import FallbackCard from './FallbackCard';

interface FallbackSectionProps {
  imageVariant?: 'circular' | 'rounded';
}

const FallbackSection = ({
  imageVariant = 'rounded',
}: FallbackSectionProps) => {
  return (
    <FallbackSectionContainer>
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'row',
          alignItems: 'center',
          justifyContent: 'space-between',
          overflow: 'hidden',
          position: 'relative',
        }}
      >
        <Box
          sx={{
            mr: 6,
            ml: 4,
          }}
        >
          <Grid container spacing={2}>
            {[...Array(6)].map((_, index) => (
              <FallbackCard
                key={index}
                index={index}
                imageVariant={imageVariant}
              />
            ))}
          </Grid>
        </Box>
      </Box>
    </FallbackSectionContainer>
  );
};

export default FallbackSection;
