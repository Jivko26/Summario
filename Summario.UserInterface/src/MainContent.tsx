// src/components/MainContent.js
import React, { useState } from 'react';
import './styles.css';


const MainContent = () => {

    const [fileName, setFileName] = useState('');
    const [searchResult, setSearchResult] = useState('');

    const searchFile = async () => {
        if (!fileName.trim()) {
            alert('Please enter a file name.');
            return;
        }

        try {
            const response = await fetch(`https://localhost:7095/api/Summarizer/SearchFile?fileName=${encodeURIComponent(fileName)}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }

            console.log("Response", response);


            const result = response.url;
            console.log("Result", result);

            setSearchResult(result);
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    };

    return (
        <main>
            <div className="input-container">
                <input type="file" id="fileInput" accept=".pdf, .docx" />
                <button id="uploadButton">Upload Document</button>
            </div>
            <div className="search-container">
                <input
                    type="text"
                    id="searchInput"
                    placeholder="Search for papers"
                    value={fileName}
                    onChange={(e) => setFileName(e.target.value)}
                />
                <button id="searchButton" onClick={searchFile}>Search {searchResult ? searchResult : "Click to search"}</button>
            </div>
            <button id="historyButton">View History</button>
            <button id="translateButton">Translate Document</button>
            <button id="summarizeButton">Summarize Document</button>
        </main>
    );
};

export default MainContent;
