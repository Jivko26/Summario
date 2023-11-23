import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Header from './Header';
import MainContent from './MainContent';
import Footer from './Footer';

function App() {
  const [file, setFile] = useState(null);

  const handleFileChange = (event: any) => {
    setFile(event.target.files[0]);
  };

  const handleSubmit = async (event: any) => {
    event.preventDefault();
    if (!file) {
      alert('Please select a file first!');
      return;
    }

    const formData = new FormData();
    formData.append('file', file);

    try {
      const response = await fetch('https://localhost:44320/api/Summarizer/UploadPdf', {
        method: 'POST',
        body: formData,
      });

      if (response.ok) {
        const summaries = await response.json();
        console.log(summaries); // Do something with the summaries
        alert('File uploaded successfully!');
      } else {
        alert('Failed to upload file.');
      }
    } catch (error) {
      console.error('Error:', error);
      alert('Error uploading file.');
    }
  };

  return (
    <div>
      <Header />
      <MainContent />
      <Footer />
    </div>
  );
}

export default App
