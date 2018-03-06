

import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';


import { IAppConfigSettings } from '../models/common/app-config-settings.model';

import { TransactionsInquiryGeneralInfo } from '../models/transactions/transactionsInquiryGeneralInfo.model';
import { GenericPaginationResponse } from '../models/transactions/genericPaginationResponse.model';
import { TransactionsInquiry } from '../models/transactions/transactionsInquiry.model';

declare var gAppConfigSettings: IAppConfigSettings;


@Injectable()
export class TransactionService {

    private webApiUrl: string;

    /**
     * @constructor This provides initialization for the service
     * @param _http This will be used to make calls to the Web API
     */
    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}transaction`;

    }

    /**
     * @method This is used to retrieve the general information for a terminal.
     * @param terminalNumber 
     */
    public getTerminalInfo(terminalNumber: string): Observable<TransactionsInquiryGeneralInfo> {


        let webApiUrlLocal: string = `${gAppConfigSettings.WebApiUrl}TransactionsInquiryGeneralInfo`;
        let webApiPath: string = `${webApiUrlLocal}?id=${terminalNumber}`;

        let theObservable: Observable<TransactionsInquiryGeneralInfo> = this._http.get(webApiPath).map(r => r.json());

        return theObservable;

    }

    /**
     * @method This is used to retrieve a page of transaction records
     * @param terminalId This is the terminal id for the transaction records
     * @param beginDate this is the begin date for the date range of the transaction records
     * @param endDate This is the end date for the date range of the transaction records
     * @param searchCriteriaId This is the id of the search criteria
     * @param firstSix This is the first six numbers of the credit card number
     * @param lastFour This is the last four numbers of the credid card number
     * @param batchNumber This holds the batch number that the transaction was processed in
     * @param skip This is the number of records to skip when returning a page of records
     * @param take This is the maximum records to return in the batch
     * @returns This is an observable paging response. It returns the paging information.
     */
    public getTransactionRecordsForSearch(
        transGenInfo: TransactionsInquiryGeneralInfo,
        terminalId: string,
        beginDate: Date,
        endDate: Date,
        searchCriteriaId: number,
        firstSix: string,
        lastFour: string,
        cardType: string,
        batchNumber: string,
        skip: number,
        take: number): Observable<GenericPaginationResponse<TransactionsInquiry>> {


        let theMonth: number = beginDate.getMonth() + 1;
        let beginDateFormmated: string = `${theMonth}-${beginDate.getDate()}-${beginDate.getFullYear()}`;
        theMonth = endDate.getMonth() + 1;
        let endDateFormatted: string = `${theMonth}-${endDate.getDate()}-${endDate.getFullYear()}`;

        let webApiUrlPath: string = `${gAppConfigSettings.WebApiUrl}TransactionsInquiry`;
        let webApiPath: string = `${webApiUrlPath}?customerNbr=${transGenInfo.customerNbr}&merchantNbr=${transGenInfo.merchantNbr}&address=${transGenInfo.address}`
            + `&city=${transGenInfo.city}&state=${transGenInfo.state}&zipcode=${transGenInfo.zipcode}`
            + `&sicCode=${transGenInfo.sicCode}&sicDesc=${transGenInfo.sicDesc}&name=${transGenInfo.name}&services=${transGenInfo.services}`
            + `&statusDesc=${transGenInfo.statusDesc}&businessDesc=${transGenInfo.businessDesc}&lastDepositDate=${transGenInfo.lastDepositDate}`
            + `&consolidation=${transGenInfo.consolidation}`
            + `&sensitivitylevel=${transGenInfo.sensitivitylevel}&istoptier=${transGenInfo.istoptier}&customerId=${transGenInfo.customerId}`
            + `&lidType=${cardType}&TerminalNbr=${terminalId}&startDate=${beginDateFormmated}&endDate=${endDateFormatted}`
            + `&SearchId=${searchCriteriaId}&CardNo=${firstSix}${lastFour}&BatchNo=${batchNumber}&SkipRecords=${skip}&PageSize=${take}`;

        let theObservable: Observable<GenericPaginationResponse<TransactionsInquiry>> = this._http.get(webApiPath).map(r => r.json());

        return theObservable;

    }
}

