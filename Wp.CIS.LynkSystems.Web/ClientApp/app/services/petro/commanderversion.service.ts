import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response,RequestOptionsArgs } from '@angular/http';
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { commanderbaseversion, commanderversion } from '../../models/petro/commanderversion.model';
import { User } from './../../models/Authentication/user-authentication.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class CommanderVersionService {
    errorMessage: string;
    mode = 'Observable';
    constructor(private http: Http) {

    }
    public getCommanderBaseVersion(): Observable<commanderbaseversion[]> {

        const Url = gAppConfigSettings.WebApiUrl + 'CommanderVersion/GetBaseVersions';
        let headers = new Headers({
            'Content-Type': 'application/json; charset=utf-8'
        });
        headers.append('Cache-control', 'no-cache');
        headers.append('Cache-control', 'no-store');
        headers.append('Expires', '0');
        headers.append('Pragma', 'no-cache');

        let options = new RequestOptions({ headers: headers });
        return this.http.get(Url, options).map(r => r.json());
    }

    public addComanderVersion(version: commanderversion):Observable<any> {
        const url = gAppConfigSettings.WebApiUrl + 'CommanderVersion/CreateVersion';
        version.createdByUser = localStorage.getItem('WorldPay.cis.currentUser');
       
        return this.http.post(url, version).map(r =>
        {
           return r.text();
        }
        );    
    }


    public deleteCommanderVersion(versionID: number): Observable<any> {
       let userName: string = localStorage.getItem('WorldPay.cis.currentUser');
       const url = gAppConfigSettings.WebApiUrl + 'CommanderVersion/' + versionID + '/' + userName;
        
        return this.http.delete(url).map(r => {
            return r.text();
        });
    }
} 
