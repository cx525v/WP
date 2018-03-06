import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { MerchantLocation} from '../../models/dashboard.model';
import { MerchantFilter,MerchantPage} from '../../models/dashboardInfo/merchantlocation.model';
import { apiResponse } from '../../models/dashboardInfo/apiResponse.model';

declare var gAppConfigSettings: IAppConfigSettings;
@Injectable()
export class MerchantLocationService {
    constructor(private http: Http) {

    }

    public getMerchantLocations(data: MerchantFilter): Observable<apiResponse<MerchantLocation>> {
        var url = gAppConfigSettings.WebApiUrl + 'MerchantList';
        return this.http.post(url, data).map(r => {          
            return r.json();
        },
        (error) => {
            console.log(error);
        });
    }
}