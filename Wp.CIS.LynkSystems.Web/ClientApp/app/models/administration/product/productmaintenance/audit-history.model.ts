
import { ActionTypeEnum } from '../../../common/action-type.enum';
import { LidTypesEnum } from '../../../common/lid-types.enum';

export class AuditHistoryModel {

    public lidType: LidTypesEnum;

    public lid: number;

    public actionType: ActionTypeEnum;

    public actionDate: Date;

    public userName: string;

    public notes: string;

    public auditId: number;

}