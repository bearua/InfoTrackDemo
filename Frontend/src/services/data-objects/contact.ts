export interface Contact
{
    id: number;
    name: string | undefined;
    location: string;
    address: string | null;
    phone: string | null;
    starsCount: number | null;
    votesCount: number | null;
    isNew: boolean | null;
}

