using System;
using NSubstitute;
using System.Security.Principal;
using Wp.CIS.LynkSystems.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Xunit;
using Wp_CIS_LynkSystems;
using Wp_CIS_LynkSystems.Controllers;
using System.Threading;
using System.Security.Permissions;
using System.Net.Http;
using System.Security.Claims;

namespace CIS.WebApp.UnitTests
{

    public class TestHomeController : Controller
    {
        
        [Fact]
        public void WindowsAuthenticationTest()
        {

            //Arrange


            var _userName = "CISTestUser";
            var _domainName = "domain";
            // Attach the principal to the current thread.  
            // This is not required unless repeated validation must occur,  
            // other code in your application must validate, or the   
            // PrincipalPermisson object is used.   


            IOptions<AppConfigSettings> _optionsAccessor = Substitute.For<IOptions<AppConfigSettings>>();
            var username = "domain\\CISTestUser";

            ClaimsPrincipal cp = Substitute.For<ClaimsPrincipal>(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                                {
                                      new Claim(ClaimTypes.Name, username)
                                })));
            cp.Identity.IsAuthenticated.ReturnsForAnyArgs(true);
            cp.Identity.Name.ReturnsForAnyArgs(username);

            _optionsAccessor.Value.ReturnsForAnyArgs(new AppConfigSettings()
            {
                EnvironmentName = "Fake Environment Name",
                WebApiUrl = "Fake URL"
            });
            HomeController controller = GetFakeController(_optionsAccessor);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = cp }
            };
            
            //Act

            var result = controller.Index() as ViewResult;
            var model = result.Model as IndexPageModel;


            //Assert
            //Test if the Controller's Action result return to View.
            Assert.NotNull(result);

            //Test the Windows Authentication
            Assert.True(model.User.IsAuthenticated);

            //Test the UserInformation
            Assert.Equal(model.User.UserName, _userName);
            Assert.Equal(model.User.DomainName, _domainName);

        }

        private HomeController GetFakeController(IOptions<AppConfigSettings> _optionsAccessor)
        {
            return new HomeController(_optionsAccessor);
        }


    }



}
