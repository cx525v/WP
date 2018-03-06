import { Injectable }  from '@angular/core'
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import { User, AuthHeader, TokenService } from './index';
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';

declare var gAppConfigSettings: IAppConfigSettings;
@Injectable()
export class AuthenticationService {

    public authHeader: AuthHeader;
    public token: string;
    tokenService: TokenService = new TokenService();
    private url: string = gAppConfigSettings.WebApiUrl + 'Account/token';

    constructor(private http: Http) {       
        if (this.authHeader == null) {
            this.authHeader = new AuthHeader(); 
        }
    }

    
   
    login(user: User): Observable<any> {
        try {
            let headers = new Headers({
                'Content-Type':
                'application/json; charset=utf-8'
            });
            let options = new RequestOptions({ headers: headers });        
            
            //Call of Web APi for the JWT Auth token
            return this.http.post(this.url, JSON.stringify(user), options)
                .map((response: Response) => {
                    // login successful if there's a jwt token in the response

                    let token = response.json(); //&& response.json().token;

                    if (token) {
                        // set token property
                        this.token = token;
                        console.log(response.json());
                        this.authHeader.expirationClient = new Date();
                        

                        this.authHeader.expirationServer = response.json().expiration;
                        // store username and jwt token in local storage to keep user logged in between page refreshes
                        this.tokenService.setAccessToken('WorldPay.cis.currentUserToken', JSON.stringify(
                            { username: user.userName, token: token, expiration: response.json().expiration, expirationClient: this.authHeader.expirationClient }));

                        // return token to indicate successful login
                        return this.token;
                    } else {
                        // return null to indicate failed login
                        return null;
                    }
                })

        } catch (err){
            console.log(err);
        }
                
    }   

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('WorldPay.cis.currentUserToken');
    }

    
}