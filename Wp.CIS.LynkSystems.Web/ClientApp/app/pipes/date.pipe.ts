import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'dateFormat'
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {  
    transform(value: any): string {
        if (!value) {
            return 'N/A';
        } else {
            var date: Date = new Date(value);
            if (date.getFullYear() == 1) {
                return 'N/A';
            } else {
                var DATE_FMT = 'MM/dd/yyyy';                
                var JMS_FMT = 'jms';               
                return super.transform(value, DATE_FMT) + ' ' + super.transform(value, JMS_FMT);
            }
        }
    }
}