

export interface BankingInformation {
    activityAcctTypeDescription: string;
    bankAcctNbr: string;
    bankRTNbr: string;
}

export interface DashboardInfo {
    custProfile: CustomerProfile;
    groupInfo: GroupInfo;
    actvServices: ActiveServices;
    merchInfo?: IMerchantInfo;
    termProfile: TerminalProfileData;
    demographicsInfo: DemographicsInfo[];
    demographicsInfoCust: DemographicsInfo[];
    demographicsInfoMerch: DemographicsInfo[];
    demographicsInfoTerm: DemographicsInfo[];
    merchantsList: MerchantLocation[];
    caseHistorysList: CaseHistory[];
    termInfo?: TermInfo;
    totalNumberOfCaseHistoryRecords?: number;  
    totalMerchantRecords?: number;
    totalDemographicsRecords?: number;
    totalDemographicsInfoCustRecords?: number;
    totalDemographicsInfoMerchRecords?: number;
}

export interface TermInfo {
    activationDt: string;
    busTypeDesc: string;
    businessType: number;
    captureType: number;
    cashAdv: number;
    checkSvc: number;
    commType: number;
    credit: number;
    cspStatusInterval: string;
    customerID: number;
    cutOffTime: string;
    deactivationDt: string;
    debit: number;
    defaultNetwork: number;
    downLoadDate: string;
    ebt: number;
    fleet: number;
    forcedBillingDate: string;
    incrementalDt: string;
    installDate: string;
    merchantId: number;
    merchantName: string;
    originalSO?: number;
    pob: number;
    programType: number;
    sentToStratusDate: string;
    statDesc: string;
    statusIndicator: number;
    suppLA: number;
    terminalId: string;   
}
export interface CustomerProfile {
    customerID: number;
    description: string;
    activationDt: Date;
    statusIndicator: number;
    legalType: number;
    businessEstablishedDate: string;
    lynkAdvantageDate: Date;
    customerNbr: string;
    classCode: number;
    sensitivityLevel: number
    stmtTollFreeNumber: string;
    deactivationDt: Date;
    legalDesc: string;
    senseLvlDesc: string;
    statDesc: string;
    lynkAdvantage: number;
    pinPadPlus: number;
    giftLynk: number;
    rewardsLynk: number;
    demoID: number;
    custName: string;
    custContact: string;
    prinID: number;
    prinName: string;
    prinAddress: string;
    prinCity: string;
    prinState: string;
    prinZipcode: string;
    prinSSN: string;
    custFederalTaxID: string;
    irsverificationStatus: number;
    propHasEmployees: number;

}
export interface GroupInfo {
    groupID: number;
    groupName: string;
    groupType: string;
    statusIndicator: string;
}

export interface ActiveServices {
    lidType?: number;
    lid?: number;
    billingMethodType: number;
    billMtdDesc: string;  
    chkName: string;
    giftLynk_ON?: boolean;
    rewardsLynk_ON?: boolean;
    lastProcessingDt: string;
    amex_ON?: boolean;
    discover_ON?: boolean;
    discover_CT21_ON?: boolean;
    diner_ON?: boolean;
    jcB_ON?: boolean;
    openCase: number;
    terminalRental_ON?: boolean;
    printerRental_ON?: boolean;
    pinPadRental_ON?: boolean;
    softDesc: string;
    creditST_ON?: boolean;
    debitST_ON?: boolean;
    checkST_ON?: boolean;
    achsT_ON?: boolean;
    lynkAdvantage_ON?: boolean;
    externalTermID: string;
    authProcessorDesc: string;  
    sicDesc: string;
    laDesc: string;  
    activeServiesDesc: string;   
}


