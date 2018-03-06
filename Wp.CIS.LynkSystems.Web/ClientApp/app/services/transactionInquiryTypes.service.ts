
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { TransactionInquiryTypes } from '../models/transactions/transactionInquiryTypes.model';

import { IAppConfigSettings } from '../models/common/app-config-settings.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class TransactionInquiryTypesService {

    /**
     * @field This will hold the base address for the web api that will be called by this service
     */
    private webApiUrl: string;

    /**
     * @constructor This initializes the class
     * @param _http This is used to make calls to the web api
     */
    constructor(private _http: Http) {

        //this.webApiUrl = `${resources.DEV_BASESERVER}TransactionInquiryTypes`;
        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}TransactionInquiryTypes`;
    }

    /**
     * @method This is used to retrieve all of the active transaction types.
     * @returns An array of TransactionInquiryTypes
     */
    public getAllActiveTransactionInquiryTypes(): Observable<Array<TransactionInquiryTypes>> {

        let theObservable: Observable<Array<TransactionInquiryTypes>> = this._http.get(this.webApiUrl).map(r => r.json());

        return theObservable;
    }
}