
import { Response, ResponseOptions } from '@angular/http'

export class CisResponse extends Response {

    /**
     * @field This holds the source url for the error.
     */
    public sourceUrl: string;

    /**
     * @constructor This initializes the class.
     * @param responseOptions
     */
    constructor(responseOptions: ResponseOptions) {

        super(responseOptions);

        this.sourceUrl;
    }
}
