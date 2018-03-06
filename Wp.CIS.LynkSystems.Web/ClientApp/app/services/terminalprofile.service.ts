import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../models/common/app-config-settings.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class TerminalProfileServcie {
    errorMessage: string;
    mode = 'Observable';
    constructor(private http: Http) {

    }
    public getMerchantProfileById(idObj: any) {
 
        console.log("I am in TerminalProfileServcie");
        
        return this.http.get(gAppConfigSettings.WebApiUrl + 'merchantprofile/' + idObj);
    }    
} 