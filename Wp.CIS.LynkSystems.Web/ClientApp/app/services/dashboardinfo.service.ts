import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../models/common/app-config-settings.model';

//import { LIDTypes } from './../models/dashboard.model'
import { LidTypesEnum } from '../models/common/lid-types.enum';
import { DashboardPrimaryKeysModel } from '../models/dashboardInfo/dashboard-primary-keys.model';

import {
    DashboardInfo, CustomerProfile, IMerchantInfo,
    DemographicsInfo, ActiveServices,
    TerminalProfileData, BankingInformation,
    GroupInfo, CaseHistory, MerchantProfileData
} from '../models/dashboard.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class DashboardInfoService {
    errorMessage: string;
    mode = 'Observable';
    constructor(private http: Http) {

    }

    public getDashboardInfo(lidtype: LidTypesEnum, lid: string): Observable<DashboardInfo> {
            

        return this.http.get(gAppConfigSettings.WebApiUrl + 'dashboardinfo/GetDashBoardInfoSearch?LIDType=' + lidtype + "&LID=" + lid)
            .map(r => {                
                return r.json();
            });
    }

    public getSearchPkInfo(lidType: LidTypesEnum, lid: string): Observable<DashboardPrimaryKeysModel> {

        let webUrl: string = `${gAppConfigSettings.WebApiUrl}dashboardinfo/GetSearchPrimaryKeys?LIDType=${lidType}&LID=${lid}`;

        return this.http
            .get(webUrl)
            .map(r => r.json());
    }
    
    public getRecentStatements(merchantNbr: string): Observable<any[]> {
       
        return this.http.get(gAppConfigSettings.WebApiUrl +'recentstatement/' + merchantNbr)
            .map(r => {  
                if (r.json() == 'NoDataFound') {
                    return [];
                } else {
                    return r.json();
                }
            });
    }
}

