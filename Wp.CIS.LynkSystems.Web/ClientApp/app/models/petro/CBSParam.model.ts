export class CBSParam {
    parameterID: number;
    paramName: string;
    parameterDesc: string;
    dataType: string;
    numVal?: number;
    bitVal?: boolean;
    stringVal: string;
    isCardSpecific?: number;
    pdl: boolean;
    useSpace: boolean;
    isStratus: boolean;
    isVericentre: boolean;
    stratusMultiplier?: number;
    isCustomerDefault?: boolean;
    isOLADefault?: boolean;
}