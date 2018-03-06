import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { LidTypesEnum } from '../../models/common/lid-types.enum';
import { DashboardPrimaryKeysModel } from '../../models/dashboardInfo/dashboard-primary-keys.model';
import {TerminalEquipment,TerminalFilter,TerminalPage  } from '../../models/dashboardInfo/terminalequipment.model';
import { apiResponse } from '../../models/dashboardInfo/apiResponse.model';

declare var gAppConfigSettings: IAppConfigSettings;
@Injectable()
export class TerminalInfoService {
    constructor(private http: Http) {

    }

    public getTerminalList(data: TerminalFilter): Observable<apiResponse<TerminalEquipment>> {
        var url = gAppConfigSettings.WebApiUrl + 'TerminalList';       
        return this.http.post(url, data).map(r => {            
            return r.json();
        },
            (error) => {
                console.log(error);
            });
    } 

    public getTerminalInfoDetail(terminalNbr: number): Observable<any> {
        var url = gAppConfigSettings.WebApiUrl + 'TerminalDetails/GetTerminalDetails?termNbr=' + terminalNbr;
        return this.http.get(url).map(
            r => {
                  return r.json();
            },
            (error) => {
                console.log(error);
            });
    }

    public getTerminalEquipments(merchantId: number): Observable<any[]> {

        return this.http.get(gAppConfigSettings.WebApiUrl + 'TerminalList/' + merchantId)
            .map(r => {
                return r.json();
            });
    }

    public getTerminalSettlementInfo(terminalNbr: number): Observable<any> {

        return this.http.get(gAppConfigSettings.WebApiUrl + 'TerminalDetails/GetSettlementInfo?termNbr=' + terminalNbr)
            .map(r => {
                return r.json();
            });
    }
}