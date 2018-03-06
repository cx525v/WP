import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class EPSLogServcie {
    constructor(private http: Http) {

    }
    public getEPSLog(startDate: string, endDate: string): Observable<any[]>{

        const epsLogUrl = gAppConfigSettings.WebApiUrl + 'epslog?StartDate=' + startDate + '&EndDate=' + endDate;

        return this.http.get(epsLogUrl).map(r => {return r.json() });      
    }
    
} 
