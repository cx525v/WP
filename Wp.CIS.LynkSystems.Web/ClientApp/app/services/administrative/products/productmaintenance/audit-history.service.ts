

import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../../../models/common/app-config-settings.model';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { AuditHistoryModel } from '../../../../models/administration/product/productmaintenance/audit-history.model';
import { ActionTypeEnum } from '../../../../models/common/action-type.enum';
import { LidTypesEnum } from '../../../../models/common/lid-types.enum';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class AuditHistoryService {

    private webApiUrl: string;

    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}AuditHistory`;
    }

    /**
     * @method This is used to retrieve all of the products.
     * @param terminalNumber 
     */
    public getLatestAuditHistory(lidType: LidTypesEnum, lid: number, actionType: ActionTypeEnum): Observable<AuditHistoryModel> {

        let theWebUrl: string = `${this.webApiUrl}?lidType=${lidType}&lid=${lid}&actionType=${actionType}`;

        let theObservable: Observable<AuditHistoryModel> = this._http.get(theWebUrl).map(r => r.json());

        return theObservable;
    }
}