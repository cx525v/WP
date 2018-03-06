import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { Audit} from '../../models/petro/audit.model';
import { AuditDetail } from '../../models/petro/auditDetail.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class AuditServcie {
    constructor(private http: Http) {
    }

    public getAudit(versionId: number, startDate: string, endDate: string): Observable<Audit[]> {
        const url = gAppConfigSettings.WebApiUrl + 'epspetroaudit/' + versionId + '/' + startDate +'/' + endDate;
        return this.http.get(url).map(r => { return r.json() });
    }

    public getAuditDetail(auditId: number): Observable<AuditDetail[]> {

        const url = gAppConfigSettings.WebApiUrl;

        return this.http.get(url).map(r => {return r.json() });
    }

    public getMapping(xml: string): Observable<any> {
        const url = gAppConfigSettings.WebApiUrl + 'xml/mapping';
        let data = JSON.stringify(xml);
        return this.http.post(url, data).map(r => {
            return r.json();
        });
    }

    public getMappings(xml:string): Observable<any[]> {
        const url = gAppConfigSettings.WebApiUrl + 'xml/mappings';
        let data = JSON.stringify(xml);
        return this.http.post(url, data).map(r => {
             return r.json();
        });
    }

    public getPetroTable(xml: string): Observable<any> {
        const url = gAppConfigSettings.WebApiUrl + 'xml/petrotable';
        let data = JSON.stringify(xml);
        return this.http.post(url, data).map(r => {         
            return r.json();
        });
    }

    public getPetroTables(xml: string): Observable<any[]> {
        const url = gAppConfigSettings.WebApiUrl + 'xml/petrotables';
        let data = JSON.stringify(xml);
        return this.http.post(url, data).map(r => {
            return r.json();
        });
    }

    public getversionaudit(xml: string): Observable<any> {
        const url = gAppConfigSettings.WebApiUrl + 'xml/versionaudit';
        let data = JSON.stringify(xml);
        return this.http.post(url, data).map(r => {
            return r.json();
        });
    }
} 
