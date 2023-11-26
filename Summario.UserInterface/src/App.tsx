import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import 'primereact/resources/themes/saga-blue/theme.css';
import 'primereact/resources/primereact.css';
import 'primeicons/primeicons.css';
import Header from './Header';
import MainContent from './MainContent';
import Footer from './Footer';

function App() {

  return (
    <div>
      <Header />
      <MainContent />
      <Footer />
    </div>
  );
}

export default App
