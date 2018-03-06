﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IProductTypesApi
    {
        Task<IEnumerable<ProductTypeModel>> GetAllProductTypesAsync();
    }
}
