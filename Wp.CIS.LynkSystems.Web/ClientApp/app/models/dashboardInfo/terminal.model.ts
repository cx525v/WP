export class SensitivityInfo {
    sensitivityLevel: number;
    senLevelDesc: string;
}
export class TerminalDetail {   
    tid: string;
    terminalEquipment: string;
    softDesc : string;
    statDesc : string;
    activationDt : string;
    deactivationDt : string;
    activeServiesDesc : string;
    billMtdDesc: string;
    terminalType: string;
}

import {TermInfo, ActiveServices} from '../dashboard.model';
export class TerminalDetails {
    amex: number;
    amexCanx: number;
    autoSettleIndicator: string;
    autoSettleOverride: string;
    autoSettleTime: string;
    checkSvc: number;
    credit: number;
    cutOffTime: string;
    debit: number;
    diners: number;
    discCanx: number;
    discover: number;
    ebt: number;
    giftLynk: number;
    lynkAdvantageDesc: string;
    pob: number;
    revPip: number;
    rewardsLynk: number;
    terminalDescription: string;
    tid: string;
    timeZone: string;
    visaMC: number;
    terminalType: string;

}

export class TerminalSettlementInfo {
    grossAmt: number;
    nbrOfTrans: number;
}

export class Terminal {
    terminalDetails: TerminalDetails;
    activeServices: ActiveServices;
    terminalInfo: TermInfo;
    sensitivityInfo?: SensitivityInfo
    services?: string;
}