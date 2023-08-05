export class User {
    id?: number;
    username?: string;
    password?: string;
    token?: string;
    roles?:string;
}

export class TokenResponse {
    userName?: string;
    token?: string;
    roles?:string;

    empDetailsID?:number;

}