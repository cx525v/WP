import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'datemdy'
})
export class YearMonthDatePipe extends DatePipe implements PipeTransform {
    transform(value: any): string {
        if (!value) {
            return '';
        } else {
            var date: Date = new Date(value);
            if (date.getFullYear() == 1) {
                return '';
            } else {
                var DATE_FMT = 'MM/dd/yyyy';
                return super.transform(value, DATE_FMT);
            }
        }
    }
}