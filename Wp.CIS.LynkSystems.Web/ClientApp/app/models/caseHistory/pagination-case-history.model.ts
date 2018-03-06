
import { PaginationModel } from '../common/pagination.model';

export class PaginationCaseHistoryModel extends PaginationModel {

    constructor() {

        super();
    }

    public filterCaseId: string;
    public filterCaseDesc: string;
    public filterOrgDeptName: string;
    public filterCaseLevel: string;
    public filterCreateDate: Date;
}