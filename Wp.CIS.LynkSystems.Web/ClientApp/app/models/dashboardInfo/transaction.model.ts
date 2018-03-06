import { Pagination } from './pagination.model';
export enum TransactionType {
    Settled,
    Acquired
}

export class Transaction {
    reQ_BUS_DATE: string;
    reQ_AMT: number;
    reQ_PAN_4: string;
    reQ_TRAN_TYPE: string;
    resP_NETWRK_AUTH_CD: string;
    descript: string;
}

export class TransactionPage extends Pagination {
    FilterByDate?: string;
    FilterByAmount?: string;
    FilterByLast4?: string;
    FilterByTranType?: string;
    FilterByNetworkCD?: string;
    FilterByDesc?: string;
    TransactionType: TransactionType
}

export class TransactionFilter {
    lidTypeEnum: number;
    LIDValue: string;
    Page?: TransactionPage;
}