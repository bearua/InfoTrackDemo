import {ContactLocation} from "./data-objects/contact-location";
import {BaseService} from "./base-service";

export class locationsService extends BaseService {
    public async getLocations(): Promise<ContactLocation[]> {
        return this.get<ContactLocation[]>('locations');
    }

    public async searchLocations(query: string): Promise<ContactLocation[]> {
        return await this.get<ContactLocation[]>('locations/search?query=' + query);
    }

    public async addLocation(item: ContactLocation): Promise<ContactLocation> {
        return await this.post<ContactLocation, {}>(`locations?title=${item.title}&text=${item.text}`, {});
    }

    public async deleteLocation(item: ContactLocation): Promise<string> {
        return await this.delete<string, {}>(`locations?title=${item.title}`, {});
    }
}