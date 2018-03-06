
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../../../models/common/app-config-settings.model';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { MobileLookupModel } from '../../../../models/administration/product/productmaintenance/mobile-lookup.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class MobileLookupService {

    private webApiUrl: string;

    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}MobileLookup`;
    }

    /**
     * @method This is used to retrieve all of the products.
     * @param terminalNumber 
     */
    public getAllMobileLookups(): Observable<Array<MobileLookupModel>> {

        let theObservable: Observable<Array<MobileLookupModel>> = this._http.get(this.webApiUrl).map(r => r.json());

        return theObservable;
    }
}