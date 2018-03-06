
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../../../models/common/app-config-settings.model';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { ProductBrandModel } from '../../../../models/administration/product/productmaintenance/product-brand.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class BrandService {

    private webApiUrl: string;

    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}ProductBrands`;
    }

    /**
     * @method This is used to retrieve all of the products.
     * @param terminalNumber 
     */
    public getProductBrands(): Observable<Array<ProductBrandModel>> {


        let theObservable: Observable<Array<ProductBrandModel>> = this._http.get(this.webApiUrl).map(r => r.json());

        return theObservable;
    }
}