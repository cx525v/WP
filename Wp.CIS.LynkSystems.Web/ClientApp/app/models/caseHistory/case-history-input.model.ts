
import { PaginationCaseHistoryModel } from './pagination-case-history.model';
import { ClientInputBaseModel } from '../common/client-input-base.model';
import { LidTypesEnum } from '../common/lid-types.enum';

export class CaseHistoryInputModel extends ClientInputBaseModel {

    constructor() {

        super();
    }

    public page: PaginationCaseHistoryModel;
    public extraID: string;
}