export class BaseService{
    readonly baseUrl: string;

    constructor() {
        this.baseUrl = "http://localhost:5268/";
    }

    private async request<T>(endpoint: string, options?: RequestInit): Promise<T> {
        const url = `${this.baseUrl}${endpoint}`;

        const headers = {
            'Content-Type': 'application/json',
            ...(options?.headers || {}),
        };

        const response = await fetch(url, {...options, headers});

        // Explicitly catch and isolate HTTP error codes
        if (!response.ok) {
            throw new Error(`HTTP Error: ${response.status} - ${response.statusText}`);
        }
        return await response.json() as Promise<T>;
    }

    public async get<T>(endpoint: string): Promise<T> {
        return this.request<T>(endpoint, { method: 'GET' });
    }

    public async post<T, U>(endpoint: string, body: U): Promise<T> {
        return this.request<T>(endpoint, {
            method: 'POST',
            body: JSON.stringify(body),
        });
    }
    public async delete<T, U>(endpoint: string, body: U): Promise<T> {
        return this.request<T>(endpoint, {
            method: 'DELETE',
            body: JSON.stringify(body),
        });
    }

}