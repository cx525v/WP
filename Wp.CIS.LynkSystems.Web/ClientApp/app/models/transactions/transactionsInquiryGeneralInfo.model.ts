
export class TransactionsInquiryGeneralInfo {

    constructor(
        public customerNbr: string,
        public merchantNbr: string,
        public address: string,
        public city: string,
        public state: string,
        public zipcode: string,
        public sicCode: number,
        public sicDesc: string,
        public name: string,
        public services: string,
        public statusDesc: string,
        public businessDesc: string,
        public lastDepositDate: Date,
        public consolidation: string,
        public sensitivitylevel: number,
        public istoptier: boolean,
        public customerId: number,
        public lidType: number    ) {

    }
}