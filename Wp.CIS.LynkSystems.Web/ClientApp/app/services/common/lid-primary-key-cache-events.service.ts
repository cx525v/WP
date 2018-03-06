
import { Injectable } from '@angular/core';

import { Subject } from 'rxjs/Subject';

import { Observable } from "RxJS/Rx";

import { LidPrimeyKeyEventModel } from '../../models/common/lid-primary-key-event.model';
import { CustomerPrimaryKeyInfoModel } from '../../models/common/customer-primary-key-info.model';
import { MerchantPrimaryKeyInfoModel } from '../../models/common/merchant-primary-key-info.model';
import { TerminalPrimaryKeyInfoModel } from '../../models/common/terminal-primary-key-info.model';

import { LidTypesEnum } from '../../models/common/lid-types.enum';
import { ActionTypeEnum } from '../../models/common/action-type.enum';

@Injectable()
export class LidPrimaryKeyCacheEventsService {

    private _terminalNumberListChangedSource = new Subject<Map<string, LidPrimeyKeyEventModel>>();

    private _merchantNumberListChangedSource = new Subject<Map<string, LidPrimeyKeyEventModel>>();

    private _customerNumberListChangedSource = new Subject<Map<string, LidPrimeyKeyEventModel>>();

    public _terminalNumberListChanged$: Observable<Map<string, LidPrimeyKeyEventModel>> = this._terminalNumberListChangedSource.asObservable();

    public _merchantNumberListChangedSource$: Observable<Map<string, LidPrimeyKeyEventModel>> = this._merchantNumberListChangedSource.asObservable();

    public _customerNumberListChangedSourc$: Observable<Map<string, LidPrimeyKeyEventModel>> = this._customerNumberListChangedSource.asObservable();

    private _terminalNumbers: Map<LidTypesEnum, LidPrimeyKeyEventModel>;

    private _merchantIds: Map<LidTypesEnum, LidPrimeyKeyEventModel>;

    private _customerIds: Map<LidTypesEnum, LidPrimeyKeyEventModel>;


    private _customerIdsCache: Map<string, CustomerPrimaryKeyInfoModel>;

    private _merchantIdsCache: Map<string, MerchantPrimaryKeyInfoModel>;

    private _terminalNbrsCache: Map<string, TerminalPrimaryKeyInfoModel>;

    constructor() {

        this._terminalNumbers = new Map<LidTypesEnum, LidPrimeyKeyEventModel>();
        this._merchantIds = new Map<LidTypesEnum, LidPrimeyKeyEventModel>();
        this._customerIds = new Map<LidTypesEnum, LidPrimeyKeyEventModel>();


        this._customerIdsCache = new Map<string, CustomerPrimaryKeyInfoModel>();
        this._merchantIdsCache = new Map<string, MerchantPrimaryKeyInfoModel>();
        this._terminalNbrsCache = new Map<string, TerminalPrimaryKeyInfoModel>();
    }

    public addTerminalInfoToCache(terminalInfo: TerminalPrimaryKeyInfoModel): void {

        if (terminalInfo
            && terminalInfo.terminalId) {

            let terminalIdKey: string = terminalInfo
                                            .terminalId
                                            .trim()
                                            .toUpperCase();

            this._terminalNbrsCache[terminalIdKey] = terminalInfo;
        }

    };

    public addMerchantInfoToCache(merchantInfo: MerchantPrimaryKeyInfoModel): void {

        if (merchantInfo
            && merchantInfo.merchantNbr) {

            let merchantNbrKey: string = merchantInfo
                                            .merchantNbr
                                            .trim()
                                            .toUpperCase();

            this._merchantIdsCache[merchantNbrKey] = merchantInfo;
        }

    };

    public addCustomerInfoToCache(customerInfo: CustomerPrimaryKeyInfoModel): void {

        if (customerInfo
            && customerInfo.customerNbr) {

            let customerNbrKey: string = customerInfo
                                            .customerNbr
                                            .trim()
                                            .toUpperCase();

            this._customerIdsCache[customerNbrKey] = customerInfo;
        }
    };

    public getTerminalInfoFromCache(terminalId: string): TerminalPrimaryKeyInfoModel {

        let terminalInfo: TerminalPrimaryKeyInfoModel = null;

        if (terminalId) {

            let terminalIdKey = terminalId.trim().toUpperCase();

            terminalInfo = this._terminalNbrsCache[terminalIdKey];

            if (!terminalInfo) {

                terminalInfo = null;
            }
        }

        return terminalInfo;
    }

    public getMerchantInfoFromCache(merchantNbr: string): MerchantPrimaryKeyInfoModel {

        let merchantInfo: MerchantPrimaryKeyInfoModel = null;

        if (merchantNbr) {

            let merchantNbrKey = merchantNbr.trim().toUpperCase();

            merchantInfo = this._merchantIdsCache[merchantNbrKey];

            if (!merchantInfo) {

                merchantInfo = null;
            }
        }

        return merchantInfo;
    }

    public getCustomerInfoFromCache(customerNbr: string): CustomerPrimaryKeyInfoModel {

        let customerInfo: CustomerPrimaryKeyInfoModel = null;

        if (customerNbr) {

            let customerNbrKey = customerNbr.trim().toUpperCase();

            customerInfo = this._customerIdsCache[customerNbrKey];

            if (!customerInfo) {

                customerInfo = null;
            }
        }

        return customerInfo;
    }

    //addToTerminalNumberList(eventModel: LidPrimeyKeyEventModel): void {

    //    this._terminalNumbers[eventModel.searchString] = eventModel;

    //    let terminalIdsList: Map<string, LidPrimeyKeyEventModel> = <Map<string, LidPrimeyKeyEventModel>>JSON.parse(JSON.stringify(this._terminalNumbers));

    //    this._terminalNumberListChangedSource.next(terminalIdsList);
    //}

    //addToMerchantIdsList(eventModel: LidPrimeyKeyEventModel): void {

    //    this._merchantIds[eventModel.searchString] = eventModel;

    //    let merchantIdsList: Map<string, LidPrimeyKeyEventModel> = <Map<string, LidPrimeyKeyEventModel>>JSON.parse(JSON.stringify(this._merchantIds));

    //    this._merchantNumberListChangedSource.next(merchantIdsList);
    //}

    //addToCustomerIdsList(eventModel: LidPrimeyKeyEventModel): void {

    //    this._customerIds[eventModel.searchString] = eventModel;

    //    let customerIdsList: Map<string, LidPrimeyKeyEventModel> = <Map<string, LidPrimeyKeyEventModel>>JSON.parse(JSON.stringify(this._customerIds));

    //    this._customerNumberListChangedSource.next(customerIdsList);
    //}

    //addToIdsList(eventModel: LidPrimeyKeyEventModel): void {

    //    switch (eventModel.lidType) {

    //        case LidTypesEnum.CustomerID:
    //            this.addToCustomerIdsList(eventModel);
    //            break;

    //        case LidTypesEnum.MerchantID:
    //            this.addToMerchantIdsList(eventModel);
    //            break;

    //        case LidTypesEnum.TerminalNbr:
    //            this.addToTerminalNumberList(eventModel);
    //            break;

    //        default:
    //            break;
    //    }

    //}
}