import { useParams } from 'react-router-dom';
import useGetAlbum from './hooks/useGetAlbum';
import { Helmet } from 'react-helmet-async';
import AlbumViewDetails from './components/AlbumViewDetails';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';

const AlbumView = () => {
  const { id } = useParams();
  const { data: album, isLoading, isError } = useGetAlbum(id);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError) {
    return <div>Error</div>;
  }

  return (
    <>
      <Helmet>
        <title>
          {album.title} - {album.owner.stageName}
        </title>
        <meta
          name="description"
          content={`Details for Album ${album.title} by ${album.owner.stageName}`}
        />
      </Helmet>

      <AppBarWrapper
        showPlayer={true}
        children={<AlbumViewDetails album={album} />}
      />
    </>
  );
};

export default AlbumView;
