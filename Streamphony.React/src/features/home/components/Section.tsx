import { Box, Grid } from '@mui/material';
import { Dispatch, SetStateAction, Suspense } from 'react';
import Card from './Card';
import FeedSectionContainer from './SectionContainer';
import { Item } from '../../../shared/interfaces/Interfaces';
import NextPaginationButton from './NextPaginationButton';
import BackPaginationButton from './BackPaginationButton';

interface SectionProps {
  items: Item[];
  sectionTitle: string;
  imageVariant?: 'circular' | 'rounded';
  totalRecords: number;
  pageSize: number;
  pageNumber: number;
  setPageNumber: Dispatch<SetStateAction<number>>;
}

const Section = ({
  items,
  sectionTitle,
  imageVariant = 'rounded',
  totalRecords,
  pageSize,
  pageNumber,
  setPageNumber,
}: SectionProps) => {
  return (
    <FeedSectionContainer
      sectionTitle={sectionTitle}
      totalRecords={totalRecords}
      pageSize={pageSize}
      pageNumber={pageNumber}
    >
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'row',
          alignItems: 'center',
          justifyContent: 'space-between',
          overflow: 'hidden',
          position: 'relative',
          width: '100%',
        }}
      >
        <BackPaginationButton
          pageNumber={pageNumber}
          setPageNumber={setPageNumber}
        />

        <Box
          sx={{
            overflowX: 'hidden',
            flexGrow: 1,
            mr: 6,
            ml: 4,
          }}
        >
          <Grid container spacing={3} sx={{ minWidth: 1200 }}>
            {items.map((item: Item, index: number) => (
              <Suspense key={index} fallback={<div>Loading...</div>}>
                <Grid item xs={12} sm={6} md={4} lg={3} xl={2}>
                  <Card
                    key={index}
                    index={index}
                    image={item.coverUrl}
                    title={item.name}
                    description={item.description}
                    resource={item}
                    imageAlt={item.name}
                    imageVariant={imageVariant}
                  />
                </Grid>
              </Suspense>
            ))}
          </Grid>
        </Box>

        <NextPaginationButton
          totalRecords={totalRecords}
          pageSize={pageSize}
          pageNumber={pageNumber}
          setPageNumber={setPageNumber}
        />
      </Box>
    </FeedSectionContainer>
  );
};

export default Section;
