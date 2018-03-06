import {Pagination } from './pagination.model';

export class TerminalEquipment {
    mid?: string;
    terminalNbr: number;
    terminalID: string;
    equipment: string;
    software: string;
    deactivateActivateDate: string;
    status: string;
}

export class TerminalPage extends Pagination{
    FilterDate?: string;
    FilterSoftware?: string;
    FilterStatus?: string;
    FilterStatusEquipment?: string;
    FilterTID?: number;
}

export class TerminalFilter {
    lidTypeEnum: number;
    LIDValue: string;
    Page: TerminalPage;
}