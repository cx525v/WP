using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.WebApi.UnitTests.Parameters
{
    public class MockParametersRepository
    {
        List<Wp.CIS.LynkSystems.Model.Parameters> list;

        public MockParametersRepository()
        {
            list = new List<Wp.CIS.LynkSystems.Model.Parameters>();
            list.Add(new Wp.CIS.LynkSystems.Model.Parameters()
            {
                ParamName =   "% USED TO CALCULATE TAX AMT",
                ParameterDesc = "This field is used to specify the percent used in calculating the tax amount.Use the nn.nnn format",
                DataType = "decimal",
                IsCardSpecific = false,
                PDL = true,
                UseSpace = false,
                IsStratus = false,
                IsVericentre = true,
                StratusMultiplier = true,
                IsCustomerDefault = true,

            });
        }
        public ApiResult<ICollection<Wp.CIS.LynkSystems.Model.Parameters>> GetMockData()
        {
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.Parameters>> expected = new ApiResult<ICollection<Wp.CIS.LynkSystems.Model.Parameters>>()
            {
                Result = list
            };
            return expected;
        }
    }
}
