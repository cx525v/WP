using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wp.CIS.LynkSystems.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Wp.CIS.LynkSystems.Model.Administrative;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Models.Administrative.Product;
using Wp.CIS.LynkSystems.Interfaces.Lookup;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    //[Route("api/Products")]
    public class ProductsController : Controller
    {
        #region Private Fields

        private readonly IDistributedCache _cache;
        private readonly IProductApi _productMaintenanceApi;
        private readonly IDownloadTimesApi _downloadTimesApi;
        private readonly IProductTypesApi _productTypesApi;
        private readonly IManufacturersApi _manufacturersApi;
        private readonly IInstallTypesApi _installTypesApi;
        private readonly IMobileLookupApi _mobileLookupApi;
        private readonly IBrandApi _brandApi;
        private readonly IStringLocalizer<ProductsController> _localizer;

        #endregion

        #region Public Constructors

        /// <summary>
        /// This is the constructor. It is used to 
        /// </summary>
        public ProductsController(IDistributedCache cache,
            IProductApi productMaintenanceApi,
            IDownloadTimesApi downloadTimesApi,
            IProductTypesApi productTypesApi,
            IManufacturersApi manufacturersApi,
            IInstallTypesApi installTypesApi,
            IMobileLookupApi mobileLookupApi,
            IBrandApi brandApi,
            IStringLocalizer<ProductsController> localizer)
        {
            this._cache = cache;

            this._productMaintenanceApi = productMaintenanceApi;

            this._downloadTimesApi = downloadTimesApi;
            this._productTypesApi = productTypesApi;
            this._manufacturersApi = manufacturersApi;
            this._installTypesApi = installTypesApi;
            this._mobileLookupApi = mobileLookupApi;
            this._brandApi = brandApi;

            this._localizer = localizer;
        }

 

        #endregion

        #region Public Actions/Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/Products
        [Route("api/Products")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult response = null;

            try
            {
                var products = await this._productMaintenanceApi
                                              .GetAllProductsAsync();

                response = this.Ok(products);
            }
            catch(Exception)
            {
                var msg = this._localizer?["ProductsAllErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Products/5
        //[HttpGet("{id}", Name = "Get")]
        [Route("api/Products/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            IActionResult response = null;

            try
            {
                var data = new Operation(_cache).RetrieveCache(id.ToString(), new ProductModel());
                if (data == null)
                {
                    data = await this._productMaintenanceApi
                                                  .GetFirstProductByDescriptionAsync(id);
                    if (data != null)
                    {
                        //Now add data to cache..
                        await new Operation(_cache).AddCacheAsync(id.ToString(), data);
                    }
                }

                response = this.Ok(data);
            }
            catch (Exception)
            {
                var msg = this._localizer?["ProductSingleErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/Products/ProductsWithPaging")]
        [HttpGet]
        public async Task<IActionResult> GetProductsWithPaging(GetProductsWithPagingRequestModel request)
        {
            IActionResult response = null;

            try
            {
                if (ModelState.IsValid)
                {

                    var product = await this._productMaintenanceApi
                                                  .GetProductsWithPagingAsync(request.FirstRecordNumber, 
                                                  request.PageSize, 
                                                  request.SortField, 
                                                  request.SortOrder);

                    response = this.Ok(product);
                }
                else
                {
                    response = BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                var msg = this._localizer?["ProductPageErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Products/GetLookupsForProducts")]
        [HttpGet]
        public async Task<IActionResult> GetLookupsForProducts()
        {
            IActionResult response = null;

            try
            {
                var models = new ProductLookupValuesModel();

                models.Brands = await this._brandApi.GetProductBrandsAsync();
                models.DownloadTimes = await this._downloadTimesApi.GetAllDownloadTimesAsync();
                models.InstallTypes = await this._installTypesApi.GetAllInstallTypesAsync();
                models.Manufacturers = await this._manufacturersApi.GetAllManufacturersAsync();
                models.MobileLookups = await this._mobileLookupApi.GetAllMobileLookupsAsync();
                models.ProductTypes = await this._productTypesApi.GetAllProductTypesAsync();
                
                response = this.Ok(models);

            }
            catch (Exception)
            {
                var msg = this._localizer?["ProductPageErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }
        #endregion
    }
}
