
import { Component, OnInit } from '@angular/core';
import { ResponseOptions, Response, ResponseType, ResponseOptionsArgs } from '@angular/http'
import { FormGroup, FormControl, Validators, FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';


import { ProductModel } from '../../../../models/administration/product/productmaintenance/product.model';
import { ProductMaintenanceRowSelectedModel } from '../../../../models/administration/product/productmaintenance/product-maintenance-row-selected.model';

import { AuditHistoryService } from '../../../../services/administrative/products/productmaintenance/audit-history.service';
import { ErrorService } from '../../../../services/error.service';

import { AuditHistoryModel } from '../../../../models/administration/product/productmaintenance/audit-history.model';

import { ActionTypeEnum } from '../../../../models/common/action-type.enum';
import { LidTypesEnum } from '../../../../models/common/lid-types.enum';


@Component({
    templateUrl: './product-maintenance.component.html',
    styleUrls: ['./product-maintenance.component.css'],
    providers: [
        AuditHistoryService,
        ErrorService
    ]
})
export class ProductMaintenanceComponent implements OnInit {

    public _errorMessage: string;

    public _selectedProduct: ProductModel;

    public _isEditMode: boolean;

    public _selectedInstallType: string;

    public _auditHistoryForSelectedRecord: AuditHistoryModel;

    public _displayLoadingDialog: boolean;

    /**
     * @constructor This initializes the class state.
     */
    constructor(
        private _auditHistoryService: AuditHistoryService,
        private _errorService: ErrorService) {

        this._errorMessage = null;

        this._selectedProduct = null;

        this._isEditMode = false;

        this._selectedInstallType = null;

        this._auditHistoryForSelectedRecord = null;

        this._displayLoadingDialog = false;
    }

    /**
     * @method This is used to initialize the state of the component
     */
    public ngOnInit(): void {

    };

     public onRowSelect(selectedItem: ProductMaintenanceRowSelectedModel): void {

         try {

             if (selectedItem) {

                 this._selectedProduct = selectedItem.selectedProduct;

                 this._selectedInstallType = selectedItem.selectedInstallType;

                 this.getAuditInformation();

             } else {

                 this._selectedProduct = null;

                 this._selectedInstallType = null;

                 this._auditHistoryForSelectedRecord = null;
             }

         } catch (err) {

             this._errorService.logException("Error during row selection", err);
         }
       
    };

    private getAuditInformation(): void {

        this._auditHistoryService
            .getLatestAuditHistory(LidTypesEnum.TerminalID, this._selectedProduct.productCode, ActionTypeEnum.ProjectMaintenanceScreen)
            .subscribe(
            (data: AuditHistoryModel) => {

                this._auditHistoryForSelectedRecord = data;
            },
            (error: Response) => {

                this._errorMessage = error.text();
            }
        );
    }

}