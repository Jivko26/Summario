// src/components/MainContent.js
import React, { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { FileUpload } from 'primereact/fileupload';
import './styles.css';


const MainContent = () => {

    const [fileName, setFileName] = useState('');
    const [searchResult, setSearchResult] = useState('');

    const searchFile = async () => {
        // if (!fileName.trim()) {
        //     alert('Please enter a file name.');
        //     return;
        // }

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
                <FileUpload mode="basic" name="demo[]" accept=".pdf,.docx" maxFileSize={10000000} />
            </div>
            <div className="search-container">
                <InputText
                    id="searchInput"
                    placeholder="Search for papers"
                    value={fileName}
                    onChange={(e) => setFileName(e.target.value)}
                />
                <Button
                    id="searchButton"
                    label="Search"
                    icon="pi pi-search"
                    onClick={searchFile}
                />
            </div>
            <Button id="historyButton" label="View History" icon="pi pi-clock" />
            <Button id="translateButton" label="Translate Document" icon="pi pi-globe" />
            <Button id="summarizeButton" label="Summarize Document" icon="pi pi-list" />
        </main>
    );
};

export default MainContent;
