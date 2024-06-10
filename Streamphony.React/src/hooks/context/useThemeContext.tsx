import { useContext } from 'react';
import ThemeContext from '../../themes/ThemeContext';

const useThemeContext = () => useContext(ThemeContext);

export default useThemeContext;
