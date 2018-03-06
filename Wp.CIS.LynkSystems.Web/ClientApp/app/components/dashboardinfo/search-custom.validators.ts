
import { ValidatorFn, AbstractControl } from '@angular/forms';

export class SearchCriterialHelper {

    public static readonly CustomerNbrRegExpression: string = '^(1000\\d{6}|\\d{6})$';

    public static readonly SixDigitsRegExp: string = '^(\\d{6})$';

    public static readonly FiveDigitsRegExp: string = '^(\\d{5})$';

    public static readonly TwoToFourDigitsRegExp: string = '^(\\d{2,4})$';

    public static readonly OneToFourDigitsWildcardSuffixRegExp: string = '^(\\d{1,4}\\*)$';

    public static readonly OneToFourDigitsWildcardPrefixRegExp: string = '^(\\*\\d{1,4})$';

    public static readonly OnlyAllowNumericValuesRegExpression: string = "^[0-9]*$";

    public static readonly MerchantNbrRegExpression: string = "^(\\d{15})$";

    // The next regular expression might be needed for the second sprint
    //public static readonly TerminalIdRegExpression: string = "^(\\d{1,4}\\*|\\*\\d{1,4}|[lL][kK]\\d{6}|[lL][yY][kK]\\d{5}|\\d{2,6}|[lL]\\*[kK]\\d{1,4}\\*)$";
    public static readonly TerminalIdRegExpression: string = "^([lL][kK]\\d{6}|[lL][yY][kK]\\d{5}|\\d{2,6})$";

    public static readonly OneToFourNumWildCardSuffixRegExp = "^(\d{1,4}\*)";

    public static readonly OneToFourNumWildCardPrefixRegExp = "^(\*\d{1,4})";

    public static readonly TwoToFourNumRegExp = "^(\d{2,4})";

}

export function customerLidLengthsValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
        let regexp: RegExp = new RegExp(SearchCriterialHelper.CustomerNbrRegExpression);
        const customerLidLengths: boolean = regexp.test(control.value);

        let response: Object = null;

        if (false === customerLidLengths) {

            response = { 'customerLidLengths': { value: control.value } }
        }

        return response;
    };
};

export function merchantLidValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
        let regexp: RegExp = new RegExp(SearchCriterialHelper.MerchantNbrRegExpression);
        const customerLidLengths: boolean = regexp.test(control.value);

        let response: Object = null;

        if (false === customerLidLengths) {

            response = { 'merchantLid': { value: control.value } }
        }

        return response;
    };
};

export function terminalLidValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
        let regexp: RegExp = new RegExp(SearchCriterialHelper.TerminalIdRegExpression);
        const customerLidLengths: boolean = regexp.test(control.value);

        let response: Object = null;

        if (false === customerLidLengths) {

            response = { 'termainalLid': { value: control.value } }
        }

        return response;
    };
};

export function onlyAllowNumbersValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
        let regexp: RegExp = new RegExp(SearchCriterialHelper.OnlyAllowNumericValuesRegExpression);
        const customerLidLengths: boolean = regexp.test(control.value);

        let response: Object = null;

        if (false === customerLidLengths) {

            response = { 'onlyAllowNumbers': { value: control.value } }
        }

        return response;
    };
};