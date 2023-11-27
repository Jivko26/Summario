
import './App.css'
import 'primereact/resources/themes/saga-blue/theme.css';
import 'primereact/resources/primereact.css';
import 'primeicons/primeicons.css';
import Header from './components/Header';
import MainContent from './components/MainContent';
import Footer from './components/Footer';

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
