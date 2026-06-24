import {BaseService} from "./base-service";
import {Contact} from "./data-objects/contact";
import {UpdateContactsResult} from "./data-objects/update-contacts-result";

export class contactsService extends BaseService
{
    public async getContacts(): Promise<Contact[]>
    {
        return this.get<Contact[]>('contacts');
    }

    public async updateLocation(location: string): Promise<UpdateContactsResult>
    {
        return await this.post<UpdateContactsResult, {}>(`contacts/update?location=${location}`, {});
    }
}