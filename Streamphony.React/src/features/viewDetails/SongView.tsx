import { useParams } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';
import AppBarWrapper from '../../shared/drawer/AppBarWrapper';
import useGetSong from './hooks/useGetSong';
import SongViewDetails from './components/SongViewDetails';

const SongView = () => {
  const { id } = useParams();
  const { data: song, isLoading, isError } = useGetSong(id);

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
          {song.title} - {song.artist.stageName}
        </title>
        <meta
          name="description"
          content={`Details for Song ${song.title} by ${song.artist.stageName}`}
        />
      </Helmet>

      <AppBarWrapper
        showPlayer={true}
        children={<SongViewDetails song={song} />}
      />
    </>
  );
};

export default SongView;
