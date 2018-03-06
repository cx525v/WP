
export class Mapping implements TableMapping {

    constructor(
        public versionID?,
        public mappingID?,
        public pdlFlag?,
        public paramID?,
        public paramName?,
        public worldPayFieldName?,
        public worldPayTableName?,
        public worldPayJoinFields?,
        public worldPayCondition?,
        public worldPayOrderBy?,
        public worldPayFieldDescription?,
        public effectiveBeginDate?,
        public effectiveEndDate?,
        public viperTableName?,
        public viperFieldName?,
        public viperCondition?,
        public charStartIndex?,
        public charLength?,
        public createdByUser?
    ) { }
}


export interface TableMapping {
     versionID?: number,
     mappingID?: number,
     pdlFlag?: boolean,
     paramID?: number,
     paramName?: string,
     worldPayFieldName?: string,
     worldPayTableName?: string,
     worldPayJoinFields?: string,
     worldPayCondition?: string,
     worldPayOrderBy?: string,
     worldPayFieldDescription?:string,
     effectiveBeginDate?: string,
     effectiveEndDate?: string,
     viperTableName?: string,
     viperFieldName?: string,
     viperCondition?: string,
     charStartIndex?: number,
     charLength?: number,
     createdByUser?: string,
     lastUpdatedBy?: string
}