import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { PetroTable, UpdateXmlModel } from '../../models/petro/petroTable.model';
import { User } from './../../models/Authentication/user-authentication.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class EPSTableloadService {
    constructor(private http: Http) {

    }
    public getTableSchema(tableid: number, active:number, effectiveDate: Date, schemaID:number): Observable<any[]> {

        const url = gAppConfigSettings.WebApiUrl + 'PetroDataTableSchema';

        return this.http.get(url).map(r => { return r.json() });

    }

    public GetAllPetroTablesByVersion(versionID: number): Observable<any[]> {

        const url = gAppConfigSettings.WebApiUrl + 'EPSTable/GetAllPetroTablesByVersion?versionID=' + versionID;
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

    public SavePetroTable(table: PetroTable): Observable<any> {     
        table.lastUpdatedBy = localStorage.getItem('WorldPay.cis.currentUser');
        const url = gAppConfigSettings.WebApiUrl + 'EPSTable/EPSUpsertPetroTable';
        if (table.schemaDef) {
            table.schemaDef = table.schemaDef.replace('encoding="UTF-8"', '').replace('encoding="utf-8"', '');
        }

        if (table.defaultXML) {
            table.defaultXML = table.defaultXML.replace('encoding="UTF-8"', '').replace('encoding="utf-8"', '');
        }

        return this.http.post(url, table).map(
            r => {             
               return r.text();
            },
           error => {
                console.log(error);
            }
        ); 
    }

} 
