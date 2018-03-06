
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../../../models/common/app-config-settings.model';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { ManufacturerModel } from '../../../../models/administration/product/productmaintenance/manufacturer.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class ManufacturersService {

    private webApiUrl: string;

    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}Manufacturers`;
    }

    /**
     * @method This is used to retrieve all of the products.
     * @param terminalNumber 
     */
    public getAllManufacturers(): Observable<Array<ManufacturerModel>> {

        let theObservable: Observable<Array<ManufacturerModel>> = this._http.get(this.webApiUrl).map(r => r.json());

        return theObservable;
    }
}