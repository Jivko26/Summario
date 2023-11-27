// BingSearchModels.ts
export interface BingSearchResponse {
    webPages: WebPageResults;
    // Include other properties like images, videos, etc., as needed
}

export interface WebPageResults {
    value: WebPage[];
}

export interface WebPage {
    name: string;
    url: string;
    snippet: string;
    dateLastCrawled: string;
}
