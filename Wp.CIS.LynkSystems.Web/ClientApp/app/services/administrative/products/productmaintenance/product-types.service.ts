
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../../../models/common/app-config-settings.model';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { ProductTypeModel } from '../../../../models/administration/product/productmaintenance/product-type.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class ProductTypesService {

    private webApiUrl: string;

    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}ProductTypes`;
    }

    /**
     * @method This is used to retrieve all of the products.
     * @param terminalNumber 
     */
    public getAllProductTypes(): Observable<Array<ProductTypeModel>> {

        let theObservable: Observable<Array<ProductTypeModel>> = this._http.get(this.webApiUrl).map(r => r.json());

        return theObservable;
    }
}