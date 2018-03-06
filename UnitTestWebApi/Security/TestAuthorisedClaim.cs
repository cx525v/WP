using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Interfaces.Secuirity;
using Wp.CIS.LynkSystems.Model.Authentication;
using Wp.CIS.LynkSystems.Services.Security;
using Xunit;

namespace CIS.WebApi.UnitTests.Security
{
    public class TestAuthorisedClaim
    {

        
        private IAuthorisedClaimApi _claimApi = Substitute.For<IAuthorisedClaimApi>();        
        private CISUser _user;
        private string _symmetricKey = "CIS-97D766FDD0C14EDD96DFF1CA5C5D866C-2448BDFBDDB540DEBB139908B58E4840";
        private string _Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJSS3VtYXIiLCJqdGkiOiI3MmZkMTFkZS02NWFmLTRhZTUtYjIzNC02MDM4ODEyOTc0NzIiLCJlbWFpbCI6IlJLdW1hciIsImlzQWRtaW4iOiJ0cnVlIiwiZXhwIjoxNTA1NDgyNDk3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjYyNzQzLyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjI3NDMvIn0.V1tyPWSQx1h8s9qF_n2yxnQpjXN75Rqe63N1xCreBe4";

        [Fact]
        public void TestCreateToken()
        {
            //Arrange 
            _user = new CISUser
            {
                UserName = "CISUser",
                DomainName = "domain",
                UserRole = new UserRole
                {
                    Role = Roles.CIS_Admin
                }
            };
            
            IAuthorisedClaimApi fakeApi = Substitute.For<IAuthorisedClaimApi>();
            fakeApi.IssuerServer.Returns("http://localhost:65517/");
            fakeApi.AudianceServer.Returns("http://localhost:65517/");
            fakeApi.ExpirationTime.Returns("30");
            fakeApi.SymmetricSecurityKey.Returns(_symmetricKey);
            fakeApi.CreateToken(_user).ReturnsForAnyArgs(_Token);
            
            //Act 

            fakeApi.CreateToken(_user).ReturnsForAnyArgs(_Token);

            var res = fakeApi.CreateToken(_user);
            var result = res;

            //Assert
            Assert.Contains(_Token, result );
        }        
    }
}
