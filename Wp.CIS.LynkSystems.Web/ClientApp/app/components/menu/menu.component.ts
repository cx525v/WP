import { Component } from '@angular/core';
import { MegaMenu, MenuItem } from 'primeng/primeng';
import { ViewEncapsulation } from '@angular/core';
@Component({
    selector: 'menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.css'],
    encapsulation: ViewEncapsulation.None,
})
export class MenuComponent {
    items: MenuItem[];
    ngOnInit() {
        this.items = [
            {
                label: '',
                icon: 'fa fa-bars',
                items: [
                        [
                            {
                                label: 'CIS',
                                items:
                                [
                                    {
                                        label: 'CIS Dashboard',
                                        icon: 'fa fa-tachometer',
                                        routerLink: '/dashboardinfo'
                                    }
                                ]
                            }
                        ],
                        [
                            {
                                label: 'Admin',                          
                                items: [
                                    //{
                                    //    label: 'Product Maintenance',
                                    //    icon: 'fa fa-refresh',
                                    //    routerLink:'/Admin/ProductMaintanence'
                                    //},
                                    {
                                        label: 'Petro',
                                        icon: 'fa fa-table',
                                        routerLink: '/epstableload'
                                    }
                                ]
                            }
                        ],               
                      ]
            },     
            
        ];
    }
}
