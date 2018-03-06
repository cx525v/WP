import { ErrorHandler, Inject } from '@angular/core';
import { NotificationService } from '../services/notification.service';


export class GlobalErrorHandler implements ErrorHandler {

    constructor(@Inject(NotificationService) private notificationService: NotificationService) {
    }

    handleError(error: any): void {
        console.log(error);
        setTimeout(() => this.notificationService.error("Error occured while processing your request."),1);
    }
}