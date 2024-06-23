import Section from './Section';
import FallbackSection from './fallback/FallbackSection';
import { useState } from 'react';
import useGetPopularAlbums from '../hooks/useGetPopularAlbums';
import { ItemType } from '../../../shared/interfaces/Interfaces';
import { AlbumDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';

const pageSize = 6;

interface PopularAlbumsProps {
  imageVariant: 'circular' | 'rounded';
}

const PopularAlbums = ({ imageVariant }: PopularAlbumsProps) => {
  const [pageNumber, setPageNumber] = useState(1);
  const { data: paginatedAlbums, isLoading } = useGetPopularAlbums(
    pageNumber,
    pageSize,
  );

  if (isLoading) return <FallbackSection imageVariant={imageVariant} />;

  const totalRecords = paginatedAlbums.totalRecords || 0;
  const items =
    paginatedAlbums.items?.map((album: AlbumDetails) => ({
      name: album.title,
      coverUrl: album.coverUrl,
      resourceUrl: '',
      description: album.owner.stageName,
      resource: album,
      resourceType: ItemType.ALBUM,
    })) || [];

  return (
    <Section
      items={items}
      sectionTitle="Popular Albums"
      imageVariant={imageVariant}
      totalRecords={totalRecords}
      pageSize={pageSize}
      pageNumber={pageNumber}
      setPageNumber={setPageNumber}
    />
  );
};

export default PopularAlbums;
