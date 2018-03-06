using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.WebApi.Models.Administrative.Product
{
    /// <summary>
    /// 
    /// </summary>
    public class GetProductsWithPagingRequestModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Range(0, int.MaxValue)]
        [Required]
        public int FirstRecordNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [RangeAttribute(1, 100)]
        [Required]
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SortOrderEnum SortOrder { get; set; }

    }
}
