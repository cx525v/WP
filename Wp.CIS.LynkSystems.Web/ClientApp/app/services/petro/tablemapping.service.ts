import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { TableMapping } from '../../models/petro/petroTablemapping.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class TableMappingServcie { 
    constructor(private http: Http) {

    }
    public getTableMappings(versionId: number): Observable<any[]> {
     
        const url = gAppConfigSettings.WebApiUrl + 'epsmapping/' + versionId;

        let headers = new Headers({
            'Content-Type': 'application/json; charset=utf-8'
        });
        headers.append('Cache-control', 'no-cache');
        headers.append('Cache-control', 'no-store');
        headers.append('Expires', '0');
        headers.append('Pragma', 'no-cache');

        let options = new RequestOptions({ headers: headers });
        return this.http.get(url, options).map(r => {
            return r.json();
        });
    }

    public insertTableMappings(mapping: TableMapping): Observable<any> {
        const url = gAppConfigSettings.WebApiUrl + 'epsmapping';
        this.trimMapping(mapping);
        mapping.createdByUser = localStorage.getItem('WorldPay.cis.currentUser');
        return this.http.post(url, mapping).map(r => {return r.text() });
    }

    trimMapping(mapping: TableMapping): TableMapping {
        if (mapping) {
            if (mapping.pdlFlag) {
                mapping.worldPayTableName = undefined ;
                mapping.worldPayJoinFields = undefined ;
                mapping.worldPayFieldName = undefined ;
                mapping.worldPayCondition = undefined ;
                mapping.worldPayOrderBy = undefined ;         
            } else {
                mapping.paramID = undefined ;
                mapping.paramName = undefined ;
            }
        }

        return mapping;
    }

    public updateableMappings(mapping: TableMapping): Observable<any> {
        const url = gAppConfigSettings.WebApiUrl + 'epsmapping';
        this.trimMapping(mapping);       
        mapping.lastUpdatedBy = localStorage.getItem('WorldPay.cis.currentUser');
        return this.http.put(url, mapping).map(r => { return r.text() });
    }

    public copyMapping(fromVersionID: number, toVersionID: number): Observable<any> {
        const url = gAppConfigSettings.WebApiUrl + 'epsmapping/copy';
        let userName: string = localStorage.getItem('WorldPay.cis.currentUser');
 
        let data = {
            FromVersionID: fromVersionID,
            ToVersionID: toVersionID,
            UserName: userName
        }; 
        return this.http.post(url, data).map(r => {return r.text() });
    }


    public getCBSParameters(): Observable<any[]> {

        const url = gAppConfigSettings.WebApiUrl + 'Parameters/GetParameters' ;

        return this.http.get(url).map(r => {
            return r.json();
        }
        );
    }
} 
