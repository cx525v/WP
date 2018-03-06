using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Wp.CIS.LynkSystems.WebApi.Models.Administrative.Product
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductLookupValuesModel
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DownloadTimeModel> DownloadTimes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ProductTypeModel> ProductTypes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ManufacturerModel> Manufacturers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<InstallTypeModel> InstallTypes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<MobileLookupModel> MobileLookups { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ProductBrandModel> Brands { get; set; }


    }
}
