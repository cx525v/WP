
import { PrimeNgSelectItemValue } from './primeNgSelecteItemValue.model'

export class PrimeNgSelectOption<T> {

    constructor(public label: string,
        public value: T) {

    }
}