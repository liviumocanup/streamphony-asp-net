import { Grid } from '@mui/material';
import { Suspense } from 'react';
import Card from './Card';
import FeedSectionContainer from './SectionContainer';
import { Item } from '../../../shared/Interfaces';

interface SectionProps {
  items: Item[];
  sectionTitle: string;
  imageVariant?: 'circular' | 'rounded';
}

const Section = ({
  items,
  sectionTitle,
  imageVariant = 'rounded',
}: SectionProps) => {
  return (
    <FeedSectionContainer sectionTitle={sectionTitle}>
      <Grid container spacing={2}>
        {items.map((item: Item, index: number) => (
          <Suspense key={index} fallback={<div>Loading...</div>}>
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
          </Suspense>
        ))}
      </Grid>
    </FeedSectionContainer>
  );
};

export default Section;
