import { Injectable } from '@angular/core';
import { Observable } from "RxJS/Rx";
import { Http, Headers, RequestOptions, Response } from '@angular/http';

@Injectable()
export class FakeServcie {
    errorMessage: string;
    mode = 'Observable';
    constructor(private http: Http) {

    }

    public fakeCall(): Observable<any[]> {
        return this.http.get("/fakecall").map(r => r.json());
    }
}
