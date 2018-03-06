import {Pagination } from './pagination.model';
export class Contacts {
    id: string;
    contact: string;
    addressType: string;
    lastFour: string;
}

export class ContactPage extends Pagination {
    FilterContact: string;
    FilterRole: string;
    FilterLast4: string;  
}

export class ContactFilter{
    lidTypeEnum: number;
    LIDValue: string;
    Page: ContactPage;
}