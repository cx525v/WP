export class PetroTable {
    tableID?: number;
    tableName: string;
    versionID?: number;
    active: boolean;
    definitionOnly: boolean;
    schemaDef: string;
    defaultXML: string;
    createdDate: string;
    effectiveDate: string;
    lastUpdatedBy: string;
    lastUpdatedDate?: string;  
}

export class UpdateXmlModel { 
    xml: string;
    updates: Updates[]
}

export class Updates {
    rowNum: number;
    colName: string;
    newValue: string;
    oldValue: string;
}

export class Tree {
    label: string;
    icon: string;
    data: TreeData;
    children: Tree[];       
}

export class TreeData {
    rowNum: number;
    columnName: string;
    oldValue: string;
    newValue: string;
    attributes: string;
}

