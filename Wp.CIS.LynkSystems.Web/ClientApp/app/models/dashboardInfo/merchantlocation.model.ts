import { Pagination } from './pagination.model';

export class MerchantPage extends Pagination {
    FilterMID?: string;
    FilterName?: string;
    FilterState?: string;
    FilterZipCode?: string;
    FilterStatusIndicator?: string;
}

export class MerchantFilter {
    lidTypeEnum: number;
    LIDValue: string;
    Page: MerchantPage;
}