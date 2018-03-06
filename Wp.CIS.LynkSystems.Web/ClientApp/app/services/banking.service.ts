import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../models/common/app-config-settings.model';
import { Observable } from "rxjs/Rx";
import 'rxjs/add/operator/map';

import { LidTypesEnum } from '../models/common/lid-types.enum';
import { BankingInfoModel } from '../models/bankingInfo/banking-info.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class BankingService {


    constructor(private http: Http) {

    }

    public getBankingInfo(LidType: LidTypesEnum, Lid: string): Observable<BankingInfoModel[]> {

        const bankingUrl = gAppConfigSettings.WebApiUrl + `Banking/GetBankingInfo?LIDType=${LidType}&LID=${Lid}`;

        let response: Observable<BankingInfoModel[]> = this.http.get(bankingUrl).map(r => r.json());

        return response;
    }
}
