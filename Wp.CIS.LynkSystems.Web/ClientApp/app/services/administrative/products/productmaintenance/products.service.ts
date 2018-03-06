
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../../../models/common/app-config-settings.model';
import { Observable } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { ProductModel } from '../../../../models/administration/product/productmaintenance/product.model';
import { ProductLookupValuesModel } from '../../../../models/administration/product/productmaintenance/product-lookup-values.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class ProductsService {

    private webApiUrl: string;

    constructor(private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}products`;
    }

    /**
     * @method This is used to retrieve all of the products.
     * @param terminalNumber 
     */
    public getAllProducts(): Observable<Array<ProductModel>> {

        let theObservable: Observable<Array<ProductModel>> = this._http.get(this.webApiUrl).map(r => r.json());

        return theObservable;
    }

    /**
      * @method This is used to retrieve all of the products.
      * @param terminalNumber 
      */
    public getAllProductsWithPaging(offset: number,
        pageSize: number,
        sortField: string,
        sortOrder: number): Observable<Array<ProductModel>> {


        let theWebApiAddress = `${this.webApiUrl}/ProductsWithPaging?firstRecordNumber=${offset}&pageSize=${pageSize}&sortField=${sortField}&sortOrder=${sortOrder}`;

        let theObservable: Observable<Array<ProductModel>> =
            this._http
            .get(theWebApiAddress)
            .map(r => r.json());

        return theObservable;
    }

    /**
     * @method This is used to retrieve a record by the description.
     */
    public getLookupsForProducts(): Observable<ProductLookupValuesModel> {


        let theWebApiAddress = `${this.webApiUrl}/GetLookupsForProducts`;

        let theObservable: Observable<ProductLookupValuesModel> = this._http.get(theWebApiAddress).map(r => r.json());

        return theObservable;
    }
}