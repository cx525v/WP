import { Injectable, Injector } from '@angular/core';
import { Request, XHRBackend, RequestOptions, Response, Http, RequestOptionsArgs, Headers, ResponseOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { NotificationService } from "../services/notification.service";

import { CisResponse } from "../models/core/cisResponse.model";

@Injectable()
export class HttpInterceptor extends Http {
    constructor(backend: XHRBackend, defaultOptions: RequestOptions, private injector: Injector) {
        super(backend, defaultOptions);
    }
    reqUrl: any;
    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        this.reqUrl = url;


        let defaultHeader = new Headers({
            'Content-Type': 'application/json; charset=utf-8'
        });

        if (url instanceof Request) {

            if (url) {

                if (url.headers) {
                    let keys: Array<string> = url.headers.keys();

                    if (!keys || 0 === keys.length) {

                        url.headers = defaultHeader;
                    }

                } else {
                    url.headers = defaultHeader;
                }

            }
        }

        if (url instanceof String) {

            if (options) {

                if (options.headers) {

                    let keys: Array<string> = options.headers.keys();

                    if (!keys || 0 === keys.length) {

                        options.headers = defaultHeader;
                    }

                } else {
                    options.headers = defaultHeader;
                }

            } else {

                options = new RequestOptions({ headers: defaultHeader });
            }
        }

        if (!options) {
            options = new RequestOptions({ headers: defaultHeader });
        }       
        options.headers.append('UserName', localStorage.getItem('WorldPay.cis.currentUser'));

        return super.request(url, options).catch((error: Response): Observable<Response> => {
            var url;
            if (typeof this.reqUrl === "string") {
                url = this.reqUrl;
            } else {
                url = this.reqUrl.url;
            }

            let options: ResponseOptions = new ResponseOptions();
            let newResponse: CisResponse = new CisResponse(options);

            Object.assign(newResponse, error);

            newResponse.sourceUrl = url;

            throw newResponse;
        });
    }


    //post(url: string, body: any, options?: RequestOptionsArgs): Observable<Response> {

    //    let defaultHeader = new Headers({
    //        'Content-Type': 'application/json; charset=utf-8'
    //    });


    //        if (options) {

    //            if (!options.headers || 0 === options.headers.keys.length) {

    //                options.headers = defaultHeader;

    //            }

    //        } else {

    //            options = new RequestOptions({ headers: defaultHeader });

    //        }

    //    let response = super.post(url, body, options);

    //    return response;
    //}
}