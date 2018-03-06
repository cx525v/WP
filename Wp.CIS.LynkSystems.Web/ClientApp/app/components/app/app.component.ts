import { Component, ChangeDetectorRef, NgZone, OnInit } from '@angular/core';
import { By } from '@angular/platform-browser';
import { Message, Messages  } from 'primeng/primeng';
import { NotificationService } from '../../services/notification.service';
import { ViewEncapsulation } from '@angular/core';
import { DashboardInfoService } from './../../services/dashboardinfo.service';
import { AuthenticationService, AuthGuard, TokenService } from './../../services/Authentication/index';
import { User } from './../../models/Authentication/user-authentication.model';
import { Router } from '@angular/router';

import '../../app.module.client';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    encapsulation: ViewEncapsulation.None ,
    providers: [DashboardInfoService,
        TokenService,
        AuthenticationService,
        AuthGuard]
})
export class AppComponent implements OnInit {
    
  
    innerWidth: number;
    width: number;
    public user: User = new User();
    public _data: string;
    public showSmalldeviceClassSearch: any = [''];
    constructor(private notification: NotificationService,
        private ngZone: NgZone,
        private authService: AuthenticationService,
        private tokenService: TokenService,
        private authGaurd: AuthGuard,
        private route: Router) {           

        window.onload = (e) => {
            this.topLogoNavResize();
        };
        
    }


    ngOnInit(): void {
        try {
            
            if (this.tokenService.getAccessToken('WorldPay.cis.currentUser') != null) {
                this.user.userName = this.tokenService.getAccessToken('WorldPay.cis.currentUser');
                this.user.domainName = this.tokenService.getAccessToken(this.user.userName);
                
                if (this.tokenService.getAccessToken('WorldPay.cis.currentUserToken') != null) {
                    console.log("JWT Token :" + this.tokenService.getAccessToken('WorldPay.cis.currentUserToken'));

                } else {                    
                     this.authService.login(this.user)
                        .subscribe(r => {
                            console.log("New JWT Token :" + this.tokenService.getAccessToken('WorldPay.cis.currentUserToken'));
                        });
                }

            } 
        }
        catch (err){
            console.log(err);
        }        
    }

    
    onResize(event) {
        this.topLogoNavResize();
    }

    

    private topLogoNavResize() {
        this.ngZone.run(() => {
            this.width = window.innerWidth;
            
            this.showSmalldeviceClassSearch = [];
            //console.log("In top Nav");
            if (this.width < 768) {                
                this.showSmalldeviceClassSearch.push('search-link-push-sm')
            } else {
               
                this.showSmalldeviceClassSearch.push('search-link-push-lg')
            }
        });
    }

    public isExpanded: boolean = false;
    

    getMessages(): Message[] {
        return this.notification.message;
    }
    toggleSearch() {        
        if (!this.isExpanded) {
            document.querySelector('#dashSearch').removeAttribute("hidden");
            
        }
        else {
            document.querySelector('#dashSearch').setAttribute("hidden", '');            
        }
        this.isExpanded = !this.isExpanded;
    }
}
