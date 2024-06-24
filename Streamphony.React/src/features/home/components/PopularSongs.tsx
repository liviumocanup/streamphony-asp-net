import Section from './Section';
import FallbackSection from './fallback/FallbackSection';
import { useState } from 'react';
import useGetPopularSongs from '../hooks/useGetPopularSongs';
import { ItemType } from '../../../shared/interfaces/Interfaces';
import { SongDetails } from '../../../shared/interfaces/EntityDetailsInterfaces';

const pageSize = 6;

interface PopularSongsProps {
  imageVariant: 'circular' | 'rounded';
}

const PopularSongs = ({ imageVariant }: PopularSongsProps) => {
  const [pageNumber, setPageNumber] = useState(1);
  const { data: paginatedSongs, isLoading } = useGetPopularSongs(
    pageNumber,
    pageSize,
  );

  if (isLoading) return <FallbackSection imageVariant={imageVariant} />;

  const totalRecords = paginatedSongs.totalRecords || 0;
  const items =
    paginatedSongs.items?.map((song: SongDetails) => ({
      name: song.title,
      coverUrl: song.coverUrl,
      resourceUrl: song.audioUrl,
      description: song.artist.stageName,
      resource: song,
      resourceType: ItemType.SONG,
    })) || [];

  return (
    <Section
      items={items}
      sectionTitle="Popular Songs"
      imageVariant={imageVariant}
      totalRecords={totalRecords}
      pageSize={pageSize}
      pageNumber={pageNumber}
      setPageNumber={setPageNumber}
    />
  );
};

export default PopularSongs;
