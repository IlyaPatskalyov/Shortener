import {Fetcher} from "../core/Fetcher";

export class LinksApi {

    static add(url: string): Promise<string>{
        return Fetcher.postJSON<string>("/api/links/add", url);
    }

    static getMyLinks(): Promise<Link[]> {
        return Fetcher.get<Link[]>("/api/links");
    }
}