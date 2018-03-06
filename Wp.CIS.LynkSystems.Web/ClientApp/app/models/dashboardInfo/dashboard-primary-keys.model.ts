
import { LidTypesEnum } from '../common/lid-types.enum'

export class DashboardPrimaryKeysModel {

    terminalNbr: number;

    merchantID: number;

    customerID: number;

    convertedLidPk: number;

    recordFound: boolean;

    lidType: LidTypesEnum;

 }