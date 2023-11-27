
import { Card } from 'primereact/card';

const SearchResultCard = ({ result }: any) => {
    return (
        <Card title={result.name}>
            <p>{result.snippet}</p>
            <a href={result.url} target="_blank" rel="noopener noreferrer">Read more</a>
            {/* You can add more details like dateLastCrawled here */}
        </Card>
    );
};

export default SearchResultCard;