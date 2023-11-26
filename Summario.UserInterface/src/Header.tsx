
import { useState } from 'react';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';

import './styles.css';

const Header = () => {
    const [searchVisible, setSearchVisible] = useState(false);
    const [searchQuery, setSearchQuery] = useState('');


    const toggleSearch = () => {
        setSearchVisible(!searchVisible);
    };


    const handleSearchChange = (e: any) => {
        setSearchQuery(e.target.value);
    }


    const searchFile = async () => {

        try {
            const response = await fetch(`https://localhost:7095/api/Summarizer/SearchFile?fileName=${encodeURIComponent(searchQuery)}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            //Prod
            // const response = await fetch(`http://87.215.96.234:80/api/Summarizer/SearchFile?fileName=${encodeURIComponent(fileName)}`, {
            //     method: 'GET',
            //     headers: {
            //         'Content-Type': 'application/json'
            //     }
            // });


            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }

            console.log("Response", response);


            const result = response.url;
            console.log("Result", result);
            alert(`Search result is: ${result}`);
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    };

    return (
        <header>
            <h1>Scientific Paper Summarizer</h1>
            <div className="search-container">
                <InputText
                    id="searchInput"
                    placeholder="Search for papers"
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                />
                <Button
                    id="searchButton"
                    label={`Search`}
                    icon="pi pi-search"
                    onClick={searchFile}
                    onKeyPress={(e) => e.key === 'Enter' && searchFile()}
                />
            </div>
        </header>
    );
};

export default Header;
