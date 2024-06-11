import { Avatar, Box, Grid, Typography } from '@mui/material';
import '../../Home.css';
import HomeSkeleton from '../../../../shared/HomeSkeleton';

interface FallbackCardProps {
  index: number;
  imageVariant: 'rounded' | 'circular';
}

const FallbackCard = ({ index, imageVariant }: FallbackCardProps) => {
  return (
    <Grid item key={index}>
      <Box
        className="Card"
        sx={{
          backgroundColor: 'background.paper',
          color: 'text.primary',
        }}
      >
        <HomeSkeleton variant={imageVariant}>
          <Avatar
            variant={imageVariant}
            sx={{
              bgcolor: 'primary.main',
              width: 185,
              height: 185,
              mb: 1,
            }}
          />
        </HomeSkeleton>

        <Typography variant="subtitle1" align="left" width="90%">
          <HomeSkeleton />
        </Typography>
        <Typography variant="body2" align="left" width="40%">
          <HomeSkeleton />
        </Typography>
      </Box>
    </Grid>
  );
};

export default FallbackCard;
