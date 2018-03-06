import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../models/common/app-config-settings.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class MerchantProfileServcie {
    errorMessage: string;
    mode = 'Observable';
    constructor(private http: Http) {

    } 
    public getMerchantProfileById(idObj: any) {      
  
        console.log("I am in Merchant Profile Service");
       
        return this.http.get(gAppConfigSettings.WebApiUrl + 'merchantprofile/' + idObj );
    }
    public getMerchantProfileById2(idObj: any) {
 
        console.log("I am in dummy Merchant Profile Service");
        
        return this.http.get(gAppConfigSettings.WebApiUrl + 'Authorization/' + idObj);
    }
} 