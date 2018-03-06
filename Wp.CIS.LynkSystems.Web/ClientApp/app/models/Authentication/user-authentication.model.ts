export class User {
    userName: string;
    password?: string;
    firstName?: string;
    lastName?: string;
    role?: Roles = Roles.CIS_STAGE_Reader;
    domainName: string;
}

export enum Roles {
    CIS_Writer,
    CIS_Reader,
    CIS_Admin,
    CIS_STAGE_Writer,
    CIS_STAGE_Reader,
    CIS_STAGE_Admin
}