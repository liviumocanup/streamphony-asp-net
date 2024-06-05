import { useEffect, useState } from 'react';

const useFetchImages = (items: { name: string }[]) => {
    const [imageUrls, setImageUrls] = useState<{ [key: string]: string }>({});

    useEffect(() => {
        const fetchImages = async () => {
            items.forEach(item => {
                fetch(`https://picsum.photos/185`)
                    .then(response => response.url)
                    .then(url => setImageUrls(prevUrls => ({ ...prevUrls, [item.name]: url })))
                    .catch(error => console.error('Error fetching image:', error));
            });
        };

        fetchImages();
    }, [items]);

    return imageUrls;
};

export default useFetchImages;
