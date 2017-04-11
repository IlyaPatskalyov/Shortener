import {Fetcher} from "../core/Fetcher";
export interface Link {
    Id: string;
    UserId: string;
    Created: string;
    Modified: string;
    Key: string;
    Url: string;
    CountOfRedirects: number;

}

export class LinksApi {

    static add(url: string): Promise<string>{
        return Fetcher.postJSON<string>("/api/links/add", url);
    }

    static getMyLinks(): Promise<Link[]> {
        return Fetcher.get<Link[]>("/api/links");
    }
}