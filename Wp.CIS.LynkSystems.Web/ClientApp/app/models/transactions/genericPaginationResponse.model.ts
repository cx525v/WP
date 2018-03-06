
export class GenericPaginationResponse<T> {

    constructor(public totalNumberOfRecords: number,
        public returnedRecords: Array<T>) {

    }
}