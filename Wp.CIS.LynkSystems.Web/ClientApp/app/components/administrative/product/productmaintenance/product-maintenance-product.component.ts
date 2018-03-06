
import { Component, OnInit, Output, OnDestroy, EventEmitter } from '@angular/core';
import { ResponseOptions, Response, ResponseType, ResponseOptionsArgs } from '@angular/http'
import { FormGroup, FormControl, Validators, FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';

import { Observable } from "RxJS/Rx";

import { ProductsService } from '../../../../services/administrative/products/productmaintenance/products.service';
import { ProductModel } from '../../../../models/administration/product/productmaintenance/product.model';
import { ProductMaintenanceRowSelectedModel } from '../../../../models/administration/product/productmaintenance/product-maintenance-row-selected.model';

import { DownloadTimesService } from '../../../../services/administrative/products/productmaintenance/download-times.service';
import { ProductTypesService } from '../../../../services/administrative/products/productmaintenance/product-types.service'
import { ManufacturersService } from '../../../../services/administrative/products/productmaintenance/manufacturers.service';
import { InstallTypesService } from '../../../../services/administrative/products/productmaintenance/install-types.service';
import { BrandService } from '../../../../services/administrative/products/productmaintenance/brand.service';
import { MobileLookupService } from '../../../../services/administrative/products/productmaintenance/mobile-lookup.service';
import { AuditHistoryService } from '../../../../services/administrative/products/productmaintenance/audit-history.service';
import { ErrorService } from '../../../../services/error.service';

import { IDescription } from '../../../../models/administration/product/productmaintenance//idescription.model';
import { DownloadTimeModel } from '../../../../models/administration/product/productmaintenance//download-time.model';
import { ProductTypeModel } from '../../../../models/administration/product/productmaintenance//product-type.model';
import { ManufacturerModel } from '../../../../models/administration/product/productmaintenance//manufacturer.model';
import { InstallTypeModel } from '../../../../models/administration/product/productmaintenance//install-type.model';
import { MobileLookupModel } from '../../../../models/administration/product/productmaintenance//mobile-lookup.model';
import { AuditHistoryModel } from '../../../../models/administration/product/productmaintenance//audit-history.model';

import { ActionTypeEnum } from '../../../../models/common/action-type.enum';
import { LidTypesEnum } from '../../../../models/common/lid-types.enum';

import { ProductBrandModel } from '../../../../models/administration/product/productmaintenance/product-brand.model';

import { LazyLoadEvent, DataTable, SelectItem } from 'primeng/primeng';


@Component({
    selector: 'product-maintenance-product',
    templateUrl: './product-maintenance-product.component.html',
    styleUrls: ['./product-maintenance-product.component.css'],
    providers: [ProductsService,
        DownloadTimesService,
        ProductTypesService,
        ManufacturersService,
        InstallTypesService,
        BrandService,
        MobileLookupService,
        AuditHistoryService,
        ErrorService]
})
export class ProductMaintenanceProductComponent implements OnInit {

    @Output() onSelectedRowChanged = new EventEmitter<ProductMaintenanceRowSelectedModel>();

    public _selectedProduct: ProductModel;

    public _selectedProductForEdit: ProductModel;

    public _gridDataSource: Array<ProductModel>;

    public _errorMessage: string;

    public _productBrandOptions: Array<SelectItem>

    public _selectedProductBrand: ProductBrandModel;

    public _isMobileOptions: Array<SelectItem>;

    public _selectedIsMobile: number;

    public _installTypes: Array<SelectItem>;

    public _selectedInstallType: InstallTypeModel;

    public _manufacturers: Array<SelectItem>;

    public _selectedManufacturer: ManufacturerModel;

    public _productTypes: Array<SelectItem>;

    public _selectedProductType: ProductTypeModel;

    public _downloadTimes: Array<SelectItem>;

    public _selectedDownloadTime: SelectItem;

    private _categoryId: string;

    public _displayLoadingDialog: boolean;

    public _totalRecords: number;

    get selectedProduct(): ProductModel {

        return this._selectedProduct;
    }

    constructor(private _productsService: ProductsService,
        private _downloadTimesService: DownloadTimesService,
        private _productsTypeService: ProductTypesService,
        private _manufacturersService: ManufacturersService,
        private _installTypesService: InstallTypesService,
        private _brandService: BrandService,
        private _mobileLookupService: MobileLookupService,
        private _auditHistoryService: AuditHistoryService,
        private _errorService: ErrorService) {

        this._gridDataSource = null;

        this._errorMessage = null;
    
        this._productBrandOptions = null;

        this._selectedProduct = null;
    
        this._selectedProductBrand = null;
    
        this._isMobileOptions = new Array<SelectItem>();
    
        this._selectedIsMobile = null;
    
        this._installTypes = new Array<SelectItem>();
    
        this._selectedInstallType = null;
    
        this._manufacturers = new Array<SelectItem>();
    
        this._selectedManufacturer = null;
    
        this._productTypes = new Array<SelectItem>();
    
        this._selectedProductType = null;
    
        this._downloadTimes = new Array<SelectItem>();
        
        this._selectedDownloadTime = null;
    
        this._categoryId = null;

        this._totalRecords = 0;

        this._displayLoadingDialog = true;
    }

    public ngOnInit(): void {

    };

    public ngOnDestroy(): void {


    }
  
    /**
      * @method This is called in order to retrieve a page of data for the
      *          grid.
      * @param event This holds the even information.
      */
    public loadData(event: LazyLoadEvent): void {

        this.onSelectedRowChanged.emit(null);

        this._displayLoadingDialog = true;
        this._selectedProduct = null;

        this._productsService
            .getAllProductsWithPaging(event.first,
            event.rows,
            event.sortField,
            event.sortOrder)
            .subscribe(

            (data: Array<ProductModel>): void => {

                this._displayLoadingDialog = false;

                this._gridDataSource = data;
                this._totalRecords = data[0].overallCount;
            },
            (error: Response): void => {
                this._displayLoadingDialog = false;
                this.setErrorMessageFromResponse(error);

                }
            );
    }

    private setErrorMessageFromResponse(error: Response): void {

        if (error) {

            let errorMessage: string = error.text();

            if (errorMessage
                && error.text) {

                errorMessage = errorMessage.replace(/['"]+/g, '');
                this._errorMessage = errorMessage;
            }
        }
    }

    private convertToListOfSelectItems<T extends IDescription>(data: Array<T>): Array<SelectItem> {

        let selectItems: Array<SelectItem>
            = new Array<SelectItem>();

        if (data
            && data.length > 0) {

            for (let index: number = 0; index < data.length; index++) {

                let current = data[index];
                if (current) {

                    let newItem: SelectItem
                        = {
                            label: current.description,
                            value: current
                        };

                    selectItems.push(newItem);
                }
            }
        }
 
        return selectItems;
    }

     public onRowSelect(event: any): void {

        try {

            if (this._selectedProduct) {

                this._selectedProductForEdit = <ProductModel>JSON.parse(JSON.stringify(this._selectedProduct));
            } else {
                this._selectedProductForEdit = null;
            }

            let eventParam: ProductMaintenanceRowSelectedModel = new ProductMaintenanceRowSelectedModel();
            eventParam.selectedProduct = this._selectedProductForEdit;

            if (this._selectedInstallType) {
                eventParam.selectedInstallType = this._selectedInstallType.description;
            }

            this.onSelectedRowChanged.emit(eventParam);
        } catch (err) {

            this._displayLoadingDialog = false;
            this._errorService.logException("Error in ngOnInit()", err);
        }
    };

    private addButtonClickHandler(): void {

        alert("Not implemented");
    }

    private editButtonClickHandler(): void {

        alert("Not implemented");
    }

    private helpButtonClickHandler(): void {

        alert("Not implemented");
    }
}