using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wp.CIS.LynkSystems.Web.Models;
using Microsoft.Extensions.Options;

using System.Threading;

namespace Wp_CIS_LynkSystems.Controllers
{
   
    
    public class HomeController : Controller
    {
        #region Private Fields

        IOptions<AppConfigSettings> _optionsAccessor;
        private string[] _userName;
        private CISUser _user = new CISUser();

        #endregion

        #region Public Constructor

        public HomeController(IOptions<AppConfigSettings> optionsAccessor)
        {
            this._optionsAccessor = optionsAccessor;
        }

        #endregion

        #region Actions

     
        public IActionResult Index()
        {
            if (User != null )
            {
                if (User.Identity.Name != null && User.Identity.Name != "" && User.Identity.IsAuthenticated == true)
                {
                    if (User.Identity.Name.IndexOf("\\") > 0)
                    {
                        _userName = User.Identity.Name.Split('\\');
                        _user.DomainName = _userName[0];
                        _user.UserName = _userName[1];
                        _user.IsAuthenticated = User.Identity.IsAuthenticated;
                    }
                }
            }

            var theModel = new IndexPageModel
            {
                AppConfigSettings = new AppConfigSettings
                {
                    WebApiUrl = this._optionsAccessor.Value.WebApiUrl,
                    EnvironmentName = this._optionsAccessor.Value.EnvironmentName
                },
                User = _user
            };

            return View(theModel);
        }

        public IActionResult Error()
        {
            return View();
        }        

        #endregion
    }
}
