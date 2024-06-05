import { Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import Card from './Card';
import FeedSection from './FeedSection';
import { artists, albums, playlists } from '../../../shared/dummy_data';

const Feed = () => {
    const [imageUrls, setImageUrls] = useState<{ [key: string]: string }>({});

    useEffect(() => {
        fetchImages();
    }, []);

    const fetchImages = () => {
        const fetchData = (items: { name: string }[]) => {
            items.forEach(item => {
                fetch(`https://picsum.photos/185`)
                    .then(response => response.url)
                    .then(url => setImageUrls(prevUrls => ({ ...prevUrls, [item.name]: url })))
                    .catch(error => console.error('Error fetching image:', error));
            });
        };

        fetchData(artists);
        fetchData(albums);
        fetchData(playlists);
    };

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
