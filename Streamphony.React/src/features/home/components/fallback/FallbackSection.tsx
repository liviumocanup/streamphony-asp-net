import { Grid } from '@mui/material';
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
      <Grid container spacing={2}>
        {[...Array(7)].map((_, index) => (
          <FallbackCard key={index} index={index} imageVariant={imageVariant} />
        ))}
      </Grid>
    </FallbackSectionContainer>
  );
};

export default FallbackSection;
