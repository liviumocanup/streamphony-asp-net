import Section from './Section';
import FallbackSection from './fallback/FallbackSection';
import useGetPopularArtists from '../hooks/useGetPopularArtists';
import { useState } from 'react';
import { ItemType } from '../../../shared/interfaces/Interfaces';
import { ArtistDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';

const pageSize = 6;

interface PopularArtistsProps {
  imageVariant: 'circular' | 'rounded';
}

const PopularArtists = ({ imageVariant }: PopularArtistsProps) => {
  const [pageNumber, setPageNumber] = useState(1);
  const { data: paginatedArtists, isLoading } = useGetPopularArtists(
    pageNumber,
    pageSize,
  );

  if (isLoading) return <FallbackSection imageVariant={imageVariant} />;

  const totalRecords = paginatedArtists.totalRecords || 0;
  const items =
    paginatedArtists.items?.map((artist: ArtistDetails) => ({
      name: artist.stageName,
      coverUrl: artist.profilePictureUrl,
      resourceUrl: '',
      description: 'Artist',
      resource: artist,
      resourceType: ItemType.ARTIST,
    })) || [];

  return (
    <Section
      items={items}
      sectionTitle="Popular Artists"
      imageVariant={imageVariant}
      totalRecords={totalRecords}
      pageSize={pageSize}
      pageNumber={pageNumber}
      setPageNumber={setPageNumber}
    />
  );
};

export default PopularArtists;
