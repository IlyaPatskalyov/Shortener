export class FetcherError {
    public response: Response;
    public innerError: any;
    public message: string;

    constructor(message: string, response: Response, innerError?: any) {
        this.message = message;
        this.response = response;
        this.innerError = innerError;
    }
}

export class Fetcher {

    public static postJSON<T>(url: string, data: any): Promise<T> {
        return this.execute(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
    }

    public static post<T>(url: string): Promise<T> {
        return this.execute<T>(url, {
            method: "POST",
            headers: {},
            body: "x"
        });
    }

    public static get<T>(url: string): Promise<T> {
        return this.execute(url);
    }

    private static async execute<T>(url: RequestInfo, init?: RequestInit): Promise<T> {
        init = init || {};
        init.credentials = "same-origin";

        let response = await window.fetch(url, init);
        if (response.status < 200 || response.status >= 300)
            throw new FetcherError(await response.text(), response);
        if (response.status == 204 || response.status == 1223)
            return null;

        let contentType = response.headers.get("content-type");
        if (!contentType)
            throw new FetcherError(`Response ${response.url} has no contentType`, response);
        if (contentType.indexOf("application/json") != -1)
            return response.json();
        if (contentType.indexOf("text/plain") != -1)
            return response.text() as any;

        throw new FetcherError(`Response ${response.url} has unsupported contentType ${contentType}`, response);
    }
}


