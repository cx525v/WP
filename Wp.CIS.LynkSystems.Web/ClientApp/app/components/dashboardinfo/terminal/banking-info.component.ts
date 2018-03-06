
import { Component, OnInit, OnDestroy, Input } from '@angular/core';

import { BankingService } from '../../../services/banking.service';

import { LidTypesEnum } from '../../../models/common/lid-types.enum';

import { Response } from '@angular/http';

import { BankingInfoModel } from '../../../models/bankingInfo/banking-info.model'

@Component({

    selector: 'banking-info',
    templateUrl: './banking-info.component.html',
    styleUrls: ['./banking-info.component.css'],
    providers: [BankingService]
})
export class BankingInfoComponent implements OnInit, OnDestroy {

    private _terminalNbr: number;

    private _bankingInfoList: Array<BankingInfoModel>;

    private _errorMessage: string;

    private _isLoading: boolean;

    constructor(private _bankingService: BankingService) {

        this._errorMessage = null;

        this._bankingInfoList = null;

        this._terminalNbr = null;

        this._isLoading = false;
    }

    @Input("terminalNbr") set terminalNbr (value: number){

        this._terminalNbr = value;

        this.displayBankingInfoForTerminal(this._terminalNbr);
    }

    get terminalNbr(): number {

        return this._terminalNbr;
    }

    public ngOnInit(): void {

    }

    public ngOnDestroy(): void {

    }

    private displayBankingInfoForTerminal(theTerminalNbr: number) {

        this._errorMessage = null;

        if (this._terminalNbr) {

            this._isLoading = true;

            this._bankingInfoList = null;

            this._bankingService
                .getBankingInfo(LidTypesEnum.TerminalNbr, this._terminalNbr.toString())
                .subscribe(
                (data: Array<BankingInfoModel>) => {

                    this._isLoading = false;
                    this.orderRecordsForDisplay(data);
                },
                (error: Response) => {
                    this._isLoading = false;
                        this._errorMessage = error.text();
                }
                );
        }
    }

    private orderRecordsForDisplay(data: Array<BankingInfoModel>): void {

        if (data) {

            let settlementItem: BankingInfoModel = data.find(x => x.activityAcctTypeDescription && x.activityAcctTypeDescription.trim().toUpperCase() === "SETTLEMENT");
            let chargebackItem: BankingInfoModel = data.find(x => x.activityAcctTypeDescription && x.activityAcctTypeDescription.trim().toUpperCase() === "CHARGEBACK");
            let billingItem: BankingInfoModel = data.find(x => x.activityAcctTypeDescription && x.activityAcctTypeDescription.trim().toUpperCase() === "BILLING");

            if (!this._bankingInfoList) {

                this._bankingInfoList = new Array<BankingInfoModel>();
            }

            this._bankingInfoList.length = 0;

            if (settlementItem) {
                this._bankingInfoList.push(settlementItem);
            }

            if (chargebackItem) {
                this._bankingInfoList.push(chargebackItem);
            }

            if (billingItem) {

                this._bankingInfoList.push(billingItem);
            }
        }
    }
}