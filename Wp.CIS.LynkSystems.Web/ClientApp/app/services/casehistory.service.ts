import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../models/common/app-config-settings.model';
import { LidTypesEnum } from '../models/common/lid-types.enum';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class CaseHistoryService {
    errorMessage: string;
    mode = 'Observable';
    constructor(private http: Http) {

    }
    public getCaseHistory(LidType: LidTypesEnum, Lid: string): Observable<any[]> {

        const caseHistoryUrl = gAppConfigSettings.WebApiUrl + 'casehistory?LIDType=' + LidType + "&LID=" + Lid;

        return this.http.get(caseHistoryUrl).map(r => r.json());
    }
}

