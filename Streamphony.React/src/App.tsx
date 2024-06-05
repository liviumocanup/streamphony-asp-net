import Home from './features/home/Home'
import { useEffect } from 'react';
import SignUp from './features/auth/SignUp';
import { Route, Routes } from 'react-router-dom';
import LogIn from './features/auth/LogIn';
import { useThemeContext } from './hooks/context/ThemeContext';

const App = () => {
  const { currentTheme } = useThemeContext();

  useEffect(() => {
    document.body.style.backgroundColor = currentTheme.palette.background.default;
  }, [currentTheme]);

  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/signUp" element={<SignUp />} />
      <Route path="/logIn" element={<LogIn />} />
    </Routes>
  );
};

export default App
