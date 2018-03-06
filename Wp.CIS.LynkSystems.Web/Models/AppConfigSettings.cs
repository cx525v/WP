using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Web.Models
{
    public class AppConfigSettings
    {
        #region Public Constructors

        /// <summary>
        /// Default class constructor
        /// </summary>
        public AppConfigSettings()
        {
            this.WebApiUrl = string.Empty;
        }

        #endregion

        #region Public Properties

        public string WebApiUrl { get; set; }

        public string EnvironmentName { get; set; }

        #endregion
    }
}
