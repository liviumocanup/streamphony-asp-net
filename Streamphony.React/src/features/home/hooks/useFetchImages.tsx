import { useSuspenseQueries } from '@tanstack/react-query';

const url = 'https://picsum.photos';

const fetchImageUrl = async () => {
  // await new Promise((resolve) => setTimeout(resolve, 5000));
  const response = await fetch(`${url}/185`);
  if (!response.ok) {
    throw new Error('Network response was not ok');
  }
  return response.url;
};

const useFetchImages = (items: { name: string }[]) => {
  const queryResults = useSuspenseQueries({
    queries: items.map((item) => ({
      queryKey: ['image', item.name],
      queryFn: () => fetchImageUrl(),
      staleTime: Infinity,
    })),
  });
  const imageUrls = items.reduce(
    (acc, item, index) => {
      acc[item.name] = queryResults[index]?.data ?? '';
      return acc;
    },
    {} as { [key: string]: string },
  );

  return {
    imageUrls,
    isLoading: queryResults.some((query) => query.isLoading),
    isError: queryResults.some((query) => query.isError),
    errors: queryResults
      .filter((query) => query.error)
      .map((query) => query.error),
  };
};

export default useFetchImages;
