﻿
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../../../models/common/app-config-settings.model';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { DownloadTimeModel } from '../../../../models/administration/product/productmaintenance/download-time.model';

declare var gAppConfigSettings: IAppConfigSettings;


@Injectable()
export class DownloadTimesService {

    private webApiUrl: string;

    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}DownloadTimes`;
    }

    /**
     * @method This is used to retrieve all of the products.
     * @param terminalNumber 
     */
    public getAllDownloadTimes(): Observable<Array<DownloadTimeModel>> {

        let theObservable: Observable<Array<DownloadTimeModel>> = this._http.get(this.webApiUrl).map(r => r.json());

        return theObservable;
    }
}