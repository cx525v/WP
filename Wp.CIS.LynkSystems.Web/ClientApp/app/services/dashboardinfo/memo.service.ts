import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from "RxJS/Rx";
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { MemoList} from '../../models/dashboardInfo/memo.model';

declare var gAppConfigSettings: IAppConfigSettings;
@Injectable()
export class MemoService {
    constructor(private http: Http) {

    }

    public getMemos(LIDType: number, LID: number): Observable<MemoList> {
        var url = gAppConfigSettings.WebApiUrl + 'memoinfo?LIDType='+LIDType +'&LID=' +LID;
        return this.http.get(url).map(r => {
            return r.json();
        },
        (error) => {
            console.log(error);
        });
    }
}