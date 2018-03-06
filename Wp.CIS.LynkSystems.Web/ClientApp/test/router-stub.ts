
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { convertToParamMap, ParamMap } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';

@Injectable()
export class ActivatedRouteStub {

    constructor(private returnParams: any) {

    }

    // ActivatedRoute.paramMap is Observable
    private subject = new BehaviorSubject(convertToParamMap(this.testParamMap));
    paramMap = this.subject.asObservable();

    // Test parameters
    private _testParamMap: ParamMap;
    get testParamMap() { return this._testParamMap; }
    set testParamMap(params: {}) {
        this._testParamMap = convertToParamMap(params);
        this.subject.next(this._testParamMap);
    }

    // ActivatedRoute.snapshot.paramMap
    get snapshot() {
        return { paramMap: this.testParamMap };
    }

    //get params(): any {
    //    return Observable.of({ lidType: 3, lidTypeValue: 123456 });
    //}

    //params = Observable.of({ lidType: 3, lidTypeValue: 123456 });

    params = Observable.of(this.returnParams);
}