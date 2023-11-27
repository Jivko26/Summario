
import { useState } from 'react';
import { Button } from 'primereact/button';
import { FileUpload } from 'primereact/fileupload';
import { ProgressSpinner } from 'primereact/progressspinner';
import { Fieldset } from 'primereact/fieldset';

import '../styles.css';
import { InputText } from 'primereact/inputtext';


const MainContent = () => {
    const [isMainVisible, setIsMainVisible] = useState<boolean>(false);
    const [searchQuery, setSearchQuery] = useState('');
    const [uploadedFile, setUploadedFile] = useState(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [summaries, setSummaries] = useState<string>("");
    const [summariesArray, setsummariesArray] = useState<string>("");
    const [translation, setTranslation] = useState<any>();


    const onUpload = (e: any) => {
        console.log(e.files);
        setUploadedFile(e.files[0]);
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

    const clear = async () => {
        setIsLoading(true);
        setSearchQuery("");
        setIsLoading(false);
    };


    const searchFile = async () => {

        setIsLoading(true);

        try {
            //Prod
            // const response = await fetch(`http://87.215.96.234:80/api/Summarizer/SearchFile?fileName=${encodeURIComponent(fileName)}`, {
            //     method: 'GET',
            //     headers: {
            //         'Content-Type': 'application/json'
            //     }
            // });

            const response = await fetch(`https://localhost:7095/api/search?query=${encodeURIComponent(searchQuery)}`);

            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }

            console.log("Response", response);


            const result = response.json();
            console.log("Result", result);
            alert(`Search result is: ${result}`);
            setIsMainVisible(false);
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }

        setIsLoading(false);
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
            setsummariesArray(summaries);

        } catch (error) {
            alert(`An error occured: ${error}`);
            console.error('There was a problem with the fetch operation:', error);
        }

        setIsLoading(false);
    };

    const translateSummary = async () => {
        setIsLoading(true);


        try {
            const response = await fetch(`https://localhost:7095/api/translation?summary=${encodeURIComponent(summariesArray)}`);

            //Prod
            // const response = await fetch('http://87.215.96.234:80/api/Summarizer/SummarizeArtilce', {
            //     method: 'POST',
            //     body: formData
            // });

            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }

            const translation = await response.json();
            console.log("Translation: ", translation);
            setTranslation(translation);

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
            <div className="search-container">
                <span className="p-input-icon-right">
                    <InputText
                        placeholder="Search the web for articles and summaries"
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                        disabled={isLoading}
                        style={{
                            width: "700px",
                        }}
                    />
                    <i
                        className="pi pi-times"
                        onClick={() => {
                            clear();
                        }}
                    ></i>
                </span>
                <Button
                    label={isLoading ? "Searching..." : "Search"}
                    onClick={searchFile}
                    disabled={isLoading}
                    style={{
                        marginLeft: "10px",
                        backgroundColor: "rgb(39, 12, 189)",
                    }}
                />
                <Button
                    label={"Upload Article"}
                    disabled={isLoading}
                    style={{
                        marginLeft: "10px",
                        backgroundColor: "rgb(39, 12, 189)",
                    }}
                    onClick={() => {
                        setIsMainVisible(true);
                    }}
                />
            </div>
            {isMainVisible &&
                <>
                    <div className="card">
                        <FileUpload
                            name="demo[]"
                            multiple
                            accept=".pdf,.docx"
                            maxFileSize={1000000}
                            emptyTemplate={<p className="m-0">Drag and drop files here to upload.</p>}
                            customUpload={true}
                            onRemove={() => {
                                setSummaries("");
                                setTranslation("");
                            }}
                            onClear={() => {
                                setIsMainVisible(false);
                                setSummaries("");
                                setTranslation("");
                            }}
                            uploadLabel='Summarize'
                            onSelect={onUpload}
                            uploadHandler={summarizeArticle}
                        />
                    </div>
                    <div style={{ display: 'flex', justifyContent: 'space-around', flexWrap: 'wrap' }}>
                        {summaries &&
                            <Fieldset legend="Summary" style={{ flex: '1', minWidth: '300px', margin: '10px' }}>
                                <Button
                                    label="Copy to Clipboard"
                                    icon="pi pi-copy"
                                    className="p-button-secondary"
                                    onClick={copyToClipboard}
                                    style={{
                                        marginLeft: "10px",
                                        backgroundColor: "rgb(39, 12, 189)",
                                    }} />
                                <Button
                                    id="translateButton"
                                    label="Translate Summary"
                                    icon="pi pi-globe"
                                    onClick={translateSummary}
                                    style={{
                                        marginLeft: "10px",
                                        backgroundColor: "rgb(39, 12, 189)",
                                    }} />
                                <p className="m-0">
                                    {summaries}
                                </p>
                                <Button label="Copy to Clipboard" icon="pi pi-copy" className="p-button-secondary" onClick={copyToClipboard} />
                                <Button id="translateButton" label="Translate Summary" icon="pi pi-globe" onClick={translateSummary} />
                            </Fieldset>
                        }
                        {translation &&
                            <Fieldset legend="Translation" style={{ flex: '1', minWidth: '300px', margin: '10px' }}>
                                <Button
                                    label="Copy to Clipboard"
                                    icon="pi pi-copy"
                                    className="p-button-secondary"
                                    onClick={copyToClipboard}
                                    style={{
                                        marginLeft: "10px",
                                        backgroundColor: "rgb(39, 12, 189)",
                                    }} />
                                <p className="m-0">
                                    {translation[0].translations[0].text}
                                </p>
                                <Button
                                    label="Copy to Clipboard"
                                    icon="pi pi-copy"
                                    className="p-button-secondary"
                                    onClick={copyToClipboard}
                                    style={{
                                        marginLeft: "10px",
                                        backgroundColor: "rgb(39, 12, 189)",
                                    }} />
                            </Fieldset>
                        }
                    </div>
                </>
            }
        </main>
    );
};

export default MainContent;
