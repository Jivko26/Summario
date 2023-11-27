import SearchResultCard from "./SearchResultCard";

const SearchResults = ({ results }: any) => {
    return (
        <div>
            {results.map((result: any, index: any) => (
                <SearchResultCard key={index} result={result} />
            ))}
        </div>
    );
};

export default SearchResults;