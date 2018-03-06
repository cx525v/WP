export interface apiResponse<T> {
    totalNumberOfRecords: number;
    returnedRecords: T[];
}