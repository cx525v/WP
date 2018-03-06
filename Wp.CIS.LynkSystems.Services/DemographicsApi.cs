using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services.DapperConnection;

namespace Wp.CIS.LynkSystems.Services
{
    public class DemographicsApi : ConnectionManager, IDemographicsApi
    {
        public DemographicsApi(IOptions<Settings> optionsAccessor) : base(optionsAccessor.Value.CISConnectionString)
        {

        }

        /// <summary>
        /// Get the Demographics for a LID
        /// </summary>
        /// <param name="LidType"></param>
        /// <param name="Lid"></param>
        /// <returns></returns>
        public async Task<ICollection<Demographics>> GetDemographicsByLidType(Helper.LIDTypes LidType, string Lid)
        {
            return await WithConnection(async d =>
            {
                var p = new DynamicParameters();
                p.Add("LIDType", LidType, DbType.Int32);
                p.Add("LID", Lid, DbType.String);
                var demographics = await d.QueryAsync<Demographics>(sql: "uspCISPlusGetDemographicsByLIdType", param: p, commandType: CommandType.StoredProcedure);
                return demographics.ToList();
            });
        }
    
    }
}
