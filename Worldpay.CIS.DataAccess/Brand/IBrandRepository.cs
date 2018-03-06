using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Worldpay.CIS.DataAccess.Brand
{
    public interface IBrandRepository
    {
        Task<IEnumerable<ProductBrandModel>> GetProductBrandsAsync();
    }
}
