using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Wp.CIS.LynkSystems.WebApi.Controllers.Security;
using Wp.CIS.LynkSystems.Services.Security;
using Wp.CIS.LynkSystems.Model.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using System.ComponentModel.DataAnnotations;
using Wp.CIS.LynkSystems.Interfaces.Secuirity;
using Microsoft.Extensions.Localization;
using CIS.WebApi.UnitTests.Common;


namespace CIS.WebApi.UnitTests.Security
{
    public class TestAccountController //: Controller
    {

        private IConfigurationRoot _configurationRoot = Substitute.For<IConfigurationRoot>();
        private IStringLocalizer<AccountController> _localizer = Substitute.For<IStringLocalizer<AccountController>>();        
        private IAuthorisedClaimApi _claimApi = Substitute.For<IAuthorisedClaimApi>();

        private CISUser _user;
        private string _Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJSS3VtYXIiLCJqdGkiOiI3MmZkMTFkZS02NWFmLTRhZTUtYjIzNC02MDM4ODEyOTc0NzIiLCJlbWFpbCI6IlJLdW1hciIsImlzQWRtaW4iOiJ0cnVlIiwiZXhwIjoxNTA1NDgyNDk3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjYyNzQzLyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjI3NDMvIn0.V1tyPWSQx1h8s9qF_n2yxnQpjXN75Rqe63N1xCreBe4";

        [Fact]
        public void TestModel_IsNull_State()
        {

            //Arrange            
            _configurationRoot = MockConfigurationRoot();
            
            _localizer = new MockStringLocalizer<AccountController>();
            var controller = GetFakeController(_configurationRoot, _localizer, _claimApi);
            

            //Act
            var result = controller.CreateToken(_user);


            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode.ToString(), "400");
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value, this._localizer?["ModelIsNull"]?.Value);
        }

        [Fact]
        public void TestModel_User_IsNull_Invalid_State()
        {
                        
             //Arrange
             _configurationRoot = MockConfigurationRoot();
            _localizer = new MockStringLocalizer<AccountController>();
            var controller = GetFakeController(_configurationRoot, _localizer, _claimApi);

            _user = new CISUser
            {
                UserName = null,
                DomainName = null,
                UserRole = null
            };
            

            //Act
            var result = controller.CreateToken(_user);

            string statusCode = Convert.ToString(((Microsoft.AspNetCore.Mvc.UnauthorizedResult)result).StatusCode);
            //Assert
            Assert.Equal(statusCode, "401");

        }

        [Fact]
        public void TestModel_User_Invalid_State()
        {

            //Arrange
            _configurationRoot = MockConfigurationRoot();
            _localizer = new MockStringLocalizer<AccountController>();
            var controller = GetFakeController(_configurationRoot, _localizer, _claimApi);

            _user = new CISUser
            {
                UserName = "CISUser",
                DomainName = "domain",
                UserRole = null
            };


            //Act
            // Called the "CreateToken" of fake controller(FakeAccountController).
            // by introducing the model error making model state invalid
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.CreateToken(_user);


            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode.ToString(), "400");
        }

        [Fact]
        public void TestModel_User_Valid_State()
        {
            _user = new CISUser
            {
                UserName = "CISUser",
                DomainName = "domain",
                UserRole = new UserRole
                {
                    Role = Roles.CIS_Admin
                }
            };

            //Arrange
            IAuthorisedClaimApi fakeApi = Substitute.For<IAuthorisedClaimApi>();
            fakeApi.IssuerServer.Returns("fakeIssure");
            fakeApi.AudianceServer.Returns("fakeAudianceServer");
            fakeApi.ExpirationTime.Returns("fake ExpirationTime");
            fakeApi.SymmetricSecurityKey.Returns("fake SymmetricSecurityKey");
            fakeApi.CreateToken(_user).ReturnsForAnyArgs(_Token);

            IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
            
            _localizer = new MockStringLocalizer<AccountController>();
            AccountController ctrl = new AccountController(configurationRoot, _localizer, fakeApi);
                        
            //Act
            var res = ctrl.CreateToken(_user);
            var result = res;

                       
            //Assert
            Assert.Equal(JsonConvert.SerializeObject(_Token), ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value.ToString());
        }

       

        private AccountController GetFakeController(IConfigurationRoot _configurationRoot,
                                         IStringLocalizer<AccountController> _localizer,
                                         IAuthorisedClaimApi claimApi)
        {
            return new AccountController(_configurationRoot, _localizer, claimApi);
        }

        private string GetFakeToken()
        {
            return _Token;
        }
        private IConfigurationRoot MockConfigurationRoot()
        {
            _configurationRoot["Token:Issuer"] = "http://localhost:65517/";
            _configurationRoot["Token:Audience"] = "http://localhost:65517/";
            _configurationRoot["Token:Key"] = "CIS-97D766FDD0C14EDD96DFF1CA5C5D866C-2448BDFBDDB540DEBB139908B58E4840";
            _configurationRoot["Token:ExpirationTime"] = "30";

            return _configurationRoot;
        }
    }
}
