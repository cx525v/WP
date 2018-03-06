
import { ProductBrandModel } from './product-brand.model';
import { DownloadTimeModel } from './download-time.model';
import { ManufacturerModel } from './manufacturer.model';
import { ProductTypeModel } from './product-type.model';
import { InstallTypeModel } from './install-type.model';
import { MobileLookupModel } from './mobile-lookup.model';

export class ProductLookupValuesModel {

 
    public downloadTimes: Array<DownloadTimeModel>;

 
    public productTypes: Array<ProductTypeModel>;

 
    public manufacturers: Array<ManufacturerModel>;


    public installTypes: Array<InstallTypeModel>;

 
    public mobileLookups: Array<MobileLookupModel>;

 
    public brands: Array<ProductBrandModel>;


}