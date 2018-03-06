
import { LidTypesEnum } from '../../models/common/lid-types.enum';

export class PrimaryKeyInfoGenericModel {

    constructor(public lidId: number,
        public lidType: LidTypesEnum,
        public customerId: number,
        public merchantId: number,
        public terminalNbr: number) {

    }
}