
import { LidTypesEnum } from '../common/lid-types.enum';
import { ActionTypeEnum } from '../common/action-type.enum';

export class LidPrimeyKeyEventModel {

    constructor(public lidType: LidTypesEnum,
        public primaryKey: number,
        public searchString: string) {

    }
}
