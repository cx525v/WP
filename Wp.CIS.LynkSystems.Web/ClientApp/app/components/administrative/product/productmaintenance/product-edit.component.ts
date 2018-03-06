

import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core'
import { ResponseOptions, Response, ResponseType, ResponseOptionsArgs } from '@angular/http'

import { LazyLoadEvent, DataTable, SelectItem } from 'primeng/primeng';

import { ErrorService } from '../../../../services/error.service';
import { ProductsService } from '../../../../services/administrative/products/productmaintenance/products.service';

import { ProductModel } from '../../../../models/administration/product/productmaintenance/product.model';
import { ProductLookupValuesModel } from '../../../../models/administration/product/productmaintenance/product-lookup-values.model';
import { ProductBrandModel } from '../../../../models/administration/product/productmaintenance/product-brand.model';
import { IDescription } from '../../../../models/administration/product/productmaintenance/idescription.model';
import { DownloadTimeModel } from '../../../../models/administration/product/productmaintenance/download-time.model';
import { ProductTypeModel } from '../../../../models/administration/product/productmaintenance/product-type.model';
import { ManufacturerModel } from '../../../../models/administration/product/productmaintenance/manufacturer.model';
import { InstallTypeModel } from '../../../../models/administration/product/productmaintenance/install-type.model';
import { MobileLookupModel } from '../../../../models/administration/product/productmaintenance/mobile-lookup.model';
import { ProductMaintenanceRowSelectedModel } from '../../../../models/administration/product/productmaintenance/product-maintenance-row-selected.model';

@Component({
    selector: "product-edit",
    templateUrl: "./product-edit.component.html",
    styleUrls: ["./product-edit.component.css"],
    providers: [
        ProductsService,
        ErrorService
    ]
})
export class ProductEditComponent implements OnInit {

    private _editMode: boolean;

    public _productModelToEdit: ProductModel;

    public _productBrandOptions: Array<SelectItem>

    public _errorMessage: string;

    public _isMobileOptions: Array<SelectItem>;

    public _installTypes: Array<SelectItem>;

    public _manufacturers: Array<SelectItem>;

    public _productTypes: Array<SelectItem>;

    public _downloadTimes: Array<SelectItem>;

    public _selectedDownloadTime: SelectItem;

    public _selectedProductType: ProductTypeModel;

    public _selectedManufacturer: ManufacturerModel;

    public _selectedInstallType: InstallTypeModel;

    public _selectedIsMobile: number;

    @Input('EditMode')
    set EditMode(value: boolean) {
        this._editMode = value;
    }

    @Input('ProductModelToEdit')
    set ProductModelToEdit(value: ProductModel) {

        this._productModelToEdit = value;

        try {

            this.setSelectedDownloadTime();

            this.setSelectedProductType();

            this.setSelectedManufacturer();

            this.setSelectedInstallType();

            this.setSelectedIsMobile();

            this.setSelectedProductType();

        } catch (err) {

            this._errorService.logException("Error in ngOnInit()", err);
        }

    }

    constructor(
        private _productService: ProductsService,
        private _errorService: ErrorService) {

        this._editMode = false;

        this._productModelToEdit = null;

        this._isMobileOptions = new Array<SelectItem>();

        this._installTypes = new Array<SelectItem>();

        this._manufacturers = new Array<SelectItem>();

        this._productTypes = new Array<SelectItem>();

        this._downloadTimes = new Array<SelectItem>();

        this._selectedDownloadTime = null;

        this._selectedProductType = null;

        this._selectedManufacturer = null;

        this._selectedInstallType = null;

        this._selectedIsMobile = null;
    }

    public ngOnInit(): void {

        try {

            this.populateDropdownLists();

        } catch (err) {

            this._errorService.logException("Error in ngOnInit()", err);
        }
    }; 

    private setSelectedProductBrand(): void {

        if (this._productModelToEdit
            && this._productModelToEdit.brandId >= 0) {

            let selectedItems: Array<SelectItem> = this._productBrandOptions
                .filter((x) => x.value.id === this._productModelToEdit.brandId);

            if (selectedItems
                && selectedItems.length > 0) {

                this._selectedIsMobile = selectedItems[0].value;
            }
        }

    }

    private setSelectedIsMobile(): void {

        if (this._productModelToEdit
            && this._productModelToEdit.productTypeID >= 0) {

            let selectedItems: Array<SelectItem> = this._isMobileOptions
                .filter((x) => x.value.mobileType === this._productModelToEdit.isMobile
                    || (null === x.value.mobileType && null === this._productModelToEdit.isMobile));

            if (selectedItems
                && selectedItems.length > 0) {

                this._selectedIsMobile = selectedItems[0].value;
            }
        }
    }

    private setSelectedInstallType(): void {

        if (this._productModelToEdit
            && this._productModelToEdit.productTypeID >= 0) {

            let selectedItems: Array<SelectItem> = this._installTypes
                .filter((x) => x.value.installType == this._productModelToEdit.installType);

            if (selectedItems
                && selectedItems.length > 0) {

                this._selectedInstallType = selectedItems[0].value;
            }
        }
    }

    private setSelectedManufacturer(): void {

        if (this._productModelToEdit
            && this._productModelToEdit.dlTypeID >= 0) {

            let selectedItems: Array<SelectItem> = this._manufacturers
                .filter((x) => x.value.mfgCode == this._productModelToEdit.mfgCode);

            if (selectedItems
                && selectedItems.length > 0) {

                this._selectedManufacturer = selectedItems[0].value;
            }
        }
    }

    private setSelectedProductType(): void {

        if (this._productModelToEdit
            && this._productModelToEdit.productTypeID >= 0) {

            let selectedItems: Array<SelectItem> = this._productTypes
                .filter((x) => x.value.productTypeId == this._productModelToEdit.productTypeID);

            if (selectedItems
                && selectedItems.length > 0) {

                this._selectedProductType = selectedItems[0].value;
            }
        }
    }
    private populateDropdownLists(): void {

        this._productService
            .getLookupsForProducts()
            .subscribe(
            (data: ProductLookupValuesModel) => {

                this._productBrandOptions = this.convertToListOfSelectItems(data.brands);
                this._downloadTimes = this.convertToListOfSelectItems(data.downloadTimes);
                this._installTypes = this.convertToListOfSelectItems(data.installTypes);
                this._manufacturers = this.convertToListOfSelectItems(data.manufacturers);
                this._isMobileOptions = this.convertToListOfSelectItems(data.mobileLookups);
                this._productTypes = this.convertToListOfSelectItems(data.productTypes);

                this.setSelectedProductBrand();
                this.setSelectedDownloadTime();
                this.setSelectedInstallType();
                this.setSelectedManufacturer();
                this.setSelectedIsMobile();
                this.setSelectedProductType();

            },
            (error: Response) => {

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

    private setSelectedDownloadTime(): void {


        if (this._productModelToEdit
            && this._productModelToEdit.dlTypeID >= 0) {

            let selectedItems: Array<SelectItem> = this._downloadTimes
                .filter((x) => x.value.dlTypeID === this._productModelToEdit.dlTypeID);

            if (selectedItems
                && selectedItems.length > 0) {

                this._selectedDownloadTime = selectedItems[0].value;
            }
        }
    }


}