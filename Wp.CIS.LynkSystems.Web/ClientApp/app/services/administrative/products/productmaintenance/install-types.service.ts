
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../../../models/common/app-config-settings.model';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { InstallTypeModel } from '../../../../models/administration/product/productmaintenance/install-type.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class InstallTypesService {

    private webApiUrl: string;

    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}InstallTypes`;
    }

    /**
     * @method This is used to retrieve all of the products.
     * @param terminalNumber 
     */
    public getAllInstallTypes(): Observable<Array<InstallTypeModel>> {

        let theObservable: Observable<Array<InstallTypeModel>> = this._http.get(this.webApiUrl).map(r => r.json());

        return theObservable;
    }
}