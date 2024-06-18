import { Slider, useTheme } from '@mui/material';

interface AudioSliderProps {
  position: number;
  setPosition: (position: number) => void;
  duration: number;
  min?: number;
  max?: number;
  step?: number;
}

const AudioSlider = ({
  position,
  setPosition,
  duration,
  min = 0,
  max = duration,
  step = 1,
}: AudioSliderProps) => {
  const theme = useTheme();

  return (
    <Slider
      aria-label="time-indicator"
      size="small"
      value={position}
      min={min}
      max={max}
      step={step}
      onChange={(_, value) => setPosition(value as number)}
      sx={{
        color: theme.palette.mode === 'dark' ? '#fff' : 'rgba(0,0,0,0.87)',
        height: 3,
        '& .MuiSlider-thumb': {
          width: 8,
          height: 8,
          transition: '0.3s cubic-bezier(.47,1.64,.41,.8)',
          '&::before': {
            boxShadow: '0 2px 12px 0 rgba(0,0,0,0.4)',
          },
          '&:hover, &.Mui-focusVisible': {
            boxShadow: `0px 0px 0px 8px ${
              theme.palette.mode === 'dark'
                ? 'rgb(255 255 255 / 16%)'
                : 'rgb(0 0 0 / 16%)'
            }`,
          },
          '&.Mui-active': {
            width: 20,
            height: 20,
          },
        },
        '& .MuiSlider-rail': {
          opacity: 0.28,
        },
      }}
    />
  );
};

export default AudioSlider;
