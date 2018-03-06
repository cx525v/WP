export class MemoInfo {
    lidtype?: number;
    lid?:number;
    categoryID?: number;
    memo:string;
    enabled?:boolean;
    categoryDesc:string;
    groupID?: number;
}

export class MemoList {
    customerMemo: MemoInfo[];
    merchMemo: MemoInfo[];
    termMemo: MemoInfo[];
    groupMemo: MemoInfo[];
}