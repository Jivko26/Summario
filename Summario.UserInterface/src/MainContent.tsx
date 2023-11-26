// src/components/MainContent.js
import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { FileUpload } from 'primereact/fileupload';
import { ProgressSpinner } from 'primereact/progressspinner';
import { Fieldset } from 'primereact/fieldset';

import './styles.css';


const MainContent = () => {

    const [uploadedFile, setUploadedFile] = useState(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [summaries, setSummaries] = useState<string>("");


    const onUpload = (e: any) => {
        console.log(e.files);
        setUploadedFile(e.files[0]);
        alert("File ready for summarization or translation")
    };

    const copyToClipboard = async () => {
        try {
            await navigator.clipboard.writeText(summaries);
            alert('Summary copied to clipboard');
            console.log('Summary copied to clipboard');
        } catch (err) {
            console.error('Failed to copy: ', err);
        }
    };


    const summarizeArticle = async () => {
        setIsLoading(true);

        if (!uploadedFile) {
            alert("Please upload a file first.");
            setIsLoading(false);
            return;
        }

        const formData = new FormData();
        formData.append('file', uploadedFile);

        try {
            const response = await fetch('https://localhost:7095/api/Summarizer/SummarizeArtilce', {
                method: 'POST',
                body: formData
            });

            //Prod
            // const response = await fetch('http://87.215.96.234:80/api/Summarizer/SummarizeArtilce', {
            //     method: 'POST',
            //     body: formData
            // });

            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }

            const summaries = await response.json();
            console.log("Summaries: ", summaries);
            const concatenatedSummaries = summaries.join('\n\n');
            setSummaries(concatenatedSummaries);

        } catch (error) {
            alert(`An error occured: ${error}`);
            console.error('There was a problem with the fetch operation:', error);
        }

        setIsLoading(false);
    };

    return (
        <main>
            {isLoading && (
                <div className="loader-container">
                    <ProgressSpinner />
                </div>
            )}
            <div className="card">
                <FileUpload
                    name="demo[]"
                    multiple
                    accept=".pdf,.docx"
                    maxFileSize={1000000}
                    emptyTemplate={<p className="m-0">Drag and drop files here to upload.</p>}
                    customUpload={true}
                    onRemove={() => {
                        setSummaries("")
                    }}
                    onClear={() => {
                        setSummaries("")
                    }}
                    uploadLabel='Summarize'
                    onSelect={onUpload}
                    uploadHandler={summarizeArticle}
                />
            </div>
            {summaries &&
                <Fieldset legend="Summary">
                    <Button label="Copy to Clipboard" icon="pi pi-copy" className="p-button-secondary" onClick={copyToClipboard} />
                    <p className="m-0">
                        {summaries}
                    </p>
                    <Button label="Copy to Clipboard" icon="pi pi-copy" className="p-button-secondary" onClick={copyToClipboard} />
                </Fieldset>
            }

            {/* <Button id="historyButton" label="View History" icon="pi pi-clock" /> */}
            <Button id="translateButton" label="Translate Summary" icon="pi pi-globe" />
            {/* <Button id="summarizeButton" label="Summarize Document" icon="pi pi-list" onClick={summarizeArticle} /> */}
        </main>
    );
};

export default MainContent;
