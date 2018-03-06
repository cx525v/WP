import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { LidTypesEnum } from '../../models/common/lid-types.enum';
import { DashboardPrimaryKeysModel } from '../../models/dashboardInfo/dashboard-primary-keys.model';
import { ContactFilter, ContactPage, Contacts } from '../../models/dashboardInfo/contacts.model';
import { apiResponse} from '../../models/dashboardInfo/apiResponse.model';

declare var gAppConfigSettings: IAppConfigSettings;
@Injectable()
export class ContactService {  
    constructor(private http: Http) {
       
    }

    public getContacts(data: ContactFilter): Observable<apiResponse<Contacts>> {
        var url = gAppConfigSettings.WebApiUrl + 'ContactList';       
        return this.http.post(url, data).map(r => {            
        return r.json();
        },
        (error) => {
            console.log(error);
        });
    }
}