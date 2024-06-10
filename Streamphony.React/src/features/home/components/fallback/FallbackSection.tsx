import { Grid } from '@mui/material';
import FallbackSectionContainer from './FallbackSectionContainer';
import FallbackCard from './FallbackCard';

interface FallbackSectionProps {
  imageVariant?: 'circular' | null;
}

const FallbackSection = ({ imageVariant }: FallbackSectionProps) => {
  return (
    <FallbackSectionContainer>
      <Grid container spacing={2}>
        {[...Array(7)].map((_, index) => (
          <FallbackCard
            key={index}
            index={index}
            imageVariant={imageVariant ? imageVariant : 'rounded'}
          />
        ))}
      </Grid>
    </FallbackSectionContainer>
  );
};

export default FallbackSection;
