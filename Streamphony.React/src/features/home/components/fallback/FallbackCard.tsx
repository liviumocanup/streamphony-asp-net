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
          position: 'relative',
        }}
      >
        <Box
          sx={{ position: 'relative', width: 'fit-content', margin: 'auto' }}
        >
          <HomeSkeleton variant={imageVariant}>
            <Avatar
              variant={imageVariant}
              sx={{
                bgcolor: 'primary.main',
                display: 'block',
                // maxWidth: '185px',
                // maxHeight: '185px',
                // width: 'auto',
                // height: 'auto',
                width: 185,
                height: 185,
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
      </Box>
    </Grid>
  );
};

export default FallbackCard;
