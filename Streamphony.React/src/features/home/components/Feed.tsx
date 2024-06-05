import { Grid } from '@mui/material';
import Card from './Card';
import FeedSection from './FeedSection';
import { artists, albums, playlists } from '../../../shared/dummy_data';
import useFetchImages from '../../../hooks/useFetchImages';

const Feed = () => {
    const artistImages = useFetchImages(artists);
    const albumImages = useFetchImages(albums);
    const playlistImages = useFetchImages(playlists);
    const imageUrls = { ...artistImages, ...albumImages, ...playlistImages };

    interface Item {
        name: string;
        description: string;
    }

    const renderGridSection = (items: Item[], sectionTitle: string, imageVariant?: "circular" | null) => (
        <FeedSection title={sectionTitle}>
            <Grid container spacing={2}>
                {items.map((item: Item, index: number) => (
                    <Card
                        key={index}
                        index={index}
                        image={imageUrls[item.name]}
                        imageAlt={item.name}
                        title={item.name}
                        description={item.description}
                        imageVariant={imageVariant ? imageVariant : "rounded"}
                    />
                ))}
            </Grid>
        </FeedSection>
    );

    return (
        <>
            {renderGridSection(artists, "Popular artists", "circular")}
            {renderGridSection(albums, "Popular albums")}
            {renderGridSection(playlists, "Featured playlists")}
        </>
    );
};

export default Feed;
