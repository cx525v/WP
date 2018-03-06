import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { LidTypesEnum } from '../../models/common/lid-types.enum';
import { DashboardPrimaryKeysModel } from '../../models/dashboardInfo/dashboard-primary-keys.model';
import { DashboardAdvanceSearch} from '../../models/dashboardInfo/dashboard-advancesearch.model';

declare var gAppConfigSettings: IAppConfigSettings;
@Injectable()
export class AdvanceSearchService {

    mockData: DashboardAdvanceSearch[] = [
        {
            "lidType": 7,
            "lid": "1000393727",  
            "name": "Golden Corral Corporation"
        },
        {
            "lidType": 6,
            "lid": "542929007088230",
            "name": "Golden Corral 919"
        },
        {
            "lidType": 5,
            "lid": "LYK93887",
            "name": "Golden Corral 919"
        },
    ];
    constructor(private http: Http) {

    }

    public getAdvanceSearchResult(lidType: LidTypesEnum, searchValue: string): Observable<DashboardAdvanceSearch []> {
       return Observable.of(this.mockData);
        //var url = gAppConfigSettings.WebApiUrl + '';
        //return this.http.get(url)
        //    .map(r => {
        //        return r.json();
        //    });
    }
}