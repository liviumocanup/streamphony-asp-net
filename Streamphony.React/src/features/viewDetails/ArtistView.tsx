import { useParams } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';
import AlbumViewDetails from './components/AlbumViewDetails';
import useGetArtist from './hooks/useGetArtist';
import ArtistViewDetails from './components/ArtistViewDetails';

const ArtistView = () => {
  const { id } = useParams();
  const { data: artist, isLoading, isError } = useGetArtist(id);

  console.log(id);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError) {
    return <div>Error</div>;
  }

  return (
    <>
      <Helmet>
        <title>{artist.stageName}</title>
        <meta
          name="description"
          content={`Details for Artist ${artist.stageName}`}
        />
      </Helmet>

      <AppBarWrapper
        showPlayer={true}
        children={<ArtistViewDetails artist={artist} />}
      />
    </>
  );
};

export default ArtistView;
