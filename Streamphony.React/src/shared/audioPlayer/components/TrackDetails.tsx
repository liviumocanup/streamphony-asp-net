import { Avatar, Box, keyframes, Typography } from '@mui/material';
import { useEffect, useRef, useState } from 'react';

interface TrackDetailsProps {
  title: string;
  artist: string;
  coverUrl: string;
}

const TrackDetails = ({ title, artist, coverUrl }: TrackDetailsProps) => {
  const [isOverflowing, setIsOverflowing] = useState(false);
  const [containerWidth, setContainerWidth] = useState(0);
  const [textWidth, setTextWidth] = useState(0);
  const containerRef = useRef<HTMLDivElement>(null);
  const textRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const container = containerRef.current;
    const text = textRef.current;
    if (container && text) {
      const { scrollWidth: textWidth } = text;
      const { clientWidth: containerWidth } = container;

      setIsOverflowing(textWidth > containerWidth);
      setTextWidth(textWidth);
      setContainerWidth(containerWidth);
    }
  }, [title]);

  const scrollDistance = textWidth - containerWidth;

  const marquee = keyframes`
    0% { transform: translateX(0); }
    10% { transform: translateX(0); }
    45% { transform: translateX(-${scrollDistance}px); }
    55% { transform: translateX(-${scrollDistance}px); }
    90% { transform: translateX(0); }
    100% { transform: translateX(0); }
  `;

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'row',
        width: '25%',
        minWidth: '185px',
      }}
    >
      <Avatar
        variant="rounded"
        src={coverUrl}
        sx={{
          width: '60px',
          height: '60px',
          ml: 3,
        }}
      />

      <Box
        ref={containerRef}
        sx={{
          ml: 1,
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          overflow: 'hidden',
        }}
      >
        <Box
          ref={textRef}
          sx={{
            whiteSpace: 'nowrap',
            display: 'inline-block',
            animation: isOverflowing
              ? `${marquee} 10s linear infinite`
              : 'none',
          }}
        >
          <Typography variant="body1">{title}</Typography>
        </Box>
        <Typography variant="body2" noWrap sx={{ color: 'text.disabled' }}>
          {artist}
        </Typography>
      </Box>
    </Box>
  );
};

export default TrackDetails;
