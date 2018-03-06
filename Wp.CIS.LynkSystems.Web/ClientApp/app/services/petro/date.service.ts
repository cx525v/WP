import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';
@Injectable()
export class DateServcie {    
    convert(date: any): string {
        var datePipe = new DatePipe('en-us');
        return datePipe.transform(date, 'yyyy-MM-dd');
    }

    convertDate(dateString: string): Date {
        var date: Date = new Date(dateString);
        return new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate());
    }
} 
