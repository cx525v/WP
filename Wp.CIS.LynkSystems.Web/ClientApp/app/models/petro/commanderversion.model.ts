export class commanderbaseversion {
    versionID?: number;
    versionDescription: string;
    createdByUser: string;
    createdDate: string;
}

export class commanderversion extends commanderbaseversion {  
    basisVersion: string;
    baseVersionID?: number;
}

