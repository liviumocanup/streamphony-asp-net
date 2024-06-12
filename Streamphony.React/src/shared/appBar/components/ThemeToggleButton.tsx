import { IconButton } from '@mui/material';
import MoonIcon from '@mui/icons-material/NightsStayOutlined';
import SunIcon from '@mui/icons-material/LightModeOutlined';
import LightTheme from '../../../themes/LightTheme';
import useThemeContext from '../../../hooks/context/useThemeContext';

const ThemeToggleButton = () => {
  const { activeTheme, toggleTheme } = useThemeContext();
  const isLightMode = activeTheme === LightTheme;

  return (
    <IconButton size={'medium'} onClick={toggleTheme} sx={{ mr: 2 }}>
      {isLightMode ? <SunIcon /> : <MoonIcon />}
    </IconButton>
  );
};

export default ThemeToggleButton;
