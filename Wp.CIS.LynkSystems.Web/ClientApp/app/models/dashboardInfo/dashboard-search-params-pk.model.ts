

//dashboard - search - params - pk
import { LidTypesEnum } from '../common/lid-types.enum';

export class DashboardSearchParamsPk {

    constructor(public lidType: LidTypesEnum,
        public lidIdPk: number,
        public customerId: number,
        public merchantId: number,
        public terminalNbr: number) {

    }
}