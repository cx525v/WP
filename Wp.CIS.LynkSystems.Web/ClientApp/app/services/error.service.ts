
import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable, Subscription } from "RxJS/Rx";
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { NotificationService } from '../services/notification.service';

import { IAppConfigSettings } from '../models/common/app-config-settings.model';

declare var gAppConfigSettings: IAppConfigSettings;

import { ErrorModel } from '../models/error/error.model';


@Injectable()
export class ErrorService {

    private webApiUrl: string;

    /**
     * @constructor This provides initialization for the service
     * @param _http This will be used to make calls to the Web API
     */
    constructor( @Inject(NotificationService) private notificationService: NotificationService,
                    private _http: Http) {

        this.webApiUrl = `${gAppConfigSettings.WebApiUrl}Error`;

    }

    /**
     * @method This is used to retrieve the general information for a terminal.
     * @param terminalNumber 
     */
    public logErrorModel(errorInfo: ErrorModel): void {

 
        let errorId: string = this.generateUUID();
        errorInfo.errorId = errorId;

        let response: Subscription = this._http
                                            .post(this.webApiUrl, errorInfo)
                                            .map(r => r.json())
                                            .subscribe();

        let displayMessage: string = this.getDisplayErrorMessage(errorId);

        setTimeout(() => this.notificationService.error(displayMessage), 1);

        return;
    }

    /**
     * @method This is used to retrieve the general information for a terminal.
     * @param terminalNumber 
     */
    public logError(message: string, description: string, stackTrace: string): void {

        let errorInfo: ErrorModel = new ErrorModel();
        errorInfo.description = description;
        errorInfo.message = message;
        errorInfo.stackTrace = stackTrace;
        let errorId: string = this.generateUUID();
        errorInfo.errorId = errorId;

        let response: Subscription = this._http
            .post(this.webApiUrl, errorInfo)
            .map(r => r.json())
            .subscribe();

        let displayMessage: string = this.getDisplayErrorMessage(errorId);

        setTimeout(() => this.notificationService.error(displayMessage), 1);

        return;
    }

    /**
     * @method This is used to retrieve the general information for a terminal.
     * @param terminalNumber 
     */
    public logException(message: string, error: Error): void {

        let errorInfo: ErrorModel = new ErrorModel();
        errorInfo.message = message;

        if (error) {

            errorInfo.description = error.message;
            errorInfo.stackTrace = error.stack;
        }

        let errorId: string = this.generateUUID();
        errorInfo.errorId = errorId;

        let response: Subscription = this._http
            .post(this.webApiUrl, errorInfo)
            .map(r => r.json())
            .subscribe();

        let displayMessage: string = this.getDisplayErrorMessage(errorId);

        setTimeout(() => this.notificationService.error(displayMessage), 1);

        return;
    }

    /**
     * @method This is used to retrieve the general information for a terminal.
     * @param terminalNumber 
     */
    public logErrorResponse(message: string, error: Response): void {

        let errorInfo: ErrorModel = new ErrorModel();
        errorInfo.message = message;

        if (error
            && error.text()) {

            errorInfo.description = error.text();
        }

        let errorId: string = this.generateUUID();
        errorInfo.errorId = errorId;

        let response: Subscription = this._http
            .post(this.webApiUrl, errorInfo)
            .map(r => r.json())
            .subscribe();

        let displayMessage: string = this.getDisplayErrorMessage(errorId);

        setTimeout(() => this.notificationService.error(displayMessage), 1);

        return;
    }
    /**
     * @method This returns the error message that will be displayed to the user.
     * @param errorId
     */
    private getDisplayErrorMessage(errorId: string): string {

        let response = `Error occured while processing in the application. Please contact the administrator with reference number: ${errorId}`;

        return response;
    }

    /**
     * @method This generates a GUID that will be used for a reference.
     */
    private generateUUID(): string { // Public Domain/MIT
        var d = new Date().getTime();
        if (typeof performance !== 'undefined' && typeof performance.now === 'function') {
            d += performance.now(); //use high-precision timer if available
        }
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
    }

}

