export const formatDuration = (timeSpan: string) => {
  const parts = timeSpan.split(':');
  if (parts.length === 3) {
    const hours = parseInt(parts[0], 10);
    const minutes = parseInt(parts[1], 10);
    const seconds = parseInt(parts[2], 10); // We ignore the fractional seconds.
    return `${hours > 0 ? `${hours}:` : ''}${minutes}:${seconds.toString().padStart(2, '0')}`;
  }
  return timeSpan; // Return original if format is unexpected
};