export interface MerchantProfileData {
    accountnbr: number;
    acqBankdesc: string;
    acqbanknameaddressid: number;
    acquiringbankid: number;
    activationdt: Date;
    benefittype: number;
    benetypedesc: string;
    brandid: number;
    cardtype: number;
    chkacctnbr: number;
    customerid: number;
    deactivationdt: Date;
    descriptorcd: number;
    discountrate: number;
    federaltaxid: number;
    fnsnbr: number;
    highriskind: number;
    incrementaldt: Date;
    industrytype: number;
    industrytypedesc: string;
    interneturl: string;
    irsverificationstatus: number;
    merchantclass: string;
    merchantid: number;
    merchantnbr: string;
    merchanttype: number;
    merchtypedesc: string;
    mvv1: string;
    mvv2: string;
    programtype: number;
    programtypedesc: string;
    risklevel: number;
    riskleveldesc: string;
    risklevelid: number;
    siccode: number;
    siccodedesc: string;
    statdesc: string;
    statetaxcode: number;
    statusindicator: number;
    storenbr: number;
    subindgrpdesc: string;
    subindgrpid: number;
    thresholddt: Date;
    visaindustrytype: number;
}

export interface IMerchantInfo {
    merchantId: number;
    customerID: number;
    activationDt: Date;
    sicCode: number;
    industryType: number;
    merchantNbr: string;
    acquiringBankId: number;
    programType: number;
    statusIndicator: number;
    fnsNbr: string;
    benefitType: number;
    riskLevelID: number;
    merchantType: number;
    benefitTypeDesc?: string;
    internetURL: string;
    deactivationDt: Date;
    incrementalDt: Date;
    thresholdDt: Date;
    brandID: number;
    sicDesc: string;
    merchantClass: string;
    riskLevel: string;
    statDesc: string;
    indTypeDesc: string;
    mchName: string;
    mchAddress: string;
    mchCity: string;
    mchState: string;
    mchZipCode: string;
    mchPhone: string;
    mchContact: string;
    acquiringBank: string;
    merchFedTaxID: string;
}

export interface TerminalProfileData {
    terminalId: string;
    merchantId: number;
    CardType: number;
    AccountNbr: number;
    DiscountRate: number;
    CustomerID: number;
    ActivationDt: Date;
    SicCode: number;
    IndustryType: number;
    MerchantNbr: string;
    AcquiringBankId: number;
    ProgramType: number;
    StatusIndicator: number;
    FNSNbr: number;
    BenefitType: number;
    RiskLevelID: number;
    MerchantType: number;
    InternetURL: string;
    DeactivationDt: Date;
    IncrementalDt: Date;
    ThresholdDt: Date;
    BrandID: number;
    SicCodeDesc: string;
    AcqBankNameAddressID: number;
    AcqBankDesc: string;
    IndustryTypeDesc: string;
    ProgramTypeDesc: string;
    DescriptorCd: number;
    VisaIndustryType: number;
    HighRiskInd: number;
    BeneTypeDesc: string;
    StatDesc: string;
    MerchantClass: string;
    RiskLevel: number;
    RiskLevelDesc: string;
    ChkAcctNbr: number;
    FederalTaxID: number;
    StateTaxCode: number;
    MerchTypeDesc: string;
    SubIndGrpID: number;
    SubIndGrpDesc: string;
    MVV1: string;
    MVV2: string;
    StoreNbr: number;
    IRSVerificationStatus: number;
    CutOffTime: number;
}

export interface DemographicsInfo {
    level: string;
    addressTypeID: number;
    nameAddressID: number;
    addressType: string;
    name: string;
    address: string;
    address2: string;
    city: string;
    state: string;
    zipCode: string;
    zipCode4: string;
    county: string;
    phone: string;
    fax: string;
    contact: string;
    email: string;
    title: string;
    dob: string;
    ssn: string;
    lastFour: string;
}

export interface CaseHistory {
    caseLevel: string,
    caseDesc: string,
    caseId: number;
    createDate: Date;
    description: string;
    caseDescId: number;
    deptName: string;
    terminalId: string;
    merchantId: number;
    merchantNbr: string;
    customerNbr: string;
    merchantName: string;
    currDept: string;
    referredFrom: string;
    priorityId: number;
    closedDate: Date;
    rtnToOriginator: boolean;
    hasAttachment: boolean;
    hasCustomForm: boolean;
    hasReminder: boolean;
    parentCaseId: number;
    caseStatusId: number;
    hasEscalated: boolean;
    isCaseOpen: boolean;
    orgDeptName: string;
}


export class MerchantLocation {
    customerID: number;
    mid: string;
    name: string;
    state: string;
    zipCode: string;
    statusIndicator: string;
}

