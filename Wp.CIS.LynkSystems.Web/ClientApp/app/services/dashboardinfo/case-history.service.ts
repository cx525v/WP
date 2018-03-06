
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';

import { LidTypesEnum } from '../../models/common/lid-types.enum';

import { GenericPaginationResponse } from '../../models/transactions/genericPaginationResponse.model'
import { CaseHistory } from '../../models/dashboard.model'
import { PaginationCaseHistoryModel } from '../../models/caseHistory/pagination-case-history.model';

import { CaseHistoryInputModel } from '../../models/caseHistory/case-history-input.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class CaseHistoryService {

    private _webApiUrl: string;

    constructor(private _http: Http) {

        this._webApiUrl = `${gAppConfigSettings.WebApiUrl}CaseHistory`;
    }

    public getPageOfCaseHistoryRecords(input: CaseHistoryInputModel): Observable<GenericPaginationResponse<CaseHistory>> {


        //let body = JSON.stringify(pagingModel);
        let theObservable: Observable<GenericPaginationResponse<CaseHistory>> =
        this._http
            .post(this._webApiUrl, input)
            .map(r => r.json());

        return theObservable;
    }
}