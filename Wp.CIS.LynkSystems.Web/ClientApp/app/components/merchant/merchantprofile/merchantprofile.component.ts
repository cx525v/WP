import { Component, Pipe } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Router, RouterModule, Params } from '@angular/router';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MerchantProfileServcie } from '../../../services/merchantprofile.service';
import { AccordionModule } from 'primeng/primeng';     //accordion and accordion tab
import { MenuItem } from 'primeng/primeng';            //api
import { AutoCompleteModule } from 'primeng/primeng';
import { InputTextModule } from 'primeng/primeng';
import { TabViewModule } from 'primeng/primeng';
@Component({
    selector: 'merchantId',
    templateUrl: './merchantprofile.component.html'
    
})
export class MerchantProfileComponent {
      
    //private merchantId: number;
    //public ensettings: {};
    public ensettings: MerchantProfileData[];
    public formData: FormGroup;
    constructor(private merchService: MerchantProfileServcie) {
        this.formData = new FormGroup({
            'merchantId': new FormControl('', [Validators.required])
        });
    };
    
    public submitData() {
        if (this.formData.valid) {
            var Obj = {
                merchantId: this.formData.value.merchantId
            };
           
            if (this.formData.value.merchantId != null) {
                this.merchService.getMerchantProfileById(this.formData.value.merchantId)
                    .subscribe(
                    (data) => (this.ensettings = data.json() as MerchantProfileData[])
                    );
            }
        }
    }
}
interface MerchantProfileData {
    accountnbr: number;
    acqBankdesc: string;
    acqbanknameaddressid: number;
    acquiringbankid: number;
    activationdt: Date;
    benefittype: number;
    benetypedesc: string;
    brandid: number;
    cardtype: number;
    chkacctnbr: number;
    customerid: number;
    deactivationdt: Date;
    descriptorcd: number;
    discountrate: number;
    federaltaxid: number;
    fnsnbr: number;
    highriskind: number;
    incrementaldt: Date;
    industrytype: number;
    industrytypedesc: string;
    interneturl: string;
    irsverificationstatus: number;
    merchantclass: string;
    merchantid: number;
    merchantnbr: string;
    merchanttype: number;
    merchtypedesc: string;
    mvv1: string;
    mvv2: string;
    programtype: number;
    programtypedesc: string;
    risklevel: number;
    riskleveldesc: string;
    risklevelid: number;
    siccode: number;
    siccodedesc: string;
    statdesc: string;
    statetaxcode: number;
    statusindicator: number;
    storenbr: number;
    subindgrpdesc: string;
    subindgrpid: number;
    thresholddt: Date;
    visaindustrytype: number;
}