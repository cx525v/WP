import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { Transaction, TransactionFilter } from '../../models/dashboardInfo/transaction.model';
import { apiResponse } from '../../models/dashboardInfo/apiResponse.model';

declare var gAppConfigSettings: IAppConfigSettings;
@Injectable()
export class TransactionService {
    constructor(private http: Http) {

    }

    public getTransactionHistory(data: TransactionFilter): Observable<apiResponse<Transaction>> {
        var url = gAppConfigSettings.WebApiUrl +'TransactionHistory';
        return this.http.post(url, data).map(r => {          
            return r.json();
        },
            (error) => {
                console.log(error);
            });
    }
}