import { Grid } from '@mui/material';
import { Suspense } from 'react';
import Card from './Card';
import FeedSectionContainer from './SectionContainer';
import { Item, UrlArray } from '../../../shared/Interfaces';

interface SectionProps {
  items: Item[];
  imageUrls: UrlArray;
  sectionTitle: string;
  imageVariant?: 'circular' | null;
}

const Section = ({
  items,
  imageUrls,
  sectionTitle,
  imageVariant,
}: SectionProps) => {
  return (
    <FeedSectionContainer sectionTitle={sectionTitle}>
      <Grid container spacing={2}>
        {items.map((item: Item, index: number) => (
          <Suspense key={index} fallback={<div>Loading...</div>}>
            <Card
              key={index}
              index={index}
              image={imageUrls[item.name]}
              imageAlt={item.name}
              title={item.name}
              description={item.description}
              imageVariant={imageVariant ? imageVariant : 'rounded'}
            />
          </Suspense>
        ))}
      </Grid>
    </FeedSectionContainer>
  );
};

export default Section;
